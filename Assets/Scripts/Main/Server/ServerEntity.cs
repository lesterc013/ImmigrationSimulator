using ImmigrationSim.Core;
using ImmigrationSim.Main.QueueController;
using ImmigrationSim.Main.Traveller;
using UnityEngine;

namespace ImmigrationSim.Main.Server
{
    public class ServerEntity : MonoBehaviour, IServer
    {
        [SerializeField]
        private SimConfig simConfig; // To get the processing time of a Traveller.

        private TravellerEntity currentTraveller;
        private float remainingTime;
        private StageType serverStageType;
        // This event channel is DI-ed by the ServerManager to provide the corresponding StageType's QueueControllerEventChannel so that this Server may notify it when it is freed up.
        private QueueControllerEventChannel queueControllerEventChannel;
        // This EC also DI-ed by ServerManager to raise the exiting traveller.
        private ServerEventChannel serverEventChannel;

        public bool IsAvailable { get; private set; }

        public void Init(StageType stageType, QueueControllerEventChannel queueControllerEventChannel, ServerEventChannel serverEventChannel)
        {
            serverStageType = stageType;
            IsAvailable = true;
            this.queueControllerEventChannel = queueControllerEventChannel;
            this.serverEventChannel = serverEventChannel;
        }

        private void Update()
        {
            if (IsAvailable)
                return;

            // Has been assigned so we need to tick down the clock
            if (remainingTime > 0)
            {
                remainingTime -= SimClock.Instance.SimDeltaTime;
            }

            // Release Traveller and reset internal state.
            if (remainingTime <= 0)
            {
                Debug.Log($"{serverStageType} Server freed.");
                currentTraveller.RecordServiceEnd(serverStageType);
                // This is to tell its QueueController to call TryAssign().
                queueControllerEventChannel.RaiseServerReadyForNext();
                // This is to flow the Traveller downstream to the next Queue or Sink.
                serverEventChannel.RaiseTravellerExitingService(currentTraveller);
                FreeServer();
            }
        }

        public void Assign(TravellerEntity traveller)
        {
            Debug.Log($"{serverStageType} server: New traveller assigned.");
            IsAvailable = false;
            traveller.RecordServiceStart(serverStageType);
            // TODO: Set remainingTime from the ProcessingTimeGenerator
            remainingTime = 20f;
            currentTraveller = traveller;
        }

        private void FreeServer()
        {
            IsAvailable = true;
            currentTraveller = null;
            remainingTime = 0;
        }
    }
}
