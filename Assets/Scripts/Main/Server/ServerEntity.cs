using ImmigrationSim.Main.Traveller;
using UnityEngine;

namespace ImmigrationSim.Main.Server
{
    public class ServerEntity : MonoBehaviour, IServer
    {
        //Fields it needs:
        private TravellerEntity currentTraveller;
        private float remainingTime;
        private CheckType checkType; // which stage this server belongs to
        public event Action<TravellerEntity> OnServerFreed;
        public bool IsAvailable { get; private set; } = true;

        // Implements IServer.

        // Responsibilities:

        // Hold current Traveller being processed and a countdown timer
        // On Assign: mark unavailable, stamp RecordServiceStart on Traveller, draw processing time from ProcessingTimeGenerator
        // On Update: tick countdown down using SimClock.Instance.SimDeltaTime
        // When countdown hits zero: stamp RecordServiceEnd, raise OnServerFreed event carrying the completed Traveller, clear internal state and mark available again
    }
}
