using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main.Traveller
{
    public class QueueController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Drag in previous stageType's TravellerEventChannel - for the first stageType, it is its own one.")]
        private TravellerEventChannel inflowTravellerEventChannel;

        [SerializeField]
        [Tooltip("Drag in own TravellerEventChannel.")]
        private TravellerEventChannel ownTravellerEventChannel;

        [SerializeField]
        private StageType queueStageType;

        [SerializeField]
        [Tooltip("Drag in the corresponding server manager based on this stage type.")]
        // Note: The reason this is a direct reference instead of an Event is because we need the correct ServerManager to respond to the "is there free server?" query.
        private ServerManager serverManager;

        private Queue<TravellerEntity> queue;

        private void Awake()
        {
            queue = new Queue<TravellerEntity>();

            inflowTravellerEventChannel.OnNewTraveller += HandleNewTraveller;
            ownTravellerEventChannel.OnServerReadyForNext += HandleServerFreedUp;
        }

        // NewTraveller received
        // Subscribe to shared event with TravellerSpawner to receive a new Traveller
        // Push Traveller into Queue
        // Call TryAssign
        private void HandleNewTraveller(TravellerEntity newTraveller)
        {
            queue.Enqueue(newTraveller);
            newTraveller.RecordQueueJoin(queueStageType);
            TryAssignTraveller();
        }

        private void HandleServerFreedUp()
        {
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

            Debug.Log($"Queue length now: {queue.Count}");

            if (serverManager.FindFreeServer(out IServer freeServer))
            {
                freeServer.Assign(queue.Dequeue());
            }
        }

        private void OnDestroy()
        {
            inflowTravellerEventChannel.OnNewTraveller -= HandleNewTraveller;
            ownTravellerEventChannel.OnServerReadyForNext -= HandleServerFreedUp;
        }
    }
}
