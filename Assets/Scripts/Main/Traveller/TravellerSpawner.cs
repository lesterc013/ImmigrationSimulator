using ImmigrationSim.Core;
using ImmigrationSim.Main.QueueController;
using UnityEngine;

namespace ImmigrationSim.Main.Traveller
{
    public class TravellerSpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Drag the stage's QueueControllerEventChannel here.")]
        private QueueControllerEventChannel firstQueueControllerEventChannel;

        [SerializeField]
        private SimConfig simConfig;

        private float travellersPerSecond;
        private float citizenForeignerSplit;

        private float timeTillSpawn;

        public void Init()
        {
            travellersPerSecond = simConfig.TravellersPerSecond;
            citizenForeignerSplit = simConfig.CitizenRatio;
            timeTillSpawn = GetTimeTillSpawn();
        }

        // Note: Because we are using a frame-based Update, there will be "timer overshoots" for high sim speeds i.e. timeTillSpawn becomes lesser than 0 hence presenting possibility of "missing" a next spawn.
        // This tradeoff is accepted as the inter-arrival times are already drawn from an exponential distribution and a small overshoot per spawn is negligible enough.
        private void Update()
        {
            timeTillSpawn -= SimClock.Instance.SimDeltaTime;

            // At high SimSpeeds, SimDeltaTime might be so huge that timeTillSpawn cannot recover i.e. becomes super big -ve number,
            // Using a "while" instead of "if" changes behaviour to "I'll keep processing until I've caught up with however much time just passed"
            while (timeTillSpawn <= 0)
            {
                SpawnTraveller();
            }
        }

        private void SpawnTraveller()
        {

            var newTraveller = new TravellerEntity(GetTravellerType());
            firstQueueControllerEventChannel.RaiseNewTravellerArrived(newTraveller);

            // After spawning, reset the countdown timer with a cumulative timing to help with the spawn overshoots.
            timeTillSpawn += GetTimeTillSpawn();
            Debug.Log($"New timeTillSpawn {timeTillSpawn}");
        }

        private TravellerType GetTravellerType()
        {
            var diceRoll = Random.Range(0f, 100f);
            if (diceRoll <= citizenForeignerSplit)
            {
                return TravellerType.Citizen;
            }
            else
            {
                return TravellerType.Foreigner;
            }
        }

        private float GetTimeTillSpawn()
        {
            return -Mathf.Log(1 - Random.Range(0, 0.999999f)) / travellersPerSecond;
        }
    }
}
