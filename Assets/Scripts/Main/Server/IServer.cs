using ImmigrationSim.Main.Traveller;
using UnityEngine;

namespace ImmigrationSim.Main
{
    public interface IServer
    {
        public void Assign(TravellerEntity traveller);
    }
}
