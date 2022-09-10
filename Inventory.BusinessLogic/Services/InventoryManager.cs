using Inventory.BusinessLogic.Interface;
using Inventory.DomainModel.DatabaseModel;
using Inventory.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.BusinessLogic.Services
{
    public class InventoryManager : IInventoryManager
    {
        private IInventory _Inventory;

        public InventoryManager(IInventory IInventory)
        {
            this._Inventory = IInventory;
        }

        public List<tblInventory> getAllInventoryRecords()
        {
            List<tblInventory> getAllRecords = new List<tblInventory>();
            getAllRecords = _Inventory.GetAll().ToList();
            return getAllRecords;
        }

       
    }
}
