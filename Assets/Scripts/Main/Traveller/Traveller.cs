using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main.Traveller
{
    public class Traveller
    {
        private static int nextId = 0;
        public int Id { get; private set; }
        public float SpawnTime { get; private set; }
        /// <summary>
        /// Holds all the important timings for a certain check stage e.g. Security - QueueJoinTime, ServerStartTime, ServerEndTime
        /// </summary>
        public Dictionary<CheckType, CheckTimings> Timings { get; private set; }
        public TravellerType Type { get; private set; }

        public Traveller(TravellerType travellerType)
        {
            Id = nextId;
            nextId++;
            SpawnTime = SimClock.Instance.TotalSimTimeElapsed;
            Timings = new Dictionary<CheckType, CheckTimings>();
            Type = travellerType;
        }

        public void RecordQueueJoin(CheckType check)
        {
            EnsureCheckAvailable(check);
            Timings[check].QueueJoinTime = SimClock.Instance.TotalSimTimeElapsed;
        }

        public void RecordServiceStart(CheckType check)
        {
            EnsureCheckAvailable(check);
            Timings[check].ServiceStartTime = SimClock.Instance.TotalSimTimeElapsed;
        }

        public void RecordServiceEnd(CheckType check)
        {
            EnsureCheckAvailable(check);
            Timings[check].ServiceEndTime = SimClock.Instance.TotalSimTimeElapsed;
        }

        private void EnsureCheckAvailable(CheckType check)
        {
            if (!Timings.ContainsKey(check))
            {
                Timings[check] = new CheckTimings();
            }
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
