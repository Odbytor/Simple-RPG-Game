using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
   public static class TraderFactory
    {
        private static readonly  List<Trader> _traders = new List<Trader>();
        static TraderFactory()
        {
            Trader susan = new Trader("Susan");
            susan.AddItemToInvetory(ItemFactory.CreateGameItem(1001));

            Trader farmerTed = new Trader("Farmer Ted");
            farmerTed.AddItemToInvetory(ItemFactory.CreateGameItem(1001));

            Trader peteHerbalist = new Trader("Pete the Herbalist");
            peteHerbalist.AddItemToInvetory(ItemFactory.CreateGameItem(1001));

            AddTraderToList(susan);
            AddTraderToList(farmerTed);
            AddTraderToList(peteHerbalist);

        }
        public static Trader GetTraderByName(string name)
        {
            return _traders.FirstOrDefault(x => x.Name == name);
        }

        private static void AddTraderToList(Trader trader)
        {
            if (_traders.Any(x => x.Name == trader.Name))
            {
               throw new ArgumentException($"There is already a trader with that name!");
            }
            _traders.Add(trader);
        }
    }
}
