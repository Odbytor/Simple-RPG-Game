using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal class WorldFactory
    {
        internal World CreateWorld()
        {
            World newworld = new World();
           
            newworld.AddLocation(-2, -1, "Farmer's Field",
                "There are rows of corn growing here, with giant rats hiding between them.",
                "/Engine;component/Components/FarmFields.png");

            newworld.AddLocation(-1, -1, "Farmer's House",
                "This is the house of your neighbor, Farmer Ted.",
                "/Engine;component/Components/Farmhouse.png");

            newworld.AddLocation(0, -1, "Home",
                "This is your home",
                "/Engine;component/Components/Home.png");

            newworld.AddLocation(-1, 0, "Trading Shop",
                "The shop of Susan, the trader.",
                "/Engine;component/Components/Trader.png");

            newworld.AddLocation(0, 0, "Town square",
                "You see a fountain here.",
                "/Engine;component/Components/TownSquare.png");

            newworld.AddLocation(1, 0, "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "/Engine;component/Components/TownGate.png");

           newworld.AddLocation(2, 0, "Spider Forest",
                "The trees in this forest are covered with spider webs.",
               "/Engine;component/Components/SpiderForest.png");

            newworld.AddLocation(0, 1, "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "/Engine;component/Components/HerbalistsHut.png");

            newworld.AddLocation(0, 2, "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "/Engine;component/Components/HerbalistsGarden.png");
            return newworld;


        }
    }
}


