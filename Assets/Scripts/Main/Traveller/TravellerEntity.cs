using ImmigrationSim.Core;
using System.Collections.Generic;
using UnityEngine;

namespace ImmigrationSim.Main.Traveller
{
    public class TravellerEntity
    {
        private static int nextId = 0;
        public int Id { get; private set; }
        public float SpawnTime { get; private set; }
        /// <summary>
        /// Holds all the important timings for a certain check stage e.g. Security - QueueJoinTime, ServerStartTime, ServerEndTime
        /// </summary>
        public Dictionary<StageType, StageTimings> Timings { get; private set; }
        public TravellerType Type { get; private set; }

        public TravellerEntity(TravellerType travellerType)
        {
            Id = nextId;
            nextId++;
            SpawnTime = SimClock.Instance.TotalSimTimeElapsed;
            Timings = new Dictionary<StageType, StageTimings>();
            Type = travellerType;
        }

        public void RecordQueueJoin(StageType check)
        {
            EnsureCheckAvailable(check);
            Timings[check].QueueJoinTime = SimClock.Instance.TotalSimTimeElapsed;
        }

        public void RecordServiceStart(StageType check)
        {
            EnsureCheckAvailable(check);
            Timings[check].ServiceStartTime = SimClock.Instance.TotalSimTimeElapsed;
        }

        public void RecordServiceEnd(StageType check)
        {
            EnsureCheckAvailable(check);
            Timings[check].ServiceEndTime = SimClock.Instance.TotalSimTimeElapsed;
        }

        private void EnsureCheckAvailable(StageType check)
        {
            if (!Timings.ContainsKey(check))
            {
                Timings[check] = new StageTimings();
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
