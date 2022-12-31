using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using VZEintrittsApp.API.AD;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Import.PDFReader;
using VZEintrittsApp.Model;
using VZEintrittsApp.View;

namespace VZEintrittsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            using (DbContext context = new DbContext())
            {
                context.Database.EnsureCreated();
            }

        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<GetNumberWindow>();
            services.AddSingleton<DbContext>();
            services.AddSingleton<RecordContext>();
            services.AddSingleton<LoggerContext>();
            services.AddSingleton<FinalizeContext>();
            services.AddScoped<Repository>();
            services.AddSingleton<DirectoryServices>();
            services.AddSingleton<FinalizeEmployee>();
            services.AddSingleton<AddIndividualProperties>();
            services.AddSingleton<ReadPdfDocument>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
