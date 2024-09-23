CREATE DATABASE Crime
use Crime

-- Law Enforcement Agencies Table

CREATE TABLE LawEnforcementAgencies(
	AgencyId INT IDENTITY PRIMARY KEY,
	AgencyName VARCHAR(20),
	Jurisdiction VARCHAR(40),
	Contact VARCHAR(20),
)

-- Incidents Table

CREATE TABLE Incidents(
	IncidentId INT IDENTITY PRIMARY KEY,
	IncidentType VARCHAR(20),
	IncidentDate DATETIME,
	Location GEOGRAPHY,
	Description TEXT,
	Status VARCHAR(30)
	CHECK(Status IN('Open','Closed','UnderInvestigation')),
	AgencyId INT, --Is associated with one Law Enforcement Agency.
	FOREIGN KEY(AgencyId)
	REFERENCES LawEnforcementAgencies(AgencyId)
	ON DELETE SET NULL --  preserve the incident data but indicate that it no longer has an associated law enforcement agency
)
-- Removed VictimId and SuspectId

-- Victims Table

CREATE TABLE Victims(
	VictimId INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(20),
	LastName VARCHAR(20),
	DateOfBirth DATE,
	Gender VARCHAR(20)
	CHECK(Gender IN('Male','Female','Other')),
	VictimAddress TEXT,
	VictimContact VARCHAR(20)
)

-- Suspects Table

CREATE TABLE Suspects(
	SuspectId INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(20),
	LastName VARCHAR(20),
	DateOfBirth DATE,
	Gender VARCHAR(20)
	CHECK(Gender IN('Male','Female','Other')),
	SuspectAddress TEXT,
	SuspectContact VARCHAR(20),
)

-- Officers Table

CREATE TABLE Officers(
	OfficerId INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(20),
	LastName VARCHAR(20),
	BadgeNumber INT,
	Rank VARCHAR(40),
	MobileContact VARCHAR(20),
	AgencyId INT, --An Officer Works for a Single Law Enforcement Agency
	FOREIGN KEY(AgencyId)
	REFERENCES LawEnforcementAgencies(AgencyId) 
	ON DELETE SET NULL -- To indicate that the officer no longer belongs to any agency.
)

-- Evidence Table

CREATE TABLE Evidence(
	EvidenceId INT IDENTITY PRIMARY KEY,
	Description TEXT,
	LocationFound VARCHAR(40),
	IncidentId INT, -- Evidence Can Be Linked to an Incident
	FOREIGN KEY(IncidentId)
	REFERENCES Incidents(IncidentId)
	ON DELETE CASCADE-- delete all associated evidence when an incident is deleted.
)

-- Reports Table

CREATE TABLE Reports(
	ReportId INT IDENTITY PRIMARY KEY,
	IncidentId INT,
	ReportingOfficerID INT,
	ReportDate DATETIME,
	ReportDetails TEXT,
	Status VARCHAR(20)
	CHECK(Status IN('Draft','Finalized','Pending')),
	FOREIGN KEY(IncidentId) --Generated for Incidents by Officers 
	REFERENCES Incidents(IncidentId)
	ON DELETE CASCADE ,-- delete all associated reports if incident deletes
	FOREIGN KEY(ReportingOfficerID) --Generated for Incidents by Officers 
	REFERENCES Officers(OfficerId)
	ON DELETE SET NULL -- officer is deleted but report should still be there
)

--Can have multiple Victims and Suspects through the many-to-many
--relationship mapping tables

-- VICTIM-INCIDENT MAPPING

CREATE TABLE VictimIncidentMap(
	MappingId int PRIMARY KEY IDENTITY,
	VictimId int,
	IncidentId int,
	FOREIGN KEY(VictimId)
	REFERENCES Victims(VictimId),
	FOREIGN KEY(IncidentId)
	REFERENCES Incidents(IncidentId)

)

-- SUSPECT-INCIDENT MAPPING

CREATE TABLE SuspectIncidentMap(
	MappingId int PRIMARY KEY IDENTITY,
	SuspectId int,
	IncidentId int,
	FOREIGN KEY(SuspectId)
	REFERENCES Suspects(SuspectId),
	FOREIGN KEY(IncidentId)
	REFERENCES Incidents(IncidentId)
)