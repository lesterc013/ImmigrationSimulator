using System;
using UnityEngine;

namespace ImmigrationSim.Main.Traveller
{
    [CreateAssetMenu(fileName = "TravellerEventChannel", menuName = "Scriptable Objects/TravellerEventChannel")]
    public class TravellerEventChannel : ScriptableObject
    {
        public Action<Traveller> OnNewTraveller;

        public void RaiseNewTravellerArrived(Traveller newTraveller)
        {
            OnNewTraveller?.Invoke(newTraveller);
        }
    }
}
