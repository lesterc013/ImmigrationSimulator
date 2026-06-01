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

        private int immigrationTravellersTotal;
        private float immigrationTravellersWaitTotal;
        public float ImmigrationAvgWaitTime { get; private set; }

        // Server utilisation
        public float SecurityServerUtilisationRate { get; private set; }
        public float ImmigrationServerUtilisationRate { get; private set; }

        // Global metrics
        public int TotalTravellersCompleted { get; private set; }
        public float AvgTotalTimeInSystem { get; private set; }
        public float TravellersClearedPerMinute { get; private set; }
        public float PercentageAboveWaitThreshold { get; private set; }

        private float totalTimeInSystem;
        private int travellersAboveThreshold;

        private void Awake()
        {
            securityQueueEventChannel.OnTravellerLeftQueue += UpdateSecurityAvgWaitTime;
            immigrationQueueEventChannel.OnTravellerLeftQueue += UpdateImmigrationAvgWaitTime;
            finalStageServerEventChannel.OnTravellerExitingService += HandleTravellerCompletedData;
        }

        private void Update()
        {
            if (SimClock.Instance.IsFinished) 
                return;

            SecurityServerUtilisationRate = CalculateServerUtilisationRate(securityServerManager.GetTotalServersBusyTime());
            ImmigrationServerUtilisationRate = CalculateServerUtilisationRate(immigrationServerManager.GetTotalServersBusyTime());
            if (TotalTravellersCompleted > 0)
            {
                TravellersClearedPerMinute = TotalTravellersCompleted / (SimClock.Instance.TotalSimTimeElapsed / 60f);
            }
        }

        private void UpdateSecurityAvgWaitTime(TravellerEntity securityTraveller)
        {
            UpdateStageWaitTime(StageType.Security, securityTraveller);
            // TODO: Update the TMP Text display
        }

        private void UpdateImmigrationAvgWaitTime(TravellerEntity securityTraveller)
        {
            UpdateStageWaitTime(StageType.Immigration, securityTraveller);
            // TODO: Update the TMP Text display
        }

        private void UpdateStageWaitTime(StageType stage, TravellerEntity traveller)
        {
            float waitTime = traveller.Timings[stage].ServiceStartTime - traveller.Timings[stage].QueueJoinTime;
            if (stage == StageType.Security)
            {
                securityTravellersTotal++;
                securityTravellersWaitTotal += waitTime;
                SecurityAvgWaitTime = securityTravellersWaitTotal / securityTravellersTotal;
                Debug.Log($"[Analytics] Security traveller {traveller.Id} wait: {waitTime:F2}s | Running avg: {SecurityAvgWaitTime:F2}s | Count: {securityTravellersTotal}");
            }
            else if (stage == StageType.Immigration)
            {
                immigrationTravellersTotal++;
                immigrationTravellersWaitTotal += waitTime;
                ImmigrationAvgWaitTime = immigrationTravellersWaitTotal / immigrationTravellersTotal;
                Debug.Log($"[Analytics] Immigration traveller {traveller.Id} wait: {waitTime:F2}s | Running avg: {ImmigrationAvgWaitTime:F2}s | Count: {immigrationTravellersTotal}");
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

        private void HandleTravellerCompletedData(TravellerEntity traveller)
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

            Debug.Log($"[Analytics] Traveller {traveller.Id} completed | TimeInSystem: {timeInSystem:F2}s | TotalWaitTime: {totalWaitTime:F2}s | AboveThreshold: {totalWaitTime > simConfig.WaitThreshold} | % Above: {PercentageAboveWaitThreshold:F1}% | Throughput: {TravellersClearedPerMinute:F2}/min");
        }

        private void OnDestroy()
        {
            securityQueueEventChannel.OnTravellerLeftQueue -= UpdateSecurityAvgWaitTime;
            immigrationQueueEventChannel.OnTravellerLeftQueue -= UpdateImmigrationAvgWaitTime;
            finalStageServerEventChannel.OnTravellerExitingService -= HandleTravellerCompletedData;
        }
    }
}
