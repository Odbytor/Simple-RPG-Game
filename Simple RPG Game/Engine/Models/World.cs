using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class World
    {
        private List<Location> _locations = new List<Location>();

        internal void AddLocation(int xCoords, int yCoords, string name, string description, string imageName)
        {
            Location NewLocation = new Location();
            NewLocation.ImageName = imageName;
            NewLocation.Description = description;
            NewLocation.Name = name;
            NewLocation.XCoordinate = xCoords;
            NewLocation.YCoordinate = yCoords;

            _locations.Add(NewLocation);
        }

        public Location LocationAt(int xCoords, int yCoords)
        {
            Location CurrentLoc = null;
            foreach (Location loc in _locations)
            {
                if (loc.XCoordinate == xCoords && loc.YCoordinate == yCoords)
                    CurrentLoc = loc;
            }

            return CurrentLoc;
        }
    }
}
