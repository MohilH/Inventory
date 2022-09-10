using Inventory.DomainModel.DatabaseModel;
using Inventory.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repositories.Repository
{
    public class PaymentStatus : Repository<PO_PaymentStatus>,IPaymentStatus
    {
    }
}
