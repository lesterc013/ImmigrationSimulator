using UnityEngine;

namespace ImmigrationSim.Main
{
    public class SimClock : MonoBehaviour
    {
        private static SimClock _instance;

        public static SimClock Instance { get { return _instance; } }

        [SerializeField]
        [Tooltip("Multiplier for the Sim Clock. Value of 60 would mean 1s irl = 60s SimTime")]
        private float simSpeed = 1f;

        /// <summary>
        /// Return the deltaTime multiplied by the simSpeed.
        /// </summary>
        public float SimDeltaTime
        {
            get { return Time.deltaTime * simSpeed; }
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
            }
        }

        private void Update()
        {
            TotalSimTimeElapsed += SimDeltaTime;
        }
    }
}
