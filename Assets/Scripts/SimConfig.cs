using UnityEngine;

namespace ImmigrationSim
{
    [CreateAssetMenu(fileName = "SimConfig", menuName = "Scriptable Objects/SimConfig")]
    public class SimConfig : ScriptableObject
    {
        public float TravellersPerSecond;
        public float SimSpeed; // multiplier
        public float SimDuration; // no. of sim seconds
        public int SecurityServerCount; // 1 - 10
        public int ImmigrationServerCount; // 1 - 10
        public float CitizenRatio; // 0 - 100
    }
}
