using Inventory.BusinessLogic.Interface;
using Inventory.CommonViewModels;
using Inventory.Repositories.Repository;
using Inventory.DomainModel.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Repositories.Interface;
using NLog;

namespace Inventory.BusinessLogic.Services
{
    public class VendorManager : IVendorManager
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private IVendor _IVendor;
        private IPaymentTerms _IPaymentTerms;
        private IGlobalCurrency _IGlobalCurrency;
        private ITaxingScheme _ITaxingScheme;
        private ILocation _ILocation;
        private IProduct _IProduct;

        private IVendorItem _IVendorItem;
        private IQuantity _IQuantity;
        private IItemPrice _IItemPrice;
        private IPaymentHistory _IPaymentHistory;
        private IPurchaseOrder _IPurchaseOrder;
        private IInventoryStatus _IInventoryStatus;
        private IPaymentStatus _IPaymentStatus;
        public VendorManager()
        {


        }
        public VendorManager(IVendor IVendor, IPaymentTerms IPaymentTerms, IGlobalCurrency IGlobalCurrency, ITaxingScheme ITaxingScheme, ILocation ILocation,
            IProduct IProduct, IVendorItem IVendorItems, IQuantity IQuantity, IItemPrice IItemPrice
            , IPaymentHistory IPaymentHistory, IPurchaseOrder IPurchaseOrder, IInventoryStatus IInventoryStatus, IPaymentStatus IPaymentStatus)
        {

            this._IVendor = IVendor;
            this._IPaymentTerms = IPaymentTerms;
            this._IGlobalCurrency = IGlobalCurrency;
            this._ITaxingScheme = ITaxingScheme;
            this._ILocation = ILocation;
            this._IProduct = IProduct;

            this._IVendorItem = IVendorItems;
            this._IQuantity = IQuantity;
            this._IItemPrice = IItemPrice;
            this._IPaymentHistory = IPaymentHistory;
            this._IPurchaseOrder = IPurchaseOrder;
            this._IPaymentStatus = IPaymentStatus;
            this._IInventoryStatus = IInventoryStatus;
        }


        #region Get All Location
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<LocationVM> getAllLocation()
        {
            List<LocationVM> getLocation = new List<LocationVM>();
            List<BASE_Location> LocationList = _ILocation.GetAll().ToList();
            foreach (var Location in LocationList)
            {
                LocationVM bojLocationVM = new LocationVM
                {
                    LocationId = Location.LocationId,
                    Name = Location.Name

                };
                getLocation.Add(bojLocationVM);

            }
            return getLocation;
        }

        #endregion
        #region Get exist Product List
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public List<ProductVM> existProductList()
        {
            List<ProductVM> getProductList = new List<ProductVM>();
            List<BASE_Product> getAllProduct = new List<BASE_Product>();

            getAllProduct = _IProduct.GetAll().ToList();
            foreach (var product in getAllProduct)
            {
                ProductVM bojProductVM = new ProductVM
                {
                    ProdId = product.ProdId,
                    Name = product.Name

                };
                getProductList.Add(bojProductVM);

            }
            return getProductList;
        }
        #endregion
        #region Vendor CRUD

        /// <summary>
        /// Use for get the Vendor List
        /// </summary>
        public List<VendorVM> getAllVendors()
        {
            List<VendorVM> getAllVendors = new List<VendorVM>();
            List<BASE_Vendor> objVendorList = _IVendor.GetAll().ToList();
            foreach (var vendor in objVendorList)
            {
                VendorVM objVendor = new VendorVM
                {
                    VendorId = vendor.VendorId,
                    Name = vendor.Name,
                    Balance = vendor.Balance,
                    Credit = vendor.Credit,
                    Address1 = vendor.Address1,
                    ContactName = vendor.ContactName,
                    Phone = vendor.Phone,
                    Fax = vendor.Fax,
                    Email = vendor.Email,
                    Website = vendor.Website,
                    Discount = vendor.Discount,
                    Remarks = vendor.Remarks,
                    PaymentTerms = vendor.PaymentTerms,
                    Currency = vendor.Currency,
                    TaxingScheme = vendor.TaxingScheme,
                    //CurrencyId = vendor.CurrencyId,
                    //TaxingSchemeId = vendor.TaxingSchemeId,
                    //DefaultPaymentTermsId = vendor.DefaultPaymentTermsId
                };
                getAllVendors.Add(objVendor);
            }

            return getAllVendors;
        }

        public int saveNewVendorDetails(VendorVM VendorModel)
        {
            int vendorID = 0;
            try
            {
                BASE_Vendor objVendor = new BASE_Vendor()
                {
                    Name = VendorModel.Name,
                    Balance = VendorModel.Balance,
                    Credit = VendorModel.Credit,
                    Address1 = VendorModel.Address1,
                    ContactName = VendorModel.ContactName,
                    Phone = VendorModel.Phone,
                    Fax = VendorModel.Fax,
                    Email = VendorModel.Email,
                    Website = VendorModel.Website,
                    Discount = VendorModel.Discount,
                    Remarks = VendorModel.Remarks,
                    PaymentTerms = VendorModel.PaymentTerms,
                    Currency = VendorModel.Currency,
                    TaxingScheme = VendorModel.TaxingScheme,
                    //CurrencyId=VendorModel.CurrencyId,
                    //TaxingSchemeId=VendorModel.TaxingSchemeId,
                    //DefaultPaymentTermsId=VendorModel.DefaultPaymentTermsId
                };
                if (VendorModel.VendorId > 0)
                {
                    objVendor.VendorId = VendorModel.VendorId;
                    _IVendor.Edit(objVendor);
                }
                else
                {
                    _IVendor.Add(objVendor);
                }
                vendorID = objVendor.VendorId;

            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in vendor manager saveNewVendorDetails", ex);
            }
            return vendorID;
        }

        public VendorVM getVendorByID(int venderID)
        {
            BASE_Vendor vendor = _IVendor.FindBy(x => x.VendorId == venderID).FirstOrDefault();
            VendorVM objVendor = new VendorVM
            {
                Name = vendor.Name,
                Balance = vendor.Balance,
                Credit = vendor.Credit,
                Address1 = vendor.Address1,
                ContactName = vendor.ContactName,
                Phone = vendor.Phone,
                Fax = vendor.Fax,
                Email = vendor.Email,
                Website = vendor.Website,
                Discount = vendor.Discount,
                Remarks = vendor.Remarks,
                PaymentTerms = vendor.PaymentTerms,
                Currency = vendor.Currency,
                TaxingScheme = vendor.TaxingScheme,
                VendorId = venderID
            };
            return objVendor;
        }

        /// <summary>
        /// Use for get the Currency 
        /// </summary>
        public List<CurrencyVM> getAllCurrency()
        {
            List<CurrencyVM> getAllCurrency = new List<CurrencyVM>();
            List<GLOBAL_Currency> objCurrencyList = _IGlobalCurrency.GetAll().ToList();
            foreach (var currency in objCurrencyList)
            {
                CurrencyVM objCurrencyVM = new CurrencyVM
                {
                    CurrencyId = currency.CurrencyId,
                    Description = currency.Description
                };
                getAllCurrency.Add(objCurrencyVM);
            }
            return getAllCurrency;

        }
        /// <summary>
        /// Use for get the Payment Terms 
        /// </summary>
        public List<VendorPaymentTermVM> getAllPaymentTerms()
        {
            List<BASE_PaymentTerms> objPaymentTermList = _IPaymentTerms.GetAll().ToList();
            List<VendorPaymentTermVM> getAllPaymentTerms = new List<VendorPaymentTermVM>();
            foreach (var paymentTerm in objPaymentTermList)
            {
                VendorPaymentTermVM objVendorPaymentTermVM = new VendorPaymentTermVM
                {
                    Name = paymentTerm.Name,
                    PaymentTermsId = paymentTerm.PaymentTermsId
                };
                getAllPaymentTerms.Add(objVendorPaymentTermVM);
            }
            return getAllPaymentTerms;
        }
        /// <summary>
        /// Use for get the Taxing Scheme 
        /// </summary>
        public List<TaxingSchemeVM> getAllTaxing()
        {
            List<BASE_TaxingScheme> objTaxingList = _ITaxingScheme.GetAll().ToList();
            List<TaxingSchemeVM> getAllTaxing = new List<TaxingSchemeVM>();
            foreach (var tax in objTaxingList)
            {
                TaxingSchemeVM objTaxingSchemeVM = new TaxingSchemeVM
                {
                    TaxingSchemeId = tax.TaxingSchemeId,
                    Name = tax.Name

                };
                getAllTaxing.Add(objTaxingSchemeVM);
            }
            return getAllTaxing;
        }
        /// <summary>
        /// Use for get the Order History 
        /// </summary>
        public List<PurchaseOrderVM> getVendorOrderStatus(int vendorID)
        {

            List<PurchaseOrderVM> getVendorOrderStatus = new List<PurchaseOrderVM>();
            List<PO_PurchaseOrder> objSalesOrderList = _IPurchaseOrder.FindBy(x => x.VendorId == vendorID).ToList();

            foreach (var order in objSalesOrderList)
            {
                PurchaseOrderVM objOrder = new PurchaseOrderVM
                {
                    PurchaseOrderId = order.PurchaseOrderId,            ///////17052017(D)////////   
                    VendorId = order.VendorId,                      ///////17052017(D)////////   
                    OrderNumber = order.OrderNumber,
                    OrderDate = order.OrderDate,
                    DueDate = order.DueDate,    /////
                    PaymentStatus = order.PaymentStatus,
                    OrderTotal = order.OrderTotal,                       ///////17052017(D)////////   
                    //Total = order.Total,
                    AmountPaid = order.AmountPaid,
                    Balance = order.Balance,
                };
                string InventoryStatusName = "";
                string PaymentStatusName = "";
                if (order.InventoryStatus!=null)
                    InventoryStatusName = _IInventoryStatus.FindBy(x => x.InventoryStatusId == order.InventoryStatus).Select(x => x.Status).FirstOrDefault() ?? "";
                if (order.PaymentStatus != null)
                    PaymentStatusName = _IPaymentStatus.FindBy(x => x.PaymentStatusId == order.PaymentStatus).Select(x => x.Status).FirstOrDefault() ?? "";
                if (!string.IsNullOrEmpty(PaymentStatusName) && !string.IsNullOrEmpty(InventoryStatusName))
                    objOrder.InventoryPaymentStatusName = PaymentStatusName + "," + InventoryStatusName;
                getVendorOrderStatus.Add(objOrder);
            }

            return getVendorOrderStatus;
        }


        #endregion

        #region Vendor Products
        /// <summary>
        /// Get vendor product item by using join 
        /// </summary>
        public List<VendorProductAndItemVM> getAllVendorProductItem(int vendorID)
        {
            List<VendorProductAndItemVM> ProductItemList = new List<VendorProductAndItemVM>();
            List<BASE_Product> listOfProducts = _IProduct.FindBy(x => x.LastVendorId == vendorID).ToList();
            List<BASE_VendorItem> listOfItems = _IVendorItem.FindBy(x => x.VendorId == vendorID).ToList();
            var getProductItemList = from product in listOfProducts
                                     join item in listOfItems
                                     on product.ProdId equals item.ProdId
                                     select new
                                     {
                                         product.Name,
                                         item.VendorItemCode,
                                         item.Cost,
                                         item.VendorId,
                                         item.ProdId,
                                         item.VendorItemId
                                     };
            foreach (var productItem in getProductItemList)
            {
                VendorProductAndItemVM objVendorProductItemVM = new VendorProductAndItemVM
                {
                    ProdId = productItem.ProdId,
                    Name = productItem.Name,
                    Cost = productItem.Cost,
                    VendorId = productItem.VendorId,
                    VendorItemId = productItem.VendorItemId,
                    VendorItemCode = productItem.VendorItemCode,
                };
                ProductItemList.Add(objVendorProductItemVM);

            }
            return ProductItemList;
        }


        /// <summary>
        /// update the vendor product item 
        /// </summary>
        public int SaveVendorProducts(VendorProductAndItemVM vendorProductModel)
        {
            BASE_Product objProduct = new BASE_Product();
            BASE_VendorItem objItem = new BASE_VendorItem();
            try
            {
                if (vendorProductModel.Cost != null && vendorProductModel.Name != null && vendorProductModel.VendorItemCode != null)
                {
                    if (vendorProductModel.ProdId > 0 && vendorProductModel.VendorItemId > 0)
                    {
                        objProduct = _IProduct.FindBy(x => x.LastVendorId == vendorProductModel.VendorId).FirstOrDefault();
                        objItem = _IVendorItem.FindBy(x => x.VendorId == vendorProductModel.VendorId).FirstOrDefault();
                        objProduct.Name = vendorProductModel.Name;
                        objItem.Cost = vendorProductModel.Cost;
                        objItem.VendorItemCode = vendorProductModel.VendorItemCode;
                        _IProduct.Edit(objProduct);
                        _IVendorItem.Edit(objItem);
                    }
                    else
                    {
                        objProduct.Name = vendorProductModel.Name;
                        objProduct.LastVendorId = vendorProductModel.VendorId;
                        _IProduct.Add(objProduct);
                        var productid = objProduct.ProdId;
                        objItem.Cost = vendorProductModel.Cost;
                        objItem.ProdId = productid;
                        objItem.VendorItemCode = vendorProductModel.VendorItemCode;
                        objItem.VendorId = vendorProductModel.VendorId;
                        _IVendorItem.Add(objItem);
                        //var itemid = objItem.VendorItemId;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in vendor manager SaveVendorProducts method ", ex);
            }
            return 1;
        }
        /// <summary>
        /// Unique name method of New Vendor 
        /// </summary>
        public int getExistVendorName(string VendorName)                 ///////17052017(D)////////
        {
            BASE_Vendor obj = new BASE_Vendor();
            obj.Name = _IVendor.FindBy(x => x.Name == VendorName).Select(x => x.Name).FirstOrDefault();   ///02062017////
            int checkUser = 0;
            if (obj.Name != null)
            {
                checkUser = 1;
            }
            else
            {
                checkUser = 0;
            }
            return checkUser;
        }

        /// <summary>
        /// Unique name method of Vendor Products on Item Name 
        /// </summary>
        public int getExistVendorItemName(string vendorItemName)                 ///////17052017(D)////////
        {
            BASE_Product obj = new BASE_Product();
            obj.Name = _IProduct.FindBy(x => x.Name == vendorItemName).Select(x => x.Name).FirstOrDefault();
            //BASE_Product obj = _IProduct.FindBy(x => x.Name == vendorItemName).FirstOrDefault();
            int checkUser = 0;
            if (obj.Name != null)
            {
                checkUser = 1;
            }
            else
            {
                checkUser = 0;
            }
            return checkUser;
        }


        /// <summary>
        /// Get current payment in payment history tab
        /// </summary>
        public PurchaseOrderVM getLatestPaymentRecord(int vendorId)
        {
            PurchaseOrderVM obj = new PurchaseOrderVM();
            PO_PurchaseOrder objBalance = new PO_PurchaseOrder();
            objBalance = _IPurchaseOrder.GetAll().Where(x => x.VendorId == vendorId).AsEnumerable().Last();
            obj.CurrentBalance = objBalance.Balance;

            var totalRecords = _IPurchaseOrder.GetAll().Where(x => x.VendorId == vendorId).ToList();
            obj.Balance = totalRecords.Sum(x => x.Balance);
            return obj;
        }

        /// <summary>
        /// Get  payment select by date in payment history tab
        /// </summary>
        public List<PurchaseOrderVM> getPaymentHistoryByDate(string StartDate, string EndDate, int VendorId)
        {
            //PO_PurchaseOrder objSalesOrderList = new PO_PurchaseOrder();
            var startDateArray = StartDate.Split('/');
            var EndDateArray = EndDate.Split('/');
            DateTime startdtime = Convert.ToDateTime(startDateArray[1] + "/" + startDateArray[0] + "/" + startDateArray[2]);
            DateTime enddtime = Convert.ToDateTime(EndDateArray[1] + "/" + EndDateArray[0] + "/" + EndDateArray[2]);

            List<PurchaseOrderVM> obj = new List<PurchaseOrderVM>();
            PO_PurchaseOrder objBalanceByDate = new PO_PurchaseOrder();
            try
            {
                obj = _IPurchaseOrder.GetAll().Where(x => x.OrderDate >= startdtime && x.DueDate <= enddtime && x.VendorId == VendorId).Select(x => new PurchaseOrderVM
                {
                    OrderDate = x.OrderDate,
                    DueDate = x.DueDate,
                    OrderNumber = x.OrderNumber,
                    AmountPaid = x.AmountPaid,
                    Balance = x.Balance
                }).ToList();

            }

            catch (Exception ex)
            {
                logger.ErrorException("Error occured in vendor manager getPaymentHistoryByDate", ex);
            }
            return obj;

        }
        #endregion

    }
}




