using UnityEngine;
namespace ImmigrationSim.Core
{
    [CreateAssetMenu(fileName = "SimConfig", menuName = "Scriptable Objects/SimConfig")]
    public class SimConfig : ScriptableObject
    {
        [field: SerializeField] public float TravellersPerSecond { get; private set; }
        [field: SerializeField] public float SimSpeed { get; private set; }
        [field: SerializeField] public float SimDuration { get; private set; }

        [field: SerializeField] public int SecurityServerCount { get; private set; }
        [field: SerializeField] public float CitizenMinSecurityProcessingTime { get; private set; }
        [field: SerializeField] public float CitizenMaxSecurityProcessingTime { get; private set; }
        [field: SerializeField] public float ForeignerMinSecurityProcessingTime { get; private set; }
        [field: SerializeField] public float ForeignerMaxSecurityProcessingTime { get; private set; }

        [field: SerializeField] public int ImmigrationServerCount { get; private set; }
        [field: SerializeField] public float CitizenMinImmigrationProcessingTime { get; private set; }
        [field: SerializeField] public float CitizenMaxImmigrationProcessingTime { get; private set; }
        [field: SerializeField] public float ForeignerMinImmigrationProcessingTime { get; private set; }
        [field: SerializeField] public float ForeignerMaxImmigrationProcessingTime { get; private set; }

        [field: SerializeField] public float CitizenRatio { get; private set; }

        //[field: SerializeField] public float SecondaryScreeningProbability { get; private set; }

        public void ApplyConfig(
            float travellersPerSecond,
            float simSpeed,
            float simDuration,
            int securityServerCount,
            float minSecurityProcessingTimeCitizen,
            float maxSecurityProcessingTimeCitizen,
            float minSecurityProcessingTimeForeigner,
            float maxSecurityProcessingTimeForeigner,
            int immigrationServerCount,
            float minImmigrationProcessingTimeCitizen,
            float maxImmigrationProcessingTimeCitizen,
            float minImmigrationProcessingTimeForeigner,
            float maxImmigrationProcessingTimeForeigner,
            float citizenRatio)
        {
            TravellersPerSecond = travellersPerSecond;
            SimSpeed = simSpeed;
            SimDuration = simDuration;
            SecurityServerCount = securityServerCount;
            CitizenMinSecurityProcessingTime = minSecurityProcessingTimeCitizen;
            CitizenMaxSecurityProcessingTime = maxSecurityProcessingTimeCitizen;
            ForeignerMinSecurityProcessingTime = minSecurityProcessingTimeForeigner;
            ForeignerMaxSecurityProcessingTime = maxSecurityProcessingTimeForeigner;
            ImmigrationServerCount = immigrationServerCount;
            CitizenMinImmigrationProcessingTime = minImmigrationProcessingTimeCitizen;
            CitizenMaxImmigrationProcessingTime = maxImmigrationProcessingTimeCitizen;
            ForeignerMinImmigrationProcessingTime = minImmigrationProcessingTimeForeigner;
            ForeignerMaxImmigrationProcessingTime = maxImmigrationProcessingTimeForeigner;
            CitizenRatio = citizenRatio;
        }
    }
}