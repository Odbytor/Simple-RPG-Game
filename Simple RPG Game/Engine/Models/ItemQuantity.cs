using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
   public class ItemQuantity
    {
    public int ItemdID { get; set; }
    public int Quantity { get; set; }

        public ItemQuantity(int itemID, int quantity)
        {
            ItemdID = itemID;
            Quantity = quantity;
        }
    }
}
