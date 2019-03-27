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
using System.Windows;

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
                CompleteQuestAtLocation();
                CurrentTrader = CurrentLocation.TraderHere;
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
        //TradingRelayCommands
        public RelayCommand SellCommand { get; set; }
        public RelayCommand BuyCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        public bool HasMonster => CurrentMonster != null;
        public bool HasTrader => CurrentTrader != null;
       
        //Traders
        private Trader _currentTrader;
        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;
                OnPropertyChanged(nameof(CurrentTrader));
                OnPropertyChanged(nameof(HasTrader));
            }
        }

        
        
        //Constructor
        public GameSession()
        {
            //Player related stuff
            CurrentPlayer = new Player
            {
                Name = "Geralt",
                CharacterClass = Player._CharacterClass.Paladin,
                CurrentHitPoints = 10,
                MaximumHitPoints = 30,
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

       //questing

       private void CompleteQuestAtLocation()
       {
           foreach (Quest quest in CurrentLocation.QuestAvailable )
           {
               QuestStatus questToComplete =
                   CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);

               if (questToComplete != null)
               {
                    if (CurrentPlayer.HasAllTheItems(quest.ItemsToComplete))
                    {
                        foreach (ItemQuantity ItemQuantity in quest.ItemsToComplete)
                        {
                            for (int i = 0; i > ItemQuantity.Quantity; i++)
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == ItemQuantity.ItemID));
                        }

                        RaiseMessage(" ");
                        RaiseMessage($"You completed the {quest.Name}");
                        CurrentPlayer.ExperiencePoints += quest.RewardExperiencePoints;
                        RaiseMessage($"You receive {quest.RewardExperiencePoints} XP");
                        CurrentPlayer.Gold += quest.RewardGold;
                        RaiseMessage($"You receive {quest.RewardGold} gold!");

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            Gameitem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                            CurrentPlayer.AddItemToInventory(rewardItem);
                            RaiseMessage($"You have received {quest.RewardItems}");
                        }
                    }
                   questToComplete.IsCompleted = true;
               }
           }
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
                    RaiseMessage($"You have been given a quest:{quest.Name}. {quest.Description}");
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
                CurrentMonster.CurrentHitPoints -= damageToMonster;
                RaiseMessage($"You hit {CurrentMonster.Name} for{damageToMonster}!");
            }
            //If monster is killed get loot.
            if (CurrentMonster.CurrentHitPoints <= 0)
            {
                RaiseMessage(" ");
                RaiseMessage($"You defeated the {CurrentMonster.Name}!");
                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points!");
                CurrentPlayer.Gold += CurrentMonster.Gold;
                RaiseMessage($"You've got {CurrentMonster.Gold} gold!");

                foreach (Gameitem gameitem in CurrentMonster.Inventory)
                {
                    
                    CurrentPlayer.AddItemToInventory(gameitem);
                    RaiseMessage($"You receive one {gameitem.Name}");
                }

                CurrentMonster = null;
            }
            else
            {
                //If monster is still alive.
                int damageToPlayer = 
                    RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage("The monster attacks,but misses you!");
                }
                else
                {
                    CurrentPlayer.CurrentHitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} attacked you for{damageToPlayer} points!");
                }

                if(CurrentPlayer.CurrentHitPoints<=0)
                {
                    RaiseMessage(" ");
                    RaiseMessage($"You have been killed by {CurrentMonster.Name}");
                    CurrentLocation = CurrentWorld.LocationAt(0, -1);
                    CurrentPlayer.CurrentHitPoints = CurrentPlayer.Level * 10;
               }
            }
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }

        //TRADING WINDOW
       
    }
}

