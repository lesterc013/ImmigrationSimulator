using ImmigrationSim.Main.Traveller;
using System;
using UnityEngine;

namespace ImmigrationSim.Main.QueueController
{
    [CreateAssetMenu(fileName = "QueueControllerEventChannenl", menuName = "Scriptable Objects/QueueControllerEventChannel")]
    public class QueueControllerEventChannel : ScriptableObject
    {
        public Action<TravellerEntity> OnNewTraveller;
        public void RaiseNewTravellerArrived(TravellerEntity newTraveller)
        {
            OnNewTraveller?.Invoke(newTraveller);
        }

        /// <summary>
        /// To announce a Server is freed up.
        /// </summary>
        public Action OnServerReadyForNext;
        public void RaiseServerReadyForNext()
        {
            OnServerReadyForNext?.Invoke();
        }

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
