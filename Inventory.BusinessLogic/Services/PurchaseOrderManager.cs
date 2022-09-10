using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.BusinessLogic.Interface;
using Inventory.DomainModel.DatabaseModel;
using Inventory.Repositories.Interface;
using Inventory.CommonViewModels;
using NLog;

using Inventory.Repositories.Repository;


namespace Inventory.BusinessLogic.Services
{
    public class PurchaseOrderManager : IPurchaseOrderManager
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private IPurchaseOrder _IPurchaseOrder;
        private IPurchaseOrderLine _IPurchaseOrderLine;
        private IVendor _IVendor;
        private ILocation _ILocation;
        private IProduct _IProduct;
        private IPurchaseOrderReceiveLine _IPurchaseOrderReceiveLine;
        private IPurchaseOrderReturnLine _IPurchaseOrderReturnLine;
        private IPurchaseOrderUnstockLine _IPurchaseOrderUnstockLine;
         
        public PurchaseOrderManager(IPurchaseOrder IPurchaseOrder, IPurchaseOrderLine IPurchaseOrderLine, IVendor IVendor, ILocation ILocation,
            IProduct IProduct, IPurchaseOrderReceiveLine IPurchaseOrderReceiveLine, IPurchaseOrderReturnLine IPurchaseOrderReturnLine,
            IPurchaseOrderUnstockLine IPurchaseOrderUnstockLine )
        {
            this._IPurchaseOrder = IPurchaseOrder;
            this._IPurchaseOrderLine = IPurchaseOrderLine;
            this._IVendor = IVendor;
            this._ILocation = ILocation;
            this._IProduct = IProduct;
            this._IPurchaseOrderReceiveLine = IPurchaseOrderReceiveLine;
            this._IPurchaseOrderReturnLine = IPurchaseOrderReturnLine;
            this._IPurchaseOrderUnstockLine = IPurchaseOrderUnstockLine;
             
        }

        #region Add PurchaseOrder
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        public int addPurchaseOrder(PurchaseOrderVM purchaseOrder)
        {
            try
            {
                if (purchaseOrder != null)
                {

                    if (purchaseOrder.PurchaseOrderId > 0)
                    {
                        PO_PurchaseOrder PurchaseOrder = new PO_PurchaseOrder();

                        PurchaseOrder = _IPurchaseOrder.FindBy(x => x.PurchaseOrderId == purchaseOrder.PurchaseOrderId).FirstOrDefault();

                        PurchaseOrder.OrderNumber = purchaseOrder.OrderNumber;
                        PurchaseOrder.VendorOrderNumber = purchaseOrder.VendorOrderNumber;
                        PurchaseOrder.OrderDate = purchaseOrder.OrderDate;
                        PurchaseOrder.ContactName = purchaseOrder.ContactName;
                        PurchaseOrder.Phone = purchaseOrder.Phone;
                        PurchaseOrder.LocationId = purchaseOrder.LocationId;
                        PurchaseOrder.PaymentTermsId = purchaseOrder.PaymentTermsId;
                        PurchaseOrder.ShipToAddress1 = purchaseOrder.ShipToAddress1;
                        PurchaseOrder.VendorId = purchaseOrder.VendorId;
                        PurchaseOrder.VendorAddress1 = purchaseOrder.VendorAddress1;
                        PurchaseOrder.Carrier = purchaseOrder.Carrier;
                        PurchaseOrder.DueDate = purchaseOrder.DueDate;
                        PurchaseOrder.TaxingSchemeId = purchaseOrder.TaxingSchemeId;
                        PurchaseOrder.CurrencyId = purchaseOrder.CurrencyId;
                        PurchaseOrder.RequestShipDate = purchaseOrder.RequestShipDate;
                        PurchaseOrder.OrderRemarks = purchaseOrder.OrderRemarks;
                        PurchaseOrder.Freight = purchaseOrder.Freight;
                        PurchaseOrder.Total = purchaseOrder.Total;
                        PurchaseOrder.AmountPaid = purchaseOrder.AmountPaid;
                        PurchaseOrder.Balance = purchaseOrder.Balance;
                        PurchaseOrder.OrderSubTotal = purchaseOrder.OrderSubTotal;
                        PurchaseOrder.OrderTotal = purchaseOrder.OrderTotal;
                        PurchaseOrder.ReceiveRemarks = purchaseOrder.ReceiveRemarks;
                        PurchaseOrder.ReturnRemarks = purchaseOrder.ReturnRemarks;
                        PurchaseOrder.UnstockRemarks = purchaseOrder.UnstockRemarks;
                        PurchaseOrder.ReturnSubTotal = purchaseOrder.ReturnSubTotal;
                        PurchaseOrder.ReturnTotal = purchaseOrder.ReturnTotal;

                        _IPurchaseOrder.Edit(PurchaseOrder);
                        int GetId = PurchaseOrder.PurchaseOrderId;
                        return GetId;
                    }

                    else
                    {

                        string lastOrderNumber = _IPurchaseOrder.GetAll().OrderByDescending(x => x.PurchaseOrderId).Select(x => x.OrderNumber).FirstOrDefault();

                        if (string.IsNullOrEmpty(lastOrderNumber))
                        {
                            purchaseOrder.OrderNumber = "PO-000001";

                        }
                        else
                        {

                            int OrderNumber = Convert.ToInt32(lastOrderNumber.Substring(lastOrderNumber.LastIndexOf('-') + 1)) + 1;
                            string newOrderNumber = "PO-" + String.Format("{0:D6}", OrderNumber);
                            purchaseOrder.OrderNumber = newOrderNumber;
                        }

                        PO_PurchaseOrder PurchaseOrder = new PO_PurchaseOrder
                        {

                            OrderNumber = purchaseOrder.OrderNumber,
                            VendorOrderNumber = purchaseOrder.VendorOrderNumber,
                            OrderDate = purchaseOrder.OrderDate,
                            ContactName = purchaseOrder.ContactName,
                            Phone = purchaseOrder.Phone,
                            LocationId = purchaseOrder.LocationId,
                            PaymentTermsId = purchaseOrder.PaymentTermsId,
                            ShipToAddress1 = purchaseOrder.ShipToAddress1,
                            VendorId = purchaseOrder.VendorId,
                            VendorAddress1 = purchaseOrder.VendorAddress1,
                            Carrier = purchaseOrder.Carrier,
                            DueDate = purchaseOrder.DueDate,
                            TaxingSchemeId = purchaseOrder.TaxingSchemeId,
                            CurrencyId = purchaseOrder.CurrencyId,
                            RequestShipDate = purchaseOrder.RequestShipDate,
                            OrderRemarks = purchaseOrder.OrderRemarks,
                            Freight = purchaseOrder.Freight,
                            Total = purchaseOrder.Total,
                            AmountPaid = purchaseOrder.AmountPaid,
                            Balance = purchaseOrder.Balance,
                            OrderSubTotal = purchaseOrder.OrderSubTotal,
                            OrderTotal = purchaseOrder.OrderTotal,
                            ReceiveRemarks=purchaseOrder.ReceiveRemarks,
                            ReturnRemarks = purchaseOrder.ReturnRemarks,
                            UnstockRemarks = purchaseOrder.UnstockRemarks,
                            ReturnSubTotal = purchaseOrder.ReturnSubTotal,
                            ReturnTotal = purchaseOrder.ReturnTotal,
                        };
                        _IPurchaseOrder.Add(PurchaseOrder);
                        int GetId = PurchaseOrder.PurchaseOrderId;
                        return GetId;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = Convert.ToString(ex);
                logger.ErrorException("Error occured in addPurchaseOrder method in PurchaseOrderManager" + res, ex);

            }
            int value = 0;
            return value;

        }
        #endregion

        #region  Add PurchaseOrder Items List
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VendorProductItemModel"></param>
        /// <returns></returns>

        public int addPurchaseOrderItem(List<PurchaseOrderLineVM> PurchaseOrderLineModel)
        {

            PO_PurchaseOrder_Line PurchaseOrderLine = new PO_PurchaseOrder_Line();

            PO_PurchaseOrderReceive_Line purchaseOrderReceiveLine = new PO_PurchaseOrderReceive_Line();
            try
            {
                if (PurchaseOrderLineModel != null)
                {

                    foreach (var item in PurchaseOrderLineModel)
                    {
                        if (item.PurchaseOrderLineId > 0)
                        {
                            PurchaseOrderLine = _IPurchaseOrderLine.FindBy(x => x.PurchaseOrderLineId == item.PurchaseOrderLineId).FirstOrDefault();

                            purchaseOrderReceiveLine = _IPurchaseOrderReceiveLine                              
                                .FindBy(x => x.PurchaseOrderId == item.PurchaseOrderId)
                                .Where(x => x.PurchaseOrderLineId == item.PurchaseOrderLineId)
                                .FirstOrDefault();

                            if (purchaseOrderReceiveLine != null)
                            {
                                if (purchaseOrderReceiveLine.VendorItemCode == item.VendorItemCode)
                                {
                                    item.OrderStatus = true;
                                     
                                }

                                else if (purchaseOrderReceiveLine.VendorItemCode != item.VendorItemCode)
                                {
                                    purchaseOrderReceiveLine.VendorItemCode = item.VendorItemCode;
                                    item.OrderStatus = true;
                                    _IPurchaseOrderReceiveLine.Edit(purchaseOrderReceiveLine);
                                }
                                 
                            }


                            

                            PurchaseOrderLine.VendorItemCode = item.VendorItemCode;
                            PurchaseOrderLine.SubTotal = item.SubTotal;
                            PurchaseOrderLine.ProdId = item.ProdId;
                            PurchaseOrderLine.Discount = item.Discount;
                            PurchaseOrderLine.Quantity = item.Quantity;
                            PurchaseOrderLine.UnitPrice = item.UnitPrice;                          
                            PurchaseOrderLine.OrderStatus = item.OrderStatus;  
                            _IPurchaseOrderLine.Edit(PurchaseOrderLine);


                        }
                        else
                        {
                            PurchaseOrderLine.VendorItemCode = item.VendorItemCode;
                            PurchaseOrderLine.SubTotal = item.SubTotal;
                            PurchaseOrderLine.ProdId = item.ProdId;
                            PurchaseOrderLine.Discount = item.Discount;
                            PurchaseOrderLine.Quantity = item.Quantity;
                            PurchaseOrderLine.UnitPrice = item.UnitPrice;
                            PurchaseOrderLine.PurchaseOrderId = item.PurchaseOrderId;
                            PurchaseOrderLine.OrderStatus = false;
                            PurchaseOrderLine.PurchaseOrderReceiveLineId = 0;
                            _IPurchaseOrderLine.Add(PurchaseOrderLine);


                        }


                    }

                }
                int PurchaseOrderId = PurchaseOrderLine.PurchaseOrderId;
                return PurchaseOrderId;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in addPurchaseOrderItem method in PurchaseOrderManager", ex);
                int PurchaseOrderId = PurchaseOrderLine.PurchaseOrderId;
                return PurchaseOrderId;
            }
           
        }

        #endregion


        #region  Add PurchaseOrder Receive Items List
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VendorProductItemModel"></param>
        /// <returns></returns>

        public int addPurchaseOrderReceiveItem(List<PurchaseOrderReceiveLineVM> PurchaseOrderReceiveLineModel)
        {

            PO_PurchaseOrderReceive_Line purchaseOrderReceiveLine = new PO_PurchaseOrderReceive_Line();

            PO_PurchaseOrder_Line purchaseOrderLine = new PO_PurchaseOrder_Line();

            List<PO_PurchaseOrderReceive_Line> purchaseOrderReceiveList = new List<PO_PurchaseOrderReceive_Line>();

            List<PO_PurchaseOrder_Line> purchaseOrderLineList = new List<PO_PurchaseOrder_Line>();
            try
            {
                if (PurchaseOrderReceiveLineModel != null)
                {

                    foreach (var item in PurchaseOrderReceiveLineModel)
                    {
                        if (item.PurchaseOrderReceiveLineId > 0)
                        {

                            purchaseOrderLine = _IPurchaseOrderLine.FindBy(x=>x.PurchaseOrderId ==item.PurchaseOrderId)
                                .Where(x => x.PurchaseOrderReceiveLineId == item.PurchaseOrderReceiveLineId).Where(x => x.OrderStatus == true)
                                .FirstOrDefault();

                            if (purchaseOrderLine != null)
                            {
                                  if (purchaseOrderLine.VendorItemCode == item.VendorItemCode)
                                   {
                                       purchaseOrderLine.OrderStatus = true;
                                       
                                       _IPurchaseOrderLine.Edit(purchaseOrderLine);
                                   }
                                  else if (purchaseOrderLine.VendorItemCode != item.VendorItemCode)
                                   {
                                       purchaseOrderLine.VendorItemCode = item.VendorItemCode;
                                      
                                       _IPurchaseOrderLine.Edit(purchaseOrderLine);
                                   }
                            }
                             
                            purchaseOrderReceiveLine = _IPurchaseOrderReceiveLine.FindBy(x => x.PurchaseOrderReceiveLineId == item.PurchaseOrderReceiveLineId).FirstOrDefault();

                            purchaseOrderReceiveLine.VendorItemCode = item.VendorItemCode;
                            purchaseOrderReceiveLine.ProdId = item.ProdId;
                            purchaseOrderReceiveLine.LocationId = item.LocationId;
                            purchaseOrderReceiveLine.Quantity = item.Quantity;
                            purchaseOrderReceiveLine.ReceiveDate = item.ReceiveDate;
                            purchaseOrderReceiveLine.PurchaseOrderId = item.PurchaseOrderId;
                            //purchaseOrderReceiveLine.PurchaseOrderLineId = purchaseOrderLine.PurchaseOrderLineId;
                            _IPurchaseOrderReceiveLine.Edit(purchaseOrderReceiveLine);
  
                        }
                        else
                        {

                            //purchaseOrderReceiveLine.VendorItemCode = item.VendorItemCode;
                            //purchaseOrderReceiveLine.ProdId = item.ProdId;
                            //purchaseOrderReceiveLine.LocationId = item.LocationId;
                            //purchaseOrderReceiveLine.Quantity = item.Quantity;
                            //purchaseOrderReceiveLine.ReceiveDate = item.ReceiveDate;
                            //purchaseOrderReceiveLine.PurchaseOrderId = item.PurchaseOrderId;

                            //_IPurchaseOrderReceiveLine.Add(purchaseOrderReceiveLine);


                        }


                    }

                }
                int PurchaseOrderId = purchaseOrderReceiveLine.PurchaseOrderId;
                return PurchaseOrderId;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in addPurchaseOrderReceiveItem method in PurchaseOrderManager", ex);
                int PurchaseOrderId = purchaseOrderReceiveLine.PurchaseOrderId;
                return PurchaseOrderId;
            }
            
        }

        #endregion

        #region  Add Purchase Order Receive AutoFill Item

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VendorProductItemModel"></param>
        /// <returns></returns>

        public int addPOReceiveAutoFillItem(PurchaseOrderReceiveLineVM PurchaseOrderReceiveLineModel)
        {

            List<PO_PurchaseOrderReceive_Line> purchaseOrderReceiveLine = new List<PO_PurchaseOrderReceive_Line>();

            PO_PurchaseOrderReceive_Line objPurchaseOrderReceive = new PO_PurchaseOrderReceive_Line();

          
             
            try
            {
                if (PurchaseOrderReceiveLineModel != null)
                {
                    var deleteVar = true;

                    if (PurchaseOrderReceiveLineModel.PurchaseOrderId > 0)
                    {

                        List<PO_PurchaseOrder_Line> purchaseOrderItem = _IPurchaseOrderLine.GetAll().Where(x => x.PurchaseOrderId == PurchaseOrderReceiveLineModel.PurchaseOrderId).ToList();
                        if (purchaseOrderItem.Count > 0)
                        { 
                            if (deleteVar == true)
                            {
                                purchaseOrderReceiveLine = _IPurchaseOrderReceiveLine.FindBy(x => x.PurchaseOrderId == PurchaseOrderReceiveLineModel.PurchaseOrderId).ToList();

                                if (purchaseOrderReceiveLine.Count > 0)
                                {
                                    foreach (var receiveitem in purchaseOrderReceiveLine)
                                    {
                                        _IPurchaseOrderReceiveLine.Delete(receiveitem);
                                    }
                                }
                                deleteVar = false;
                            }
                           
                            foreach (var item in purchaseOrderItem)
                            {
                                PO_PurchaseOrderReceive_Line objpurchaseOrderReceiveLine = new PO_PurchaseOrderReceive_Line
                                {
                                    PurchaseOrderId = PurchaseOrderReceiveLineModel.PurchaseOrderId,
                                    LocationId = PurchaseOrderReceiveLineModel.LocationId,
                                    ReceiveDate = PurchaseOrderReceiveLineModel.ReceiveDate,
                                    ProdId = item.ProdId,
                                    VendorItemCode = item.VendorItemCode,
                                    Quantity = item.Quantity,
                                    PurchaseOrderLineId = item.PurchaseOrderLineId,
                                };
                                _IPurchaseOrderReceiveLine.Add(objpurchaseOrderReceiveLine);


                                int? test =objpurchaseOrderReceiveLine.PurchaseOrderLineId;

                                if(test > 0)
                                {
                                        PO_PurchaseOrder_Line objPurchaseOrderItemLine = new PO_PurchaseOrder_Line();

                                  objPurchaseOrderItemLine =  _IPurchaseOrderLine.FindBy(x => x.PurchaseOrderLineId == test).FirstOrDefault();

                                  if (objPurchaseOrderItemLine != null)
                                    {
                                  objPurchaseOrderItemLine.OrderStatus = true ;                        
                                  objPurchaseOrderItemLine.PurchaseOrderReceiveLineId = objpurchaseOrderReceiveLine.PurchaseOrderReceiveLineId;
                                 _IPurchaseOrderLine.Edit(objPurchaseOrderItemLine);
                                    }
                             

                                  
                                }
                            
                            }
                             
                        }
                    }

                }
                int PurchaseOrderId = objPurchaseOrderReceive.PurchaseOrderId;
                return PurchaseOrderId;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in addPOReceiveAutoFillItem method in PurchaseOrderManager", ex);
                int PurchaseOrderId = objPurchaseOrderReceive.PurchaseOrderId;
                return PurchaseOrderId;
            }
           
        }

        #endregion

        #region  Add PurchaseOrder Return Items List
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VendorProductItemModel"></param>
        /// <returns></returns>

        public int addPurchaseOrderReturnItem(List<PurchaseOrderReturnVM> PurchaseOrderReturnLineModel)
        {

            PO_PurchaseOrderReturn_Line purchaseOrderReturnLine = new PO_PurchaseOrderReturn_Line();

            try
            {
                if (PurchaseOrderReturnLineModel != null)
                {

                    foreach (var item in PurchaseOrderReturnLineModel)
                    {
                        if (item.PurchaseOrderReturnLineId > 0)
                        {
                            purchaseOrderReturnLine = _IPurchaseOrderReturnLine.FindBy(x => x.PurchaseOrderReturnLineId == item.PurchaseOrderReturnLineId).FirstOrDefault();

                            purchaseOrderReturnLine.VendorItemCode = item.VendorItemCode;
                            purchaseOrderReturnLine.ProdId = item.ProdId;
                            purchaseOrderReturnLine.Discount = item.Discount;
                            purchaseOrderReturnLine.Quantity = item.Quantity;
                            purchaseOrderReturnLine.UnitPrice = item.UnitPrice;
                            purchaseOrderReturnLine.SubTotal = item.SubTotal;
                            purchaseOrderReturnLine.ReturnDate = item.ReturnDate;
                            purchaseOrderReturnLine.PurchaseOrderId = item.PurchaseOrderId;

                            _IPurchaseOrderReturnLine.Edit(purchaseOrderReturnLine);


                        }
                        else
                        {
                            purchaseOrderReturnLine.VendorItemCode = item.VendorItemCode;
                            purchaseOrderReturnLine.ProdId = item.ProdId;
                            purchaseOrderReturnLine.Discount = item.Discount;
                            purchaseOrderReturnLine.Quantity = item.Quantity;
                            purchaseOrderReturnLine.UnitPrice = item.UnitPrice;
                            purchaseOrderReturnLine.SubTotal = item.SubTotal;
                            purchaseOrderReturnLine.ReturnDate = item.ReturnDate;
                            purchaseOrderReturnLine.PurchaseOrderId = item.PurchaseOrderId;



                            _IPurchaseOrderReturnLine.Add(purchaseOrderReturnLine);


                        }


                    }

                }
                int PurchaseOrderId = purchaseOrderReturnLine.PurchaseOrderId;
                return PurchaseOrderId;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in addPurchaseOrderReturnItem method in PurchaseOrderManager", ex);
                int PurchaseOrderId = purchaseOrderReturnLine.PurchaseOrderId;
                return PurchaseOrderId;
            }
           
        }

        #endregion

        #region  Add PurchaseOrder Unstock Items List
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VendorProductItemModel"></param>
        /// <returns></returns>

        public int addPurchaseOrderUnstockItem(List<PurchaseOrderUnstockVM> PurchaseOrderUnstockLineModel)
        {

            PO_PurchaseOrderUnstock_Line purchaseOrderUnstockLine = new PO_PurchaseOrderUnstock_Line();

            try
            {
                if (PurchaseOrderUnstockLineModel != null)
                {

                    foreach (var item in PurchaseOrderUnstockLineModel)
                    {
                        if (item.PurchaseOrderUnstockLineId > 0)
                        {
                            purchaseOrderUnstockLine = _IPurchaseOrderUnstockLine.FindBy(x => x.PurchaseOrderUnstockLineId == item.PurchaseOrderUnstockLineId).FirstOrDefault();

                            // purchaseOrderUnstockLine.VendorItemCode = item.VendorItemCode;
                            purchaseOrderUnstockLine.Quantity = item.Quantity;
                            purchaseOrderUnstockLine.ProdId = item.ProdId;
                            purchaseOrderUnstockLine.LocationId = item.LocationId;
                            purchaseOrderUnstockLine.UnstockDate = item.UnstockDate;
                            purchaseOrderUnstockLine.PurchaseOrderId = item.PurchaseOrderId;

                            _IPurchaseOrderUnstockLine.Edit(purchaseOrderUnstockLine);

                        }
                        else
                        {
                            // purchaseOrderUnstockLine.VendorItemCode = item.VendorItemCode;
                            purchaseOrderUnstockLine.Quantity = item.Quantity;
                            purchaseOrderUnstockLine.ProdId = item.ProdId;
                            purchaseOrderUnstockLine.LocationId = item.LocationId;
                            purchaseOrderUnstockLine.UnstockDate = item.UnstockDate;
                            purchaseOrderUnstockLine.PurchaseOrderId = item.PurchaseOrderId;

                            _IPurchaseOrderUnstockLine.Add(purchaseOrderUnstockLine);


                        }


                    }
                  
                }
                int PurchaseOrderId = purchaseOrderUnstockLine.PurchaseOrderId;
                return PurchaseOrderId;
            }

          
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in addPurchaseOrderUnstockItem method in PurchaseOrderManager", ex);
                int PurchaseOrderId = purchaseOrderUnstockLine.PurchaseOrderId;
                return PurchaseOrderId;
            }
           
        }

        #endregion

        #region Get PurchaseOrder List For Show Grid List
        /// <summary>
        /// This fields Use for Get PurchaseOrder List For Show Grid List
        /// </summary>
        /// <returns></returns>
        public List<ListPurchaseOrderVM> getAllPurchaseOrder()
        {
            List<ListPurchaseOrderVM> PurchaseOrderListVM = new List<ListPurchaseOrderVM>();
            try
            {
               
                List<PO_PurchaseOrder> purchaseOrderList = _IPurchaseOrder.GetAll().ToList();
                List<BASE_Vendor> vendorList = _IVendor.GetAll().ToList();
                List<BASE_Location> LocationList = _ILocation.GetAll().ToList();

                //GetPOList = _IPurchaseOrder.GetAll().ToList();

                var PurchaseOrderList = from PO in purchaseOrderList
                                        join Vendors in vendorList on
                                         PO.VendorId equals Vendors.VendorId
                                        join Locations in LocationList on
                                         PO.LocationId equals Locations.LocationId
                                         into Loc
                                        from subLocation in Loc.DefaultIfEmpty()
                                        select new
                                        {
                                            PO.AmountPaid,
                                            PO.DueDate,
                                            PO.Balance,
                                            PO.InventoryStatus,
                                            LocationName = (subLocation == null ? String.Empty : subLocation.Name),
                                            PO.OrderDate,
                                            PO.OrderNumber,
                                            PO.PaymentStatus,
                                            PO.PurchaseOrderId,
                                            PO.RequestShipDate,
                                            PO.OrderTotal,
                                            VendorName = Vendors.Name,
                                            PO.VendorOrderNumber,
                                            PO.VendorId

                                        };
                foreach (var PurchaseOrder in PurchaseOrderList)
                {
                    ListPurchaseOrderVM objListPurchaseOrderVM = new ListPurchaseOrderVM
                    {
                        AmountPaid = PurchaseOrder.AmountPaid,
                        DueDate = PurchaseOrder.DueDate,
                        Balance = PurchaseOrder.Balance,
                        InventoryStatus = PurchaseOrder.InventoryStatus,
                        LocationName = PurchaseOrder.LocationName,
                        OrderDate = PurchaseOrder.OrderDate,
                        OrderNumber = PurchaseOrder.OrderNumber,
                        PaymentStatus = PurchaseOrder.PaymentStatus,
                        PurchaseOrderId = PurchaseOrder.PurchaseOrderId,
                        RequestShipDate = PurchaseOrder.RequestShipDate,
                        OrderTotal = PurchaseOrder.OrderTotal,
                        VendorName = PurchaseOrder.VendorName,
                        VendorOrderNumber = PurchaseOrder.VendorOrderNumber,
                        VendorId = PurchaseOrder.VendorId
                    };
                    PurchaseOrderListVM.Add(objListPurchaseOrderVM);
                }
                return PurchaseOrderListVM;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in getAllPurchaseOrder method in PurchaseOrderManager", ex);

                return PurchaseOrderListVM;
            }
            
        }

        #endregion

        #region Get PurchaseOrder Detail For Update
        /// <summary>
        /// This is Purchase Order Details By OrderNumber 
        /// </summary>
        /// <returns></returns>
        public PurchaseOrderVM getPurchaseOrderByID(string OrderNumber)
        {

            PO_PurchaseOrder GetPODetails = new PO_PurchaseOrder();
            PurchaseOrderVM purchaseOrder = new PurchaseOrderVM();
            try
            {
                GetPODetails = _IPurchaseOrder.FindBy(x => x.OrderNumber == OrderNumber).FirstOrDefault(); ;

                if (GetPODetails != null)
                {
                    
                        purchaseOrder.PurchaseOrderId = GetPODetails.PurchaseOrderId;
                        purchaseOrder.VendorId = GetPODetails.VendorId;
                        purchaseOrder.ContactName = GetPODetails.ContactName;
                        purchaseOrder.Phone = GetPODetails.Phone;
                        purchaseOrder.VendorAddress1 = GetPODetails.VendorAddress1;
                        purchaseOrder.PaymentTermsId = GetPODetails.PaymentTermsId;
                        purchaseOrder.VendorOrderNumber = GetPODetails.VendorOrderNumber;
                        purchaseOrder.LocationId = GetPODetails.LocationId;
                        purchaseOrder.OrderNumber = GetPODetails.OrderNumber;
                        purchaseOrder.OrderDate = GetPODetails.OrderDate;
                        purchaseOrder.ShipToAddress1 = GetPODetails.ShipToAddress1;
                        purchaseOrder.Carrier = GetPODetails.Carrier;
                        purchaseOrder.DueDate = GetPODetails.DueDate;
                        purchaseOrder.TaxingSchemeId = GetPODetails.TaxingSchemeId;
                        purchaseOrder.CurrencyId = GetPODetails.CurrencyId;
                        purchaseOrder.RequestShipDate = GetPODetails.RequestShipDate;
                        purchaseOrder.OrderRemarks = GetPODetails.OrderRemarks;
                        purchaseOrder.OrderSubTotal = GetPODetails.OrderSubTotal;
                        purchaseOrder.Freight = GetPODetails.Freight;
                        purchaseOrder.Total = GetPODetails.Total;
                        purchaseOrder.AmountPaid = GetPODetails.AmountPaid;
                        purchaseOrder.Balance = GetPODetails.Balance;
                        purchaseOrder.OrderTotal = GetPODetails.OrderTotal;
                        purchaseOrder.ReceiveRemarks = GetPODetails.ReceiveRemarks;
                        purchaseOrder.ReturnRemarks = GetPODetails.ReturnRemarks;
                        purchaseOrder.UnstockRemarks = GetPODetails.UnstockRemarks;
                        purchaseOrder.ReturnSubTotal = GetPODetails.ReturnSubTotal;
                        purchaseOrder.ReturnTotal = GetPODetails.ReturnTotal;



                        
                }
                return purchaseOrder;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in getPurchaseOrderByID method in PurchaseOrderManager", ex);

                return purchaseOrder;
            }
            



            
        }
        #endregion


        #region Get all PurchaseOrder Item By OrderNumber
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public List<PurchaseOrderLineVM> getPurchaseOrderItemByOrderNumber(string OrderNumber)
        {
            List<PurchaseOrderLineVM> PurchaseOrderProductListVM = new List<PurchaseOrderLineVM>();
            try
            {
                 
                List<PO_PurchaseOrder> purchaseOrderList = _IPurchaseOrder.GetAll().ToList();
                List<PO_PurchaseOrder_Line> purchaseOrderLineList = _IPurchaseOrderLine.GetAll().ToList();
                List<BASE_Product> productList = _IProduct.GetAll().ToList();
               // List<PO_VendorItemCode> vendorItemList = _IVendorItemCode.GetAll().ToList();



                var PurchaseOrderProduct = from PO in purchaseOrderList
                                           join POL in purchaseOrderLineList on
                                            PO.PurchaseOrderId equals POL.PurchaseOrderId
                                           join BP in productList on POL.ProdId equals BP.ProdId

                                           where PO.OrderNumber == OrderNumber
                                           select new
                                           {
                                               POL.ProdId,
                                               POL.VendorItemCode,
                                               POL.Quantity,
                                               POL.UnitPrice,
                                               POL.Discount,
                                               POL.SubTotal,
                                               POL.PurchaseOrderLineId,
                                               POL.OrderStatus,

                                               productName = BP.Name

                                           };

                foreach (var OrderProduct in PurchaseOrderProduct)
                {
                    PurchaseOrderLineVM objPurchaseOrderProductVM = new PurchaseOrderLineVM
                    {
                        ProdId = OrderProduct.ProdId,
                        VendorItemCode = OrderProduct.VendorItemCode,
                        Quantity = OrderProduct.Quantity,
                        UnitPrice = OrderProduct.UnitPrice,
                        Discount = OrderProduct.Discount,
                        SubTotal = OrderProduct.SubTotal,
                        PurchaseOrderLineId = OrderProduct.PurchaseOrderLineId,
                        productName = OrderProduct.productName,
                        OrderStatus = OrderProduct.OrderStatus,

                    };
                    PurchaseOrderProductListVM.Add(objPurchaseOrderProductVM);
                }

                return PurchaseOrderProductListVM;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in getPurchaseOrderItemByOrderNumber method in PurchaseOrderManager", ex);

                return PurchaseOrderProductListVM;
            }
           

        }

        #endregion


        #region Get get PurchaseOrder Item By purchaseOrder Id
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public List<PurchaseOrderLineVM> getPurchaseOrderItemBypurchaseOrderId(int purchaseOrderId)
        {
            List<PurchaseOrderLineVM> PurchaseOrderProductListVM = new List<PurchaseOrderLineVM>();

            try
            { 
                List<PO_PurchaseOrder_Line> purchaseOrderLineList = _IPurchaseOrderLine.GetAll().ToList();
                var PurchaseOrderProduct = from POL in purchaseOrderLineList
                                           where POL.PurchaseOrderId == purchaseOrderId
                                           select new
                                           {
                                               POL.ProdId,
                                               POL.VendorItemCode,
                                               POL.Quantity,
                                               POL.UnitPrice,
                                               POL.Discount,
                                               POL.SubTotal,
                                               POL.PurchaseOrderLineId,


                                               POL.OrderStatus,


                                           };

                foreach (var OrderProduct in PurchaseOrderProduct)
                {
                    PurchaseOrderLineVM objPurchaseOrderProductVM = new PurchaseOrderLineVM
                    {

                        ProdId = OrderProduct.ProdId,
                        VendorItemCode = OrderProduct.VendorItemCode,
                        Quantity = OrderProduct.Quantity,
                        UnitPrice = OrderProduct.UnitPrice,
                        Discount = OrderProduct.Discount,
                        SubTotal = OrderProduct.SubTotal,
                        PurchaseOrderLineId = OrderProduct.PurchaseOrderLineId,

                        OrderStatus = OrderProduct.OrderStatus,



                    };
                    PurchaseOrderProductListVM.Add(objPurchaseOrderProductVM);
                }

                return PurchaseOrderProductListVM;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in getPurchaseOrderItemBypurchaseOrderId method in PurchaseOrderManager", ex);

                return PurchaseOrderProductListVM;
            }
          

        }

        #endregion
        #region Get all Receive PurchaseOrder Item By PurchaseOrderId
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public List<PurchaseOrderReceiveLineVM> getPurchaseOrderReceiveByPurchaseOrderId(int PurchaseOrderId)
        {
            List<PurchaseOrderReceiveLineVM> PurchaseOrderReceiveProductListVM = new List<PurchaseOrderReceiveLineVM>();
            try
            {            

                List<PO_PurchaseOrderReceive_Line> purchaseOrderReceiveLineList = _IPurchaseOrderReceiveLine.GetAll().ToList();
                // List<PO_PurchaseOrder_Line> purchaseOrderLineList = _IPurchaseOrderLine.GetAll().ToList();
                List<BASE_Product> productList = _IProduct.GetAll().ToList();
                List<BASE_Location> locationList = _ILocation.GetAll().ToList();
                //List<PO_VendorItemCode> vendorItemList = _IVendorItemCode.GetAll().ToList();


                var purchaseOrderReceiveProduct = from PORL in purchaseOrderReceiveLineList
                                                  join BP in productList on PORL.ProdId equals BP.ProdId
                                                  join BL in locationList on PORL.LocationId equals BL.LocationId

                                                  where PORL.PurchaseOrderId == PurchaseOrderId
                                                  select new
                                                  {
                                                      PORL.ProdId,
                                                      PORL.VendorItemCode,
                                                      PORL.Quantity,
                                                      PORL.LocationId,
                                                      PORL.ReceiveDate,
                                                      PORL.PurchaseOrderReceiveLineId,
                                                      productName = BP.Name,

                                                      locationName = BL.Name

                                                  };

                foreach (var receiveOrderProduct in purchaseOrderReceiveProduct)
                {
                    PurchaseOrderReceiveLineVM objPurchaseOrderReceiveProductVM = new PurchaseOrderReceiveLineVM
                    {
                        ProdId = receiveOrderProduct.ProdId,
                        VendorItemCode = receiveOrderProduct.VendorItemCode,
                        Quantity = receiveOrderProduct.Quantity,
                        ReceiveDate = receiveOrderProduct.ReceiveDate,
                        LocationId = receiveOrderProduct.LocationId,
                        PurchaseOrderReceiveLineId = receiveOrderProduct.PurchaseOrderReceiveLineId,
                        productName = receiveOrderProduct.productName,
                        LocationName = receiveOrderProduct.locationName,

                    };
                    PurchaseOrderReceiveProductListVM.Add(objPurchaseOrderReceiveProductVM);
                }

                return PurchaseOrderReceiveProductListVM;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in getPurchaseOrderReceiveByPurchaseOrderId method in PurchaseOrderManager", ex);

                return PurchaseOrderReceiveProductListVM;
            }
            

        }

        #endregion


        #region Get all Return PurchaseOrder Item By PurchaseOrderId
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns> 
        public List<PurchaseOrderReturnVM> getPurchaseOrderReturnByPurchaseOrderId(int PurchaseOrderId)
        {
            List<PurchaseOrderReturnVM> PurchaseOrderReturnProductListVM = new List<PurchaseOrderReturnVM>();
            try
            {
       

                List<PO_PurchaseOrderReturn_Line> purchaseOrderReturnLineList = _IPurchaseOrderReturnLine.GetAll().ToList();

                List<BASE_Product> productList = _IProduct.GetAll().ToList();



                var purchaseOrderReturnProduct = from PORL in purchaseOrderReturnLineList

                                                 join BP in productList on PORL.ProdId equals BP.ProdId
                                                 where PORL.PurchaseOrderId == PurchaseOrderId
                                                 select new
                                                 {
                                                     PORL.ProdId,
                                                     PORL.VendorItemCode,
                                                     PORL.Quantity,
                                                     PORL.UnitPrice,
                                                     PORL.Discount,
                                                     PORL.SubTotal,
                                                     PORL.ReturnDate,
                                                     PORL.PurchaseOrderReturnLineId,
                                                     productName = BP.Name

                                                 };

                foreach (var returnOrderProduct in purchaseOrderReturnProduct)
                {
                    PurchaseOrderReturnVM objPurchaseOrderReturnProductVM = new PurchaseOrderReturnVM
                    {
                        ProdId = returnOrderProduct.ProdId,
                        VendorItemCode = returnOrderProduct.VendorItemCode,
                        Quantity = returnOrderProduct.Quantity,
                        UnitPrice = returnOrderProduct.UnitPrice,
                        Discount = returnOrderProduct.Discount,
                        SubTotal = returnOrderProduct.SubTotal,
                        ReturnDate = returnOrderProduct.ReturnDate,
                        PurchaseOrderReturnLineId = returnOrderProduct.PurchaseOrderReturnLineId,
                        productName = returnOrderProduct.productName

                    };
                    PurchaseOrderReturnProductListVM.Add(objPurchaseOrderReturnProductVM);
                }

                return PurchaseOrderReturnProductListVM;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in getPurchaseOrderReturnByPurchaseOrderId method in PurchaseOrderManager", ex);

                return PurchaseOrderReturnProductListVM;
            }
            

        }

        #endregion


        #region  Get all Unstock PurchaseOrder Item By PurchaseOrderId
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns> 
        public List<PurchaseOrderUnstockVM> getPurchaseOrderUnstockByPurchaseOrderId(int PurchaseOrderId)
        {
            List<PurchaseOrderUnstockVM> PurchaseOrderUnstockProductListVM = new List<PurchaseOrderUnstockVM>();
            try
            {             

                List<PO_PurchaseOrderUnstock_Line> purchaseOrderUnstockLineList = _IPurchaseOrderUnstockLine.GetAll().ToList();

                List<BASE_Product> productList = _IProduct.GetAll().ToList();
                List<BASE_Location> locationList = _ILocation.GetAll().ToList();


                var purchaseOrderUnstockProduct = from POUSL in purchaseOrderUnstockLineList
                                                  join BP in productList on POUSL.ProdId equals BP.ProdId
                                                  join BL in locationList on POUSL.LocationId equals BL.LocationId
                                                  where POUSL.PurchaseOrderId == PurchaseOrderId
                                                  select new
                                                  {
                                                      POUSL.ProdId,
                                                      POUSL.VendorItemCode,
                                                      POUSL.Quantity,
                                                      POUSL.LocationId,
                                                      POUSL.UnstockDate,
                                                      POUSL.PurchaseOrderUnstockLineId,
                                                      productName = BP.Name,
                                                      locationName = BL.Name

                                                  };

                foreach (var unstockOrderProduct in purchaseOrderUnstockProduct)
                {
                    PurchaseOrderUnstockVM objPurchaseOrderUnstockProductVM = new PurchaseOrderUnstockVM
                    {
                        ProdId = unstockOrderProduct.ProdId,
                        VendorItemCode = unstockOrderProduct.VendorItemCode,
                        Quantity = unstockOrderProduct.Quantity,
                        LocationId = unstockOrderProduct.LocationId,
                        UnstockDate = unstockOrderProduct.UnstockDate,
                        PurchaseOrderUnstockLineId = unstockOrderProduct.PurchaseOrderUnstockLineId,
                        productName = unstockOrderProduct.productName,
                        LocationName = unstockOrderProduct.locationName

                    };
                    PurchaseOrderUnstockProductListVM.Add(objPurchaseOrderUnstockProductVM);
                }

                return PurchaseOrderUnstockProductListVM;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in getPurchaseOrderUnstockByPurchaseOrderId method in PurchaseOrderManager", ex);

                return PurchaseOrderUnstockProductListVM;
            }
            

        }

        #endregion

       

        #region  Received Data Row Remove
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns> 
        public int receivedDataRowRemove(PurchaseOrderReceiveLineVM purchaseOrderReceive)
        {

            PO_PurchaseOrderReceive_Line purchaseOrderReceiveLine = _IPurchaseOrderReceiveLine.FindBy(x => x.VendorItemCode == purchaseOrderReceive.VendorItemCode)
                .Where(x => x.PurchaseOrderLineId == purchaseOrderReceive.PurchaseOrderLineId).FirstOrDefault();
            try
            {
                if (purchaseOrderReceiveLine != null)
                {
                    _IPurchaseOrderReceiveLine.Delete(purchaseOrderReceiveLine);

                }

                return purchaseOrderReceiveLine.PurchaseOrderReceiveLineId;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in receivedDataRowRemove method in PurchaseOrderManager", ex);

                return purchaseOrderReceiveLine.PurchaseOrderReceiveLineId;
            }

        }

        #endregion



        #region  Add Received Ordered Product
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns> 
        public int addReceivedOrderedProduct(PurchaseOrderReceiveLineVM purchaseOrderReceive)
        {

            PO_PurchaseOrderReceive_Line purchaseOrderReceiveLine = new PO_PurchaseOrderReceive_Line();
            try
            {
                if (purchaseOrderReceiveLine != null)
                {
                    purchaseOrderReceiveLine.VendorItemCode = purchaseOrderReceive.VendorItemCode;
                    purchaseOrderReceiveLine.ProdId = purchaseOrderReceive.ProdId;
                    purchaseOrderReceiveLine.LocationId = purchaseOrderReceive.LocationId;
                    purchaseOrderReceiveLine.Quantity = purchaseOrderReceive.Quantity;
                    purchaseOrderReceiveLine.ReceiveDate = purchaseOrderReceive.ReceiveDate;
                    purchaseOrderReceiveLine.PurchaseOrderId = purchaseOrderReceive.PurchaseOrderId;
                    purchaseOrderReceiveLine.PurchaseOrderLineId = purchaseOrderReceive.PurchaseOrderLineId;
                    _IPurchaseOrderReceiveLine.Add(purchaseOrderReceiveLine);  

                }

                return purchaseOrderReceiveLine.PurchaseOrderReceiveLineId;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in addReceivedOrderedProduct method in PurchaseOrderManager", ex);

                return purchaseOrderReceiveLine.PurchaseOrderReceiveLineId;
            }

            
        }

        #endregion



        #region  Update Single Purchase Order Item
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns> 
        public int updateSinglePurchaseOrderItem(PurchaseOrderLineVM PurchaseOrderLineModel)
        {

            PO_PurchaseOrder_Line PurchaseOrderLine = new PO_PurchaseOrder_Line();
            try
            {
                if (PurchaseOrderLineModel.PurchaseOrderLineId > 0)
                {
                    PurchaseOrderLine = _IPurchaseOrderLine.FindBy(x => x.PurchaseOrderLineId == PurchaseOrderLineModel.PurchaseOrderLineId)
                        .Where(x => x.VendorItemCode == PurchaseOrderLineModel.VendorItemCode).Where(x => x.PurchaseOrderId == PurchaseOrderLineModel.PurchaseOrderId)
                        .FirstOrDefault();

                    PurchaseOrderLine.OrderStatus = PurchaseOrderLineModel.OrderStatus;
                    PurchaseOrderLine.PurchaseOrderReceiveLineId = PurchaseOrderLineModel.PurchaseOrderReceiveLineId;
                    _IPurchaseOrderLine.Edit(PurchaseOrderLine);


                }

                return PurchaseOrderLine.PurchaseOrderLineId;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in updateSinglePurchaseOrderItem method in PurchaseOrderManager", ex);

                return PurchaseOrderLine.PurchaseOrderLineId;
            }

            
        }

        #endregion

        
    }
}
