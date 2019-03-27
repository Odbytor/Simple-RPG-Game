using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GroupedInventoryItem: BaseNotificationClass
    {
        private Gameitem _item;
        private int _quantity;

        public Gameitem Item
        {
            get { return _item;}
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public int Quantity
        {
            get { return _quantity;}
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public GroupedInventoryItem(Gameitem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

    }
}
