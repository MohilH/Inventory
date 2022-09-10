using Inventory.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DomainModel.DatabaseModel;

namespace Inventory.Repositories.Repository
{
    public class PurchaseOrderLine : Repository<PO_PurchaseOrder_Line> , IPurchaseOrderLine
    {
    }
}
