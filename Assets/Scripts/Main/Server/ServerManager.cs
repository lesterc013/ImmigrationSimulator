using ImmigrationSim.Main.Server;
using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main
{
    public class ServerManager : MonoBehaviour
    {
        [SerializeField]
        private int numberOfServers;

        [SerializeField]
        private CheckType checkType;

        [SerializeField]
        private GameObject serverPrefab;

        private List<ServerEntity> servers;

        private void Awake()
        {
            servers = new List<ServerEntity>();

            for (int i = 0; i < numberOfServers; i++)
            {
                var server = Instantiate(serverPrefab, transform);
                var serverScript = server.GetComponent<ServerEntity>();
                serverScript.Init(checkType);
                servers.Add(serverScript);
            }
        }
    }
}
