using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void AddMonster(int monsterID, int chanceofEncountering)
        {
            if (MonsterHere.Exists(m => m.MonsterID == monsterID))
            {
                MonsterHere.First(m => m.MonsterID == monsterID)
                    .ChanceOfEncountering = chanceofEncountering;
            }
        }
}
}
