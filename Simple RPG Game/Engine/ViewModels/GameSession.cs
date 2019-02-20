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

        public Weapon CurrentWeapon { get; set; }
        //Commands
        public RelayCommand MoveNorthCommand { get; set; }
        public RelayCommand MoveSouthCommand { get; set; }
        public RelayCommand MoveEastCommand { get; set; }
        public RelayCommand MoveWestCommand { get; set; }
        //Fighting
        public RelayCommand AttackMonsterCommand { get; set;}


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
            //FightingRelayCommands
            AttackMonsterCommand = new RelayCommand(OnClickAttackMonster);
            //World
            CurrentWorld = WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, 0);

            //Items
            CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1001));
            CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1001));
            CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1002));
            //Fighting stuff
            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

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

        

        public void OnClickAttackMonster(object s)
        {
            if (CurrentWeapon is null)
            {
                RaiseMessage("You must draw a weapon first, you dumbass!");
                return;
            }
            //Determinating damage to monster
            int damageToMonster =
                RandomNumberGenerator.NumberBetween(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage);
            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed {CurrentMonster.Name}. Try harder!");
            }
            else
            {
                CurrentMonster.HitPoints = -damageToMonster;
                RaiseMessage($"You hit an {CurrentMonster.Name} for{damageToMonster}!");
            }
            //If monster is killed get loot.
            if (CurrentMonster.HitPoints <= 0)
            {
                RaiseMessage(" ");
                RaiseMessage($"You defeated the{CurrentMonster.Name}!");
                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                RaiseMessage($"You receive{CurrentMonster.RewardExperiencePoints} experience points!");
                CurrentPlayer.Gold += CurrentMonster.RewardGold;
                RaiseMessage($"You got{CurrentMonster.RewardGold} gold!");

                foreach (ItemQuantity itemQuantity in CurrentMonster.Inventory)
                {
                    Gameitem Item = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                    CurrentPlayer.AddItemToInventory(Item);
                    RaiseMessage($"You receive {itemQuantity.Quantity} x {Item.Name}");
                }
                GetMonsterAtLocation();
            }
            else
            {
                //If monster is still alive.
                int damageToPlayer = CurrentPlayer.HitPoints -=
                    RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage("The monster attacks,but misses you!");
                }
                else
                {
                    CurrentPlayer.HitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} attacked you for{damageToPlayer} points!");
                }

                if(CurrentPlayer.HitPoints<=0)
                {
                    RaiseMessage(" ");
                    RaiseMessage($"You have been killed by{CurrentMonster.Name}");
                    CurrentLocation = CurrentWorld.LocationAt(0, -1);
                    CurrentPlayer.HitPoints = CurrentPlayer.Level * 10;
               }
            }
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}

