using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main.Traveller
{
    public class QueueController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Drag in the correct TravellerEventChannel i.e. Security or Immigration for this case.")]
        private TravellerEventChannel travellerEc;

        [SerializeField]
        private CheckType queueCheckType;

        private Queue<TravellerEntity> queue;

        private void Awake()
        {
            queue = new Queue<TravellerEntity>();

            travellerEc.OnNewTraveller += HandleNewTraveller;
            // Need also subscribe to Server Event Channel OnFreeServer to receive a call from a free Server
        }

        // NewTraveller received
        // Subscribe to shared event with TravellerSpawner to receive a new Traveller
        // Push Traveller into Queue
        // Call TryAssign
        private void HandleNewTraveller(TravellerEntity newTraveller)
        {
            queue.Enqueue(newTraveller);
            newTraveller.RecordQueueJoin(queueCheckType);
            TryAssignTraveller();
        }

        // TryAssign
        // check if Queue has something
        // Check if ServerManager has a free Server
        // If no, then end
        // If yes, then get that Server and call the Assign
        private void TryAssignTraveller()
        {
            if (queue.Count == 0)
            {
                return;
            }

            Debug.Log("Check if ServerManager has a free Server, get back a Server interface/object and call the Assign method on it. Then done. Otherwise dont do anything.");
        }

        private void OnDestroy()
        {
            travellerEc.OnNewTraveller -= HandleNewTraveller;
        }
    }
}
