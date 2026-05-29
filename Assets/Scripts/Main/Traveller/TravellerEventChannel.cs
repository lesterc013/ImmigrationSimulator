using System;
using UnityEngine;

namespace ImmigrationSim.Main.Traveller
{
    [CreateAssetMenu(fileName = "TravellerEventChannel", menuName = "Scriptable Objects/TravellerEventChannel")]
    public class TravellerEventChannel : ScriptableObject
    {
        public Action<TravellerEntity> OnNewTraveller;

        public void RaiseNewTravellerArrived(TravellerEntity newTraveller)
        {
            OnNewTraveller?.Invoke(newTraveller);
        }
    }
}
