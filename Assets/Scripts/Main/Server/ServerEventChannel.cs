using ImmigrationSim.Main.Traveller;
using System;
using UnityEngine;

namespace ImmigrationSim.Main.Server
{
    [CreateAssetMenu(fileName = "ServerEventChannel", menuName = "Scriptable Objects/ServerEventChannel")]
    public class ServerEventChannel : ScriptableObject
    {
        /// <summary>
        /// To handoff traveller downstream.
        /// </summary>
        public Action<TravellerEntity> OnTravellerExitingService;
        public void RaiseTravellerExitingService(TravellerEntity completedTraveller)
        {
            OnTravellerExitingService?.Invoke(completedTraveller);
        }
    }
}
