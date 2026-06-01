using ImmigrationSim.Core;
using ImmigrationSim.Main.QueueController;
using ImmigrationSim.Main.Server;
using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main
{
    public class ServerManager : MonoBehaviour
    {
        [SerializeField]
        private StageType stageType;

        [SerializeField]
        [Tooltip("Drag in the corresponding QueueControllerEventChannel to stageType.")]
        private QueueControllerEventChannel queueControllerEventChannel;

        [SerializeField]
        [Tooltip("Drag in the corresponding ServerEventChannel to stageType.")]
        private ServerEventChannel serverEventChannel;

        [SerializeField]
        private GameObject serverPrefab;

        [SerializeField]
        private SimConfig simConfig;

        private int numberOfServers;
        private List<ServerEntity> servers;

        private void Awake()
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
                var server = Instantiate(serverPrefab, transform);
                var serverScript = server.GetComponent<ServerEntity>();
                serverScript.Init(stageType, queueControllerEventChannel, serverEventChannel);
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
    }
}
