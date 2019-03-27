using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Gameitem
    {
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsUnique { get; set; }
        public Gameitem(int itemtypeID,string name,int price, bool isUnique)
        {
            ItemTypeID = itemtypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
        }

        public Gameitem Clone()
        {
            return new Gameitem(ItemTypeID,Name,Price,IsUnique);
        }
}
}
