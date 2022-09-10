using Inventory.BusinessLogic.Interface;
using Inventory.DomainModel.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory.WebAPI.Controllers
{
    public class InventoryController : ApiController
    {
        private readonly IInventoryManager _InventoryManager;
        public InventoryController()
        {
        }
        public InventoryController(IInventoryManager InventoryManager)
        {
            _InventoryManager = InventoryManager;
        }

        [Route("GetAllInventoryRecords")]
        [HttpGet]
        public List<tblInventory> GetAllInventoryRecords()
        {
            return _InventoryManager.getAllInventoryRecords();
        }

     
    }
}
