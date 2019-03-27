using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
   internal static class QuestFactory
    {
        private static readonly List<Quest> _quest = new List<Quest>();

        static QuestFactory()
        {
              //Declare the items need to complete the quest and its rewards.
              List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
              List<ItemQuantity> rewardItems = new List<ItemQuantity>();
              

              itemsToComplete.Add(new ItemQuantity(9001,1));
              rewardItems.Add(new ItemQuantity(1002, 1));

            //Create quest here.

            _quest.Add(new Quest(
                1,"Clear the herb garden", "Defeat the snakes in the Herbalist's garden",itemsToComplete,25,10,rewardItems));
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quest.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
