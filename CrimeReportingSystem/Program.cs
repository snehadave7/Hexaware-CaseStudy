using CrimeReportingSystem.Main;
using CrimeReportingSystem.Model;
using CrimeReportingSystem.Repository;

namespace CrimeReportingSystem {
    internal class Program {
        static void Main(string[] args) {
            CrimeReportingMenu menu = new CrimeReportingMenu();
            menu.Run();
            menu.Exit();

            
           
        }
    }
}
