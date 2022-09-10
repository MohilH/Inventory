using Inventory.BusinessLogic.Interface;
using Inventory.BusinessLogic.Services;
using Inventory.Repositories.Interface;
using Inventory.Repositories.Repository;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Inventory.WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            // Business Logic Register
            container.RegisterType<IInventoryProductInfoManager, InventoryProductInfoManager>();
            container.RegisterType<IVendorManager, VendorManager>();
            container.RegisterType<IPurchaseOrderManager, PurchaseOrderManager>();
            
           

            // Repository Register
            container.RegisterType<IInventory, Repositories.Repository.Inventory>();
            container.RegisterType<IVendor, Repositories.Repository.Vendor>();
            container.RegisterType<IPurchaseOrder, Repositories.Repository.PurchaseOrder>();
            container.RegisterType<IProduct, Repositories.Repository.Product>();


            container.RegisterType<IGlobalCurrency, Repositories.Repository.GlobalCurrency>();
            container.RegisterType<IPaymentTerms, Repositories.Repository.PaymentTerms>();
            container.RegisterType<ITaxingScheme, Repositories.Repository.TaxingScheme>();
             container.RegisterType<ILocation, Repositories.Repository.Location>();

             container.RegisterType<IVendorItem, Repositories.Repository.VendorItem>();

             container.RegisterType<IQuantity, Repositories.Repository.Quantity>();
             container.RegisterType<IItemPrice, Repositories.Repository.ItemPrice>();
             container.RegisterType<IPaymentHistory,Repositories.Repository.PaymentHistory>();
             container.RegisterType<IPricingScheme, Repositories.Repository.PricingScheme>();
             container.RegisterType<ICategory, Repositories.Repository.Category>();
             container.RegisterType<IBillOfMaterial, Repositories.Repository.BillOfMaterial>();
             container.RegisterType<IInventoryStatus, InventoryStatus>();

             container.RegisterType<IPaymentStatus, PaymentStatus>();
            
            //System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
           // System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }
    }
}