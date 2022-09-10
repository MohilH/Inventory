using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DomainModel.DatabaseModel;
using Inventory.CommonViewModels;

namespace Inventory.BusinessLogic.Interface
{
    public interface IPurchaseOrderManager
    {
        int addPurchaseOrder(PurchaseOrderVM purchaseOrder);

     List<ListPurchaseOrderVM> getAllPurchaseOrder();

     PurchaseOrderVM getPurchaseOrderByID(string OrderNumber);

     List<PurchaseOrderLineVM> getPurchaseOrderItemByOrderNumber(string OrderNumber);
     int addPurchaseOrderItem(List<PurchaseOrderLineVM> PurchaseOrderLineModel );  

     int addPurchaseOrderReceiveItem(List<PurchaseOrderReceiveLineVM> PurchaseOrderReceiveLineModel);

     int addPurchaseOrderReturnItem(List<PurchaseOrderReturnVM> PurchaseOrderReturnLineModel);

     int addPurchaseOrderUnstockItem(List<PurchaseOrderUnstockVM> PurchaseOrderUnstockLineModel);

     List<PurchaseOrderReceiveLineVM> getPurchaseOrderReceiveByPurchaseOrderId(int PurchaseOrderId);



     List<PurchaseOrderReturnVM> getPurchaseOrderReturnByPurchaseOrderId(int PurchaseOrderId);

     List<PurchaseOrderUnstockVM> getPurchaseOrderUnstockByPurchaseOrderId(int PurchaseOrderId);

    

     int receivedDataRowRemove(PurchaseOrderReceiveLineVM purchaseOrderReceive);

     int addReceivedOrderedProduct(PurchaseOrderReceiveLineVM purchaseOrderReceive);

     int updateSinglePurchaseOrderItem(PurchaseOrderLineVM PurchaseOrderLineModel);

     List<PurchaseOrderLineVM> getPurchaseOrderItemBypurchaseOrderId(int purchaseOrderId);

     int addPOReceiveAutoFillItem(PurchaseOrderReceiveLineVM PurchaseOrderReceiveLineModel);
    }
}
