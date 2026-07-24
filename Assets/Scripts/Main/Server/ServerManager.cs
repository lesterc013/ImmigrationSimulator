using ImmigrationSim.Core;
using ImmigrationSim.Main.QueueController;
using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main.Server
{
    public class ServerManager : MonoBehaviour
    {
        [SerializeField] private SimConfig simConfig;
        [SerializeField] private StageType stageType;

        [SerializeField]
        [Tooltip("Drag in the corresponding QueueControllerEventChannel to stageType.")]
        private QueueControllerEventChannel queueControllerEventChannel;

        [SerializeField]
        [Tooltip("Drag in the corresponding ServerEventChannel to stageType.")]
        private ServerEventChannel serverEventChannel;

        [SerializeField] private GameObject serverPrefab;
        [SerializeField]
        [Tooltip("Drag in the corresponding StageType server grid layout in the scene.")]
        private GameObject serverGridLayout;

        private int numberOfServers;
        private List<ServerEntity> servers;

        public void Init()
        {
            if (stageType == StageType.Security)
            {
                numberOfServers = simConfig.SecurityServerCount;
            }
            else if (stageType == StageType.Immigration)
            {
                numberOfServers = simConfig.ImmigrationServerCount;
            }
            else
            {
                Debug.LogError($"ServerManager: unhandled StageType {stageType}");
                numberOfServers = 1;
            }

            servers = new List<ServerEntity>();

            for (int i = 0; i < numberOfServers; i++)
            {
                var server = Instantiate(serverPrefab, serverGridLayout.transform);
                var serverScript = server.GetComponent<ServerEntity>();
                serverScript.Init(i+1, stageType, queueControllerEventChannel, serverEventChannel);
                server.name = $"{stageType} Server #{serverScript.Id}";
                servers.Add(serverScript);
            }
        }

        public bool FindFreeServer(out IServer freeServer)
        {
            foreach (var serverEntity in servers)
            {
                if (serverEntity.IsAvailable)
                {
                    freeServer = serverEntity;
                    return true;
                }
            }

            freeServer = null;
            return false;
        }

        public float GetAverageServersBusyTime()
        {
            float totalBusyTime = 0;
            foreach (var serverEntity in servers)
            {
                totalBusyTime += serverEntity.BusyTime;
            }
            return totalBusyTime / servers.Count;
        }
    }
}
