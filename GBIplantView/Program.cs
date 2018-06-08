using GBIplantService.Interfaces;
using GBIplantService.realizationOfInterfaces;
using System;
using System.Collections.Generic;
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
            currentContainer.RegisterType<IBuyerService, BuyerServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGBIingridientService, GBIingridientsServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExecutorService, ExecutorServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGBIpieceOfArtService, GBIpieceOfArtServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
