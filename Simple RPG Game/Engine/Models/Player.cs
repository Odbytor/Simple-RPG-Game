using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Engine.Models
{
    public class Player : LivingEntity
    {
       
        public enum _CharacterClass
        {
           Paladin,
           Warrior,
           Monk,
           Mage
        }

        private _CharacterClass _characterClass;
        
        private int _experiencePoints;
        private int _level;
        

        public _CharacterClass CharacterClass
        {
            get { return _characterClass;}
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
        
      
        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; set; }
        
        public Player()
        { 
            
            Quests = new ObservableCollection<QuestStatus>();
        }

        public bool HasAllTheItems(List<ItemQuantity> Items)
        {
            foreach (ItemQuantity Item in Items)
            {
                if(Inventory.Count(i => i.ItemTypeID == Item.ItemID)<Item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
