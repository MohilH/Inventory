using Inventory.DomainModel.DatabaseModel;
using Inventory.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Repositories.Repository
{
    public class InventoryStatus : Repository<PO_InventoryStatus> ,IInventoryStatus
    {
    }
}
