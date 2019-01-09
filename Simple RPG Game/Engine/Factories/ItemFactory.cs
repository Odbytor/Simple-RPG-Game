using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
   static class ItemFactory
   {
       private static List<Gameitem> _standardGameitems;

       static ItemFactory()
       {
           _standardGameitems= new List<Gameitem>();
           _standardGameitems.Add(new Weapon(1001,"Pointy Stick",1,1,2 ));
           _standardGameitems.Add(new Weapon(1002,"Rusty Sword",5,1,3));
           _standardGameitems.Add(new Gameitem(9001,"Snake fang", 1));
           _standardGameitems.Add(new Gameitem(9002,"Snakeskin", 2));
       }

       public static Gameitem CreateGameItem(int itemTypeId)
       {
           Gameitem standardItem = _standardGameitems.FirstOrDefault(item => item.ItemTypeID == itemTypeId);
           if (standardItem != null)
           {
               return standardItem.Clone();
           }

           return null;
       }

   }
}
