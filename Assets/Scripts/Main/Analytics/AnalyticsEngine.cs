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

        // Per stage metrics
        private int securityTravellersTotal;
        private float securityTravellersWaitTotal;
        public float SecurityAvgWaitTime { get; private set; }
        public int SecurityMaxQueueCount { get; private set; }

        private int immigrationTravellersTotal;
        private float immigrationTravellersWaitTotal;
        public float ImmigrationAvgWaitTime { get; private set; }
        public int ImmigrationMaxQueueCount { get; private set; }

        // Server utilisation
        public float SecurityServerAvgUtilisationRate { get; private set; }
        public float ImmigrationServerAvgUtilisationRate { get; private set; }

        // Global metrics
        public int TotalTravellersCompleted { get; private set; }
        public float AvgTotalTimeInSystem { get; private set; }
        public float Throughput { get; private set; }
        public float PercentageAboveWaitThreshold { get; private set; }

        private float totalTimeInSystem;
        private int travellersAboveThreshold;

        private void Awake()
        {
            securityQueueEventChannel.OnTravellerLeftQueue += UpdateSecurityAvgWaitTime;
            securityQueueEventChannel.OnNewMaxQueueCount += UpdateSecurityMaxQueueCount;
            immigrationQueueEventChannel.OnTravellerLeftQueue += UpdateImmigrationAvgWaitTime;
            immigrationQueueEventChannel.OnNewMaxQueueCount += UpdateImmigrationMaxQueueCount;
            finalStageServerEventChannel.OnTravellerExitingService += UpdateTravellerCompletedData;
        }

        private void Update()
        {
            if (SimClock.Instance.IsFinished) 
                return;

            SecurityServerAvgUtilisationRate = CalculateServerUtilisationRate(securityServerManager.GetAverageServersBusyTime());
            ImmigrationServerAvgUtilisationRate = CalculateServerUtilisationRate(immigrationServerManager.GetAverageServersBusyTime());
            if (TotalTravellersCompleted > 0)
            {
                Throughput = TotalTravellersCompleted / (SimClock.Instance.TotalSimTimeElapsed / 60f);
            }
        }

        private void UpdateSecurityAvgWaitTime(TravellerEntity securityTraveller)
        {
            UpdateStageWaitTime(StageType.Security, securityTraveller);
        }

        private void UpdateSecurityMaxQueueCount(int count)
        {
            SecurityMaxQueueCount = count;
        }

        private void UpdateImmigrationAvgWaitTime(TravellerEntity securityTraveller)
        {
            UpdateStageWaitTime(StageType.Immigration, securityTraveller);
        }

        private void UpdateImmigrationMaxQueueCount(int count)
        {
            ImmigrationMaxQueueCount = count;
        }

        private void UpdateStageWaitTime(StageType stage, TravellerEntity traveller)
        {
            float waitTime = traveller.Timings[stage].ServiceStartTime - traveller.Timings[stage].QueueJoinTime;
            if (stage == StageType.Security)
            {
                securityTravellersTotal++;
                securityTravellersWaitTotal += waitTime;
                SecurityAvgWaitTime = securityTravellersWaitTotal / securityTravellersTotal;
            }
            else if (stage == StageType.Immigration)
            {
                immigrationTravellersTotal++;
                immigrationTravellersWaitTotal += waitTime;
                ImmigrationAvgWaitTime = immigrationTravellersWaitTotal / immigrationTravellersTotal;
            }
            else
            {
                Debug.LogError($"AnalyticsEngine: unhandled StageType {stage}");
            }
        }

        private float CalculateServerUtilisationRate(float serverTotalBusyTime)
        {
            if (SimClock.Instance.TotalSimTimeElapsed <= 0)
                return 0;

            return (serverTotalBusyTime / SimClock.Instance.TotalSimTimeElapsed) * 100;
        }

        private void UpdateTravellerCompletedData(TravellerEntity traveller)
        {
            TotalTravellersCompleted++;

            // Avg total time in system
            float timeInSystem = SimClock.Instance.TotalSimTimeElapsed - traveller.SpawnTime;
            totalTimeInSystem += timeInSystem;
            AvgTotalTimeInSystem = totalTimeInSystem / TotalTravellersCompleted;

            // % exceeding threshold
            float totalWaitTime = 0;
            foreach (var timings in traveller.Timings.Values)
            {
                totalWaitTime += timings.ServiceStartTime - timings.QueueJoinTime;
            }
            if (totalWaitTime > simConfig.WaitThreshold)
            {
                travellersAboveThreshold++;
            }
            PercentageAboveWaitThreshold = (float)travellersAboveThreshold / TotalTravellersCompleted * 100f;
        }

        private void OnDestroy()
        {
            securityQueueEventChannel.OnTravellerLeftQueue -= UpdateSecurityAvgWaitTime;
            immigrationQueueEventChannel.OnTravellerLeftQueue -= UpdateImmigrationAvgWaitTime;
            finalStageServerEventChannel.OnTravellerExitingService -= UpdateTravellerCompletedData;
        }
    }
}
