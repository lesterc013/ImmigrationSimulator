using ImmigrationSim.Core;
using ImmigrationSim.Main.Server;
using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main
{
    public class ServerManager : MonoBehaviour
    {
        [SerializeField]
        private CheckType checkType;

        [SerializeField]
        private GameObject serverPrefab;

        [SerializeField]
        private SimConfig simConfig;

        private int numberOfServers;
        private List<ServerEntity> servers;

        private void Awake()
        {
            if (checkType == CheckType.Security)
            {
                numberOfServers = simConfig.SecurityServerCount;
            }
            else if (checkType == CheckType.Immigration)
            {
                numberOfServers = simConfig.ImmigrationServerCount;
            }
            else
            {
                // Just set default as the num of security server counts.
                numberOfServers = simConfig.SecurityServerCount;
            }

            servers = new List<ServerEntity>();

            for (int i = 0; i < numberOfServers; i++)
            {
                var server = Instantiate(serverPrefab, transform);
                var serverScript = server.GetComponent<ServerEntity>();
                serverScript.Init(checkType);
                servers.Add(serverScript);
            }
        }

        public void ResetServerManager()
        {
            // Destroy all the created servers
            // Clear the script
        }
    }
}
