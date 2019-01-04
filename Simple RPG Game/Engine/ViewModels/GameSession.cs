using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Commands;
using Engine.Models;
using Engine.Factories;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation { get; set; }

        //Commands
        public RelayCommand AddXPCommand { get; set; }
        //Constructor
        public GameSession()
        {
            //Player related stuff
            CurrentPlayer = new Player();
            CurrentPlayer.Name = "Geralt";
            CurrentPlayer.Gold = 100000;
            CurrentPlayer.CharacterClass = Player._CharacterClass.Paladin;
            CurrentPlayer.HitPoints = 10;
            CurrentPlayer.ExperiencePoints = 0;
            CurrentPlayer.Level = 1;
            AddXPCommand = new RelayCommand(AddXP);

           
            //World
            WorldFactory factory = new WorldFactory();
            CurrentWorld = factory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, -1);
        }

        private void AddXP(object s)
        {
            CurrentPlayer.ExperiencePoints = CurrentPlayer.ExperiencePoints + 10;
        }

    }
}

