using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newworld = new World();
           
            newworld.AddLocation(-2, -1, "Farmer's Field",
                "There are rows of corn growing here, with giant rats hiding between them.",
                "/Engine;component/Components/FarmFields.png");
            newworld.LocationAt(-2,-1).AddMonster(2,100);
            

            newworld.AddLocation(-1, -1, "Farmer's House",
                "This is the house of your neighbor, Farmer Ted.",
                "/Engine;component/Components/Farmhouse.png");
            newworld.LocationAt(-1,-1).TraderHere= 
                TraderFactory.GetTraderByName("FarmerTed");

            newworld.AddLocation(0, -1, "Home",
                "This is your home",
                "/Engine;component/Components/Home.png");

            newworld.AddLocation(-1, 0, "Trading Shop",
                "The shop of Susan, the trader.",
                "/Engine;component/Components/Trader.png");
            newworld.LocationAt(-1,0).TraderHere=
            TraderFactory.GetTraderByName("Susan");

            newworld.AddLocation(0, 0, "Town square",
                "You see a fountain here.",
                "/Engine;component/Components/TownSquare.png");

            newworld.AddLocation(1, 0, "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "/Engine;component/Components/TownGate.png");

           newworld.AddLocation(2, 0, "Spider Forest",
                "The trees in this forest are covered with spider webs.",
               "/Engine;component/Components/SpiderForest.png");
            newworld.LocationAt(2,0).AddMonster(3,100);

            newworld.AddLocation(0, 1, "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "/Engine;component/Components/HerbalistsHut.png");
            newworld.LocationAt(0,1).TraderHere=
            TraderFactory.GetTraderByName("Pete the Herbalist");

            newworld.LocationAt(0,1).QuestAvailable.Add(QuestFactory.GetQuestByID(1));

            newworld.AddLocation(0, 2, "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "/Engine;component/Components/HerbalistsGarden.png");
            newworld.LocationAt(0,2).AddMonster(1,100);

            return newworld;


        }
    }
}


