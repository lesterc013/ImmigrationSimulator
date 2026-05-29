using UnityEngine;

namespace ImmigrationSim.Main.TravellerSpawner
{
    public class TravellerSpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Arrival per second")]
        private float lambda = 1f;

        private float timeTillSpawn;

        private void Awake()
        {
            timeTillSpawn = GetTimeTillSpawn();
        }

        // Note: Because we are using a frame-based Update, there will be "timer overshoots" for high sim speeds i.e. timeTillSpawn becomes lesser than 0 hence presenting possibility of "missing" a next spawn.
        // This tradeoff is accepted as the inter-arrival times are already drawn from an exponential distribution and a small overshoot per spawn is negligible enough.
        private void Update()
        {
            timeTillSpawn -= SimClock.Instance.SimDeltaTime;

            if (timeTillSpawn <= 0)
            {
                SpawnTraveller();
            }
        }

        private void SpawnTraveller()
        {
            // After spawning, reset the countdown timer with a cumulative timing to help with the spawn overshoots.
            timeTillSpawn += GetTimeTillSpawn();
        }

        private float GetTimeTillSpawn()
        {
            return -Mathf.Log(1 - Random.Range(0, 0.999999f)) / lambda;
        }
    }
}
