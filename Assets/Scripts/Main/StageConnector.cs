using ImmigrationSim.Main.QueueController;
using ImmigrationSim.Main.Server;
using ImmigrationSim.Main.Traveller;
using UnityEngine;

namespace ImmigrationSim.Main
{
    public class StageConnector : MonoBehaviour
    {
        [SerializeField]
        private ServerEventChannel prevStageServerEventChannel;

        [SerializeField]
        private QueueControllerEventChannel nextStageQueueControllerEventChannel;

        public void Init()
        {
            prevStageServerEventChannel.OnTravellerExitingService += MoveTravellerToNextStage;
        }

        private void MoveTravellerToNextStage(TravellerEntity traveller)
        {
            nextStageQueueControllerEventChannel.RaiseNewTravellerArrived(traveller);
        }

        private void OnDestroy()
        {
            prevStageServerEventChannel.OnTravellerExitingService -= MoveTravellerToNextStage;
        }
    }
}
