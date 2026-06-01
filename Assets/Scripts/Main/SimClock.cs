using ImmigrationSim.Core;
using UnityEngine;

namespace ImmigrationSim.Main
{
    public class SimClock : MonoBehaviour
    {
        private static SimClock _instance;
        public static SimClock Instance { get { return _instance; } }
        public bool IsFinished { get; private set; }

        [SerializeField]
        private SimConfig simConfig;

        private float simDuration;
        private float simSpeed;

        /// <summary>
        /// Return the deltaTime multiplied by the simSpeed.
        /// </summary>
        public float SimDeltaTime
        {
            get 
            { 
                if (IsFinished)
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
                IsFinished = false;
                simSpeed = simConfig.SimSpeed;
            }
        }

        private void Update()
        {
            if (IsFinished) 
            { 
                return; 
            }

            TotalSimTimeElapsed += SimDeltaTime;

            if (TotalSimTimeElapsed >= simDuration)
            {
                IsFinished = true;
            }
        }
    }
}
