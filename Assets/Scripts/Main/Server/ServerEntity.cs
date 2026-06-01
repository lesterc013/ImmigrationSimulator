using ImmigrationSim.Main.Traveller;
using UnityEngine;

namespace ImmigrationSim.Main.Server
{
    public class ServerEntity : MonoBehaviour, IServer
    {
        //Fields it needs:
        private TravellerEntity currentTraveller;
        private float remainingTime;
        private StageType serverCheckType;
        private TravellerEventChannel travellerEventChannel;

        public bool IsAvailable { get; private set; }

        // DI in the checkType and corresponding TravellerEventChannel
        public void Init(StageType checkType, TravellerEventChannel travellerEventChannel)
        {
            serverCheckType = checkType;
            IsAvailable = true;
            this.travellerEventChannel = travellerEventChannel;
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
                Debug.Log("Server freed.");
                currentTraveller.RecordServiceEnd(serverCheckType);
                // This is to tell its QueueController to call TryAssign().
                travellerEventChannel.RaiseServerReadyForNext();
                // This is to flow the Traveller downstream to the next Queue or Sink.
                travellerEventChannel.RaiseTravellerExitingService(currentTraveller);
                FreeServer();
            }
        }

        public void Assign(TravellerEntity traveller)
        {
            Debug.Log("New traveller assigned.");
            IsAvailable = false;
            traveller.RecordServiceStart(serverCheckType);
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
