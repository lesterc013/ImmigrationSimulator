using ImmigrationSim.Core;
using ImmigrationSim.Main.QueueController;
using ImmigrationSim.Main.Server;
using ImmigrationSim.Main.Traveller;
using UnityEngine;

namespace ImmigrationSim.Main.Analytics
{
    public class AnalyticsEngine : MonoBehaviour
    {
        [SerializeField] private QueueControllerEventChannel securityQueueEventChannel;
        [SerializeField] private QueueControllerEventChannel immigrationQueueEventChannel;
        [SerializeField] private ServerEventChannel finalStageServerEventChannel;
        [SerializeField] private SimConfig simConfig;
        [SerializeField] private ServerManager securityServerManager;
        [SerializeField] private ServerManager immigrationServerManager;

        private int securityTravellersTotal;
        private float securityTravellersWaitTotal;
        private float securityAvgWaitTime;

        private int immigrationTravellersTotal;
        private float immigrationTravellersWaitTotal;
        private float immigrationAvgWaitTime;

        private void Awake()
        {
            securityQueueEventChannel.OnTravellerLeftQueue += UpdateSecurityAvgWaitTimeDisplay;
            immigrationQueueEventChannel.OnTravellerLeftQueue += UpdateImmigrationAvgWaitTimeDisplay;
        }

        private void UpdateSecurityAvgWaitTimeDisplay(TravellerEntity securityTraveller)
        {
            UpdateStageTypeData(StageType.Security, securityTraveller);
            // TODO: Update the TMP Text display
            Debug.Log($"Security avg wait time: {securityAvgWaitTime}");
        }

        private void UpdateImmigrationAvgWaitTimeDisplay(TravellerEntity securityTraveller)
        {
            UpdateStageTypeData(StageType.Immigration, securityTraveller);
            // TODO: Update the TMP Text display
            Debug.Log($"Immigration avg wait time: {immigrationAvgWaitTime}");
        }

        private void UpdateStageTypeData(StageType stage, TravellerEntity traveller)
        {
            float waitTime = traveller.Timings[stage].ServiceStartTime - traveller.Timings[stage].QueueJoinTime;

            if (stage == StageType.Security)
            {
                securityTravellersTotal += 1;
                securityTravellersWaitTotal += waitTime;
                securityAvgWaitTime = securityTravellersWaitTotal / securityTravellersTotal;
            }
            else if (stage == StageType.Immigration)
            {
                immigrationTravellersTotal += 1;
                immigrationTravellersWaitTotal += waitTime;
                immigrationAvgWaitTime = immigrationTravellersWaitTotal / immigrationTravellersTotal;
            }
            else
            {
                Debug.LogError($"{stage} not set as a selector.");
            }
        }

        private void OnDestroy()
        {
            securityQueueEventChannel.OnTravellerLeftQueue -= UpdateSecurityAvgWaitTimeDisplay;
        }
    }
}
