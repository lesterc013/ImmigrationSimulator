using ImmigrationSim.Core;
using UnityEngine;

namespace ImmigrationSim.Main
{
    public class SimClock : MonoBehaviour
    {
        private static SimClock _instance;
        public static SimClock Instance { get { return _instance; } }

        [SerializeField]
        private SimConfig simConfig;

        private float simDuration;
        private bool continueRunning;
        private float simSpeed;

        /// <summary>
        /// Return the deltaTime multiplied by the simSpeed.
        /// </summary>
        public float SimDeltaTime
        {
            get 
            { 
                if (!continueRunning)
                {
                    return 0;
                }

                return Time.deltaTime * simSpeed; 
            }
        }

        public float TotalSimTimeElapsed
        {
            get; private set;
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                simDuration = simConfig.SimDuration;
                continueRunning = true;
                simSpeed = simConfig.SimSpeed;
            }
        }

        private void Update()
        {
            if (!continueRunning) 
            { 
                return; 
            }

            TotalSimTimeElapsed += SimDeltaTime;

            if (TotalSimTimeElapsed >= simDuration)
            {
                continueRunning = false;
            }
        }
    }
}
