using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Factories;

namespace Engine.Models
{
    public class Location
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public List<Quest> QuestAvailable { get; set; } = new List<Quest>();

        public List<MonsterEncounter> MonsterHere { get; set; } = new List<MonsterEncounter>();

        //Traders
        public Trader TraderHere { get; set; }

        public void AddMonster(int monsterID, int chanceofEncountering)
        {
            if (MonsterHere.Exists(m => m.MonsterID == monsterID))
            {
                MonsterHere.First(m => m.MonsterID == monsterID)
                    .ChanceOfEncountering = chanceofEncountering;
            }
            else

            {
                MonsterHere.Add(new MonsterEncounter(monsterID,chanceofEncountering));
            }
        }

        public Monster GetMonster()
        {
            if (!MonsterHere.Any())
            {
                return null;
            }

            int totalChances = MonsterHere.Sum(m => m.ChanceOfEncountering);
            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);
            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in MonsterHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;
                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }

            return MonsterFactory.GetMonster(MonsterHere.Last().MonsterID);
        }
    }
}
