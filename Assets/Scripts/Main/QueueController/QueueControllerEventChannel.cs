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

        public Action<TravellerEntity> OnTravellerLeftQueue;
        public void RaiseTravellerLeftQueue(TravellerEntity traveller)
        {
            OnTravellerLeftQueue?.Invoke(traveller);
        }

        /// <summary>
        /// To announce a Server is freed up.
        /// </summary>
        public Action OnServerReadyForNext;
        public void RaiseServerReadyForNext()
        {
            OnServerReadyForNext?.Invoke();
        }
    }
}
