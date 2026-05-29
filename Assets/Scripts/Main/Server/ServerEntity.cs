using ImmigrationSim.Main.Traveller;
using UnityEngine;

namespace ImmigrationSim.Main.Server
{
    public class ServerEntity : MonoBehaviour, IServer
    {
        //Fields it needs:
        private TravellerEntity currentTraveller;
        private float remainingTime;
        private CheckType serverCheckType;
        public bool IsAvailable { get; private set; }

        public void Init(CheckType checkType)
        {
            serverCheckType = checkType;
            IsAvailable = true;
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
                currentTraveller.RecordServiceEnd(serverCheckType);
                // TODO: ServerEventChannel broadcast a Server has been freed.
                FreeServer();
            }
        }

        public void Assign(TravellerEntity traveller)
        {
            IsAvailable = false;
            traveller.RecordServiceStart(serverCheckType);
            // TODO: Set remainingTime from the ProcessingTimeGenerator
            remainingTime = 5f;
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
