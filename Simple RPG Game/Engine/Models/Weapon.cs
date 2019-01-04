using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
   public class Weapon: Gameitem

    {
        public int MaximumDamage { get; set; }
        public int MinimumDamage { get; set; }

        public Weapon(int itemTypeID, string name, int price, int minDamage, int maxDamage)
        :base(itemTypeID,name,price)
        {
            MaximumDamage = maxDamage;
            MinimumDamage = minDamage;
        }

        public new Weapon Clone()
        {
            return new Weapon(ItemTypeID,Name,Price,MinimumDamage,MaximumDamage);
        }
    }
}

