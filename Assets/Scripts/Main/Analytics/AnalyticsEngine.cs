using ImmigrationSim.Main.QueueController;
using UnityEngine;

namespace ImmigrationSim.Main.Analytics
{
    public class AnalyticsEngine : MonoBehaviour
    {
        // Need to know each queue's length and when a traveller has left a queue.
        [SerializeField] private QueueControllerEventChannel securityQueueControllerEventChannel;
        [SerializeField] private QueueControllerEventChannel immigrationQueueControllerEventChannel;

        private void Awake()
        {
            
        }
    }
}
