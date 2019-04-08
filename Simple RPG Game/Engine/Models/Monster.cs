using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
   public class Monster: LivingEntity
    {
        

     
     public string ImageName { get; set; }
    

        

        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }
       

        

        public Monster(string name, string imageName, int maximumHp, int currentHp, int minimumDamage, int maximumDamage, int rewardExperiencePoints,
            int gold): 
            base(name, maximumHp, currentHp, gold)
        { 
            ImageName = string.Format("/Engine;component/Components/Monsters/{0}", imageName);
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            RewardExperiencePoints = rewardExperiencePoints;
          

        }
    }
}
