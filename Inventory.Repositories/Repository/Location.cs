using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DomainModel.DatabaseModel;
using Inventory.Repositories.Interface;

namespace Inventory.Repositories.Repository
{
    public class Location :Repository<BASE_Location> ,ILocation
    {
    }
}
