using ImmigrationSim.Core;
using ImmigrationSim.Main.Server;
using ImmigrationSim.Main.Traveller;
using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main.QueueController
{
    public class QueueControllerEntity : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Drag in own stageType's QueueControllerEventChannel.")]
        private QueueControllerEventChannel ownQueueControllerEventChannel;

        [SerializeField]
        private StageType queueStageType;

        [SerializeField]
        [Tooltip("Drag in the corresponding server manager based on this stage type.")]
        // Note: The reason this is a direct reference instead of an Event is because we need the correct ServerManager to respond to the "is there free server?" query.
        private ServerManager serverManager;

        private Queue<TravellerEntity> queue;
        public int QueueCount { get { return queue.Count; } }
        private int maxQueueCount;

        public void Init()
        {
            queue = new Queue<TravellerEntity>();

            ownQueueControllerEventChannel.OnNewTraveller += HandleNewTraveller;
            ownQueueControllerEventChannel.OnServerReadyForNext += HandleServerFreedUp;
        }

        private void HandleNewTraveller(TravellerEntity newTraveller)
        {
            queue.Enqueue(newTraveller);
            if (queue.Count > maxQueueCount)
            {
                maxQueueCount = queue.Count;
                ownQueueControllerEventChannel.RaiseMaxQueueCount(maxQueueCount);
            }
            newTraveller.RecordQueueJoin(queueStageType);
            TryAssignTraveller();
        }

        private void HandleServerFreedUp()
        {
            TryAssignTraveller();
        }

        private void TryAssignTraveller()
        {
            if (queue.Count == 0)
            {
                return;
            }

            if (serverManager.FindFreeServer(out IServer freeServer))
            {
                var travellerToAssign = queue.Dequeue();
                travellerToAssign.RecordServiceStart(queueStageType);
                ownQueueControllerEventChannel.RaiseTravellerLeftQueue(travellerToAssign);
                freeServer.Assign(travellerToAssign);
            }
        }

        private void OnDestroy()
        {
            ownQueueControllerEventChannel.OnNewTraveller -= HandleNewTraveller;
            ownQueueControllerEventChannel.OnServerReadyForNext -= HandleServerFreedUp;
        }
    }
}
