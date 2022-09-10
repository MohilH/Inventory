using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Inventory.BusinessLogic.Interface;
using Inventory.DomainModel.DatabaseModel;
using Inventory.CommonViewModels;

namespace Inventory.WebAPI.Controllers
{
   // [RoutePrefix("PurchaseOrder")]
    public class PurchaseOrderController : ApiController
    {
        public readonly IPurchaseOrderManager _IPurchaseOrderManager;
        public PurchaseOrderController()
        {

        }

        public PurchaseOrderController(IPurchaseOrderManager IPurchaseOrderManager)
        {
            this._IPurchaseOrderManager = IPurchaseOrderManager;
        }

        #region Add PurchaseOrde

        [Route("addPurchaseOrder")]
        [HttpPost]
        public int addPurchaseOrder(PurchaseOrderVM PurchaseOrder)
        {
            return _IPurchaseOrderManager.addPurchaseOrder(PurchaseOrder);
        }
        #endregion


        #region Get All PurchaseOrder
        [Route("getAllPurchaseOrder")]
        [HttpGet]
        public List<ListPurchaseOrderVM> getAllPurchaseOrder()
        {
            return _IPurchaseOrderManager.getAllPurchaseOrder();
        }
        #endregion


        #region Get PurchaseOrder By OrderNumber
        [Route("getPurchaseOrderByID")]
        [HttpGet]
        public PurchaseOrderVM getPurchaseOrderByID(string OrderNumber)
        {
            return _IPurchaseOrderManager.getPurchaseOrderByID(OrderNumber);
        }
        #endregion



        #region Add PurchaseOrder Items

        [Route("addPurchaseOrderItem")]
        public int addPurchaseOrderItem(List<PurchaseOrderLineVM> PurchaseOrderLineModel)
        {
            return _IPurchaseOrderManager.addPurchaseOrderItem(PurchaseOrderLineModel);
        } 

        #endregion


        #region Get All PurchaseOrder Item By OrderNumber

        [Route("getPurchaseOrderItemByOrderNumber")]
        public List<PurchaseOrderLineVM> getPurchaseOrderItemByOrderNumber(string OrderNumber)
        {
            return _IPurchaseOrderManager.getPurchaseOrderItemByOrderNumber(OrderNumber);
        }

        #endregion

        #region Get PurchaseOrder Item By purchaseOrder Id

        [Route("getPurchaseOrderItemBypurchaseOrderId")]
        public List<PurchaseOrderLineVM> getPurchaseOrderItemBypurchaseOrderId(int purchaseOrderId)
        {
            return _IPurchaseOrderManager.getPurchaseOrderItemBypurchaseOrderId(purchaseOrderId);
        }

        #endregion

        #region Add PurchaseOrder Receive Items    

        [Route("addPurchaseOrderReceiveItem")]
        public int addPurchaseOrderReceiveItem(List<PurchaseOrderReceiveLineVM> PurchaseOrderReceiveLineModel)
        {
            return _IPurchaseOrderManager.addPurchaseOrderReceiveItem(PurchaseOrderReceiveLineModel);
        } 

        #endregion


        #region Add PurchaseOrder Receive AutoFill Item

        [Route("addPOReceiveAutoFillItem")]
        public int addPOReceiveAutoFillItem(PurchaseOrderReceiveLineVM PurchaseOrderReceiveLineModel)
        {
            return _IPurchaseOrderManager.addPOReceiveAutoFillItem(PurchaseOrderReceiveLineModel);
        }

        #endregion

        #region Add PurchaseOrder Return Items

        [Route("addPurchaseOrderReturnItem")]
        public int addPurchaseOrderReturnItem(List<PurchaseOrderReturnVM> PurchaseOrderReturnLineModel)
        {
            return _IPurchaseOrderManager.addPurchaseOrderReturnItem(PurchaseOrderReturnLineModel);
        }

        #endregion

        #region Add PurchaseOrder Unstock Items

        [Route("addPurchaseOrderUnstockItem")]
        public int addPurchaseOrderUnstockItem(List<PurchaseOrderUnstockVM> PurchaseOrderUnstockLineModel)
        {
            return _IPurchaseOrderManager.addPurchaseOrderUnstockItem(PurchaseOrderUnstockLineModel);
        }

        #endregion

        #region Get PurchaseOrder Receive ItemsList By purchaseOrderId

        [Route("getPurchaseOrderReceiveByPurchaseOrderId")]
        public List<PurchaseOrderReceiveLineVM> getPurchaseOrderReceiveByPurchaseOrderId(int PurchaseOrderId)
        {
            return _IPurchaseOrderManager.getPurchaseOrderReceiveByPurchaseOrderId(PurchaseOrderId);
        }
        
        #endregion

        #region Get PurchaseOrder Return ItemsList By purchaseOrderId

        [Route("getPurchaseOrderReturnByPurchaseOrderId")]
        public List<PurchaseOrderReturnVM> getPurchaseOrderReturnByPurchaseOrderId(int PurchaseOrderId)
        {
            return _IPurchaseOrderManager.getPurchaseOrderReturnByPurchaseOrderId(PurchaseOrderId);
        }

        #endregion

        #region Get PurchaseOrder Unstock ItemsList By purchaseOrderId

        [Route("getPurchaseOrderUnstockByPurchaseOrderId")]
        public List<PurchaseOrderUnstockVM> getPurchaseOrderUnstockByPurchaseOrderId(int PurchaseOrderId)
        {
            return _IPurchaseOrderManager.getPurchaseOrderUnstockByPurchaseOrderId(PurchaseOrderId);
        }

        #endregion  

        #region received Data Row Remove
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorItemCode"></param>
        /// <returns></returns>
     
        [Route("receivedDataRowRemove")]
        [HttpPost]
        public int  receivedDataRowRemove(PurchaseOrderReceiveLineVM purchaseOrderReceive)
        {
            return _IPurchaseOrderManager.receivedDataRowRemove(purchaseOrderReceive);
        }

        #endregion

        #region Add Received Ordered Product
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseOrderReceive"></param>
        /// <returns></returns>
        [Route("addReceivedOrderedProduct")]
        [HttpPost]
        public int addReceivedOrderedProduct(PurchaseOrderReceiveLineVM purchaseOrderReceive)
        {
            return _IPurchaseOrderManager.addReceivedOrderedProduct(purchaseOrderReceive);
        }
         
        #endregion

        #region Update Single Purchase Order Item
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PurchaseOrderLineModel"></param>
        /// <returns></returns>
        [Route("updateSinglePurchaseOrderItem")]
        [HttpPost]
        public int updateSinglePurchaseOrderItem(PurchaseOrderLineVM PurchaseOrderLineModel)
        {
            return _IPurchaseOrderManager.updateSinglePurchaseOrderItem(PurchaseOrderLineModel);
        }
        
        #endregion
        
    }
}
