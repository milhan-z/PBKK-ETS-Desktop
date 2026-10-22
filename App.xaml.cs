using System.Windows;
using SISMA.Desktop.Data;

namespace SISMA.Desktop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DatabaseHelper.InitializeDatabase();
        }
    }
}
