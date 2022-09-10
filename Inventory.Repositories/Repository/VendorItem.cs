using Inventory.DomainModel.DatabaseModel;
using Inventory.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repositories.Repository
{
    public class VendorItem :Repository<BASE_VendorItem> ,IVendorItem
    {
    }
}
