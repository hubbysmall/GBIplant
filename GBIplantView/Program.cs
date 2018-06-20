using GBIplantService;
using GBIplantService.Interfaces;
using GBIplantService.realizationDB;
using GBIplantService.realizationOfInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace GBIplantView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IBuyerService, BuyerServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGBIingridientService, GBIingridientsServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExecutorService, ExecutorServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGBIpieceOfArtService, GBIpieceOfArtServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
