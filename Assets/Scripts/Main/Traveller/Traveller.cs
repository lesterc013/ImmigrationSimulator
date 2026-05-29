using UnityEngine;

namespace ImmigrationSim.Main.Traveller
{
    public class Traveller
    {
        private static int nextId = 0;
        public int Id { get; private set; }
        //needs Id, spawnTime, queueJoinTime, serviceStartTime, TravellerType enum
        public float SpawnTime { get; private set; }
        // Is it will need to have 2 different sets of QueueJoin and ServiceStart times? Not very scalable right?
        // Or should I use a Dictionary to update a Traveller's timings?
        public float QueueJoinTime { get; private set; }
        public float ServiceStartTime { get; private set; }

        // TODO: TravellerType Citizen or Foreigner

        public Traveller()
        {
            Id = nextId;
            nextId++;
            SpawnTime = SimClock.Instance.TotalSimTimeElapsed;
        }

        public void SetQueueJoinTime(float time)
        {
            QueueJoinTime = time;
        }

        public void SetServiceStartTime(float time)
        {
            ServiceStartTime = time;
        }

        /// <summary>
        /// To be called when resetting the simulation.
        /// </summary>
        public static void ResetTravellerId()
        {
            nextId = 0;
        }
    }
}
