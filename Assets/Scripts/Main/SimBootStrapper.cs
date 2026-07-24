using ImmigrationSim.Main.Analytics;
using ImmigrationSim.Main.QueueController;
using ImmigrationSim.Main.Server;
using ImmigrationSim.Main.Traveller;
using UnityEngine;

namespace ImmigrationSim.Main
{
    /// <summary>
    /// To init every component in the required order.
    /// This was created to solve a classic race-condition "Editor vs Build runs differently issue".
    /// Because every component now uses Awake and Start to TRY and order their inits. But Build vs Editor runs script orders differently.
    /// </summary>
    public class SimBootStrapper : MonoBehaviour
    {
        [SerializeField] private SimClock simClock;
        [SerializeField] private ServerManager securityServerManager;
        [SerializeField] private ServerManager immigrationServerManager;
        [SerializeField] private QueueControllerEntity securityQueueController;
        [SerializeField] private QueueControllerEntity immigrationQueueController;
        [SerializeField] private StageConnector stageConnector;
        [SerializeField] private AnalyticsEngine analyticsEngine;
        [SerializeField] private TravellerSpawner travellerSpawner;

        private void Awake()
        {
            // Call Init() on every MonoBehaviour that uses Awake
            simClock.Init();
        }
    }
}
