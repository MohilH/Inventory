using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Repositories.Interface;
using Inventory.DomainModel.DatabaseModel;

namespace Inventory.Repositories.Repository
{
    public class PurchaseOrder :Repository<PO_PurchaseOrder> , IPurchaseOrder
    {
    }
}
