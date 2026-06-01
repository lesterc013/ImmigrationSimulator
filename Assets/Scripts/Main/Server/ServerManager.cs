using ImmigrationSim.Core;
using ImmigrationSim.Main.Server;
using ImmigrationSim.Main.Traveller;
using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main
{
    public class ServerManager : MonoBehaviour
    {
        [SerializeField]
        private StageType checkType;

        [SerializeField]
        [Tooltip("Drag in the corresponding TravellerEventChannel to checkType.")]
        private TravellerEventChannel travellerEventChannel;

        [SerializeField]
        private GameObject serverPrefab;

        [SerializeField]
        private SimConfig simConfig;

        private int numberOfServers;
        private List<ServerEntity> servers;

        private void Awake()
        {
            if (checkType == StageType.Security)
            {
                numberOfServers = simConfig.SecurityServerCount;
            }
            else if (checkType == StageType.Immigration)
            {
                numberOfServers = simConfig.ImmigrationServerCount;
            }
            else
            {
                Debug.LogError($"ServerManager: unhandled CheckType {checkType}");
                numberOfServers = 1;
            }

            servers = new List<ServerEntity>();

            for (int i = 0; i < numberOfServers; i++)
            {
                var server = Instantiate(serverPrefab, transform);
                var serverScript = server.GetComponent<ServerEntity>();
                serverScript.Init(checkType, travellerEventChannel);
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
