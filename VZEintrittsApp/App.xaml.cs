using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using KellermanSoftware.CompareNetObjects;
using VZEintrittsApp.API.AD;
using VZEintrittsApp.DataAccess.Contexts;
using VZEintrittsApp.Import.PDFReader;
using VZEintrittsApp.Model;
using VZEintrittsApp.View;

namespace VZEintrittsApp
{
    public partial class App
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
            services.AddSingleton<NoteContext>();
            services.AddSingleton<ManagementLevelContext>();
            services.AddSingleton<AttributeNotationContext>();
            services.AddSingleton<PhoneFormatContext>();
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<IDirectoryServices, DirectoryServices>();
            services.AddSingleton<FinalizeEmployee>();
            services.AddSingleton<AddIndividualProperties>();
            services.AddSingleton<ReadPdfDocument>();
            services.AddSingleton<CompareLogic>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
