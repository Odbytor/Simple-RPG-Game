using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Trader: BaseNotificationClass
    {
        public string Name { get; set; }
        public ObservableCollection<Gameitem> TradersInv { get; set; }

        public Trader(string name)
        {
            Name = name;
            TradersInv = new ObservableCollection<Gameitem>();
        }

        public void AddItemToInvetory(Gameitem item)
        {
            TradersInv.Add(item);
        }

        public void RemoveItemFromInv(Gameitem item)
        {
            TradersInv.Remove(item);
        }
    }
}
