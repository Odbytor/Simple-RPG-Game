using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Commands;
using Engine.EventArgs;
using Engine.Models;
using Engine.Factories;

namespace Engine.ViewModels
{
    public class GameSession: BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised; 

        private Location _CurrentLocation;
        private Monster _currentMonster;

        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation
        {
            get { return _CurrentLocation; }
            set
            {
                _CurrentLocation = value;

        OnPropertyChanged(nameof(CurrentPlayer));
        OnPropertyChanged(nameof(HasLocationToNorth));
        OnPropertyChanged(nameof(CurrentLocation));
        OnPropertyChanged(nameof(HasLocationToEast));
        OnPropertyChanged(nameof(HasLocationToWest));
        OnPropertyChanged(nameof(HasLocationToSouth));

                GivePlayerQuestAtLocation();
                GetMonsterAtLocation();
            }
        }

        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                _currentMonster = value;
                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));
            }
        }
        //Commands
        public RelayCommand MoveNorthCommand { get; set; }
        public RelayCommand MoveSouthCommand { get; set; }
        public RelayCommand MoveEastCommand { get; set; }
        public RelayCommand MoveWestCommand { get; set; }


        public bool HasMonster => CurrentMonster != null;
        
        //Constructor
        public GameSession()
        {
            //Player related stuff
            CurrentPlayer = new Player
            {
                Name = "Geralt",
                CharacterClass = Player._CharacterClass.Paladin,
                HitPoints = 10,
                Gold = 1000,
                ExperiencePoints = 0,
                Level = 1

            };
            //RelayCommands
            MoveNorthCommand = new RelayCommand(OnClickMoveNorth);
            MoveSouthCommand = new RelayCommand(OnClickMoveSouth);
            MoveEastCommand = new RelayCommand(OnClickMoveEast);
            MoveWestCommand = new RelayCommand(OnClickMoveWest);
            //World
            CurrentWorld = WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, 0);

            //Items
            CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1001));
            CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1001));
            CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1002));
        }

        private void OnClickMoveNorth(object s)
        {
            if(HasLocationToNorth)
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
        }

        private void OnClickMoveWest(object s)
        {
            if(HasLocationToWest)
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate-1, CurrentLocation.YCoordinate);
        }

        private void OnClickMoveEast(object s)
        {
            if(HasLocationToEast)
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate+1, CurrentLocation.YCoordinate);
        }

        private void OnClickMoveSouth(object s)
        {
            if(HasLocationToSouth)
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
        }

       


        //Checking visibilty

        public bool HasLocationToNorth
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
            }
        }

        public bool HasLocationToEast
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
            }
        }

        public bool HasLocationToSouth
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
            }
        }

        public bool HasLocationToWest
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
            }
        }

        private void GivePlayerQuestAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestAvailable)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this,new GameMessageEventArgs(message));
        }
    }
}

