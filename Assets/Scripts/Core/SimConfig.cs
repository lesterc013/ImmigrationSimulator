using UnityEngine;
namespace ImmigrationSim.Core
{
    [CreateAssetMenu(fileName = "SimConfig", menuName = "Scriptable Objects/SimConfig")]
    public class SimConfig : ScriptableObject
    {
        [field: Header("General")]
        [field: SerializeField] public float TravellersPerSecond { get; private set; }
        [field: SerializeField] public float SimSpeed { get; private set; }
        [field: SerializeField] public float SimDuration { get; private set; }
        [field: SerializeField] public float CitizenRatio { get; private set; }

        [field: Header("Security")]
        [field: SerializeField] public int SecurityServerCount { get; private set; }
        [field: SerializeField] public float CitizenMinSecurityProcessingTime { get; private set; }
        [field: SerializeField] public float CitizenMaxSecurityProcessingTime { get; private set; }
        [field: SerializeField] public float ForeignerMinSecurityProcessingTime { get; private set; }
        [field: SerializeField] public float ForeignerMaxSecurityProcessingTime { get; private set; }

        [field: Header("Immigration")]
        [field: SerializeField] public int ImmigrationServerCount { get; private set; }
        [field: SerializeField] public float CitizenMinImmigrationProcessingTime { get; private set; }
        [field: SerializeField] public float CitizenMaxImmigrationProcessingTime { get; private set; }
        [field: SerializeField] public float ForeignerMinImmigrationProcessingTime { get; private set; }
        [field: SerializeField] public float ForeignerMaxImmigrationProcessingTime { get; private set; }
        [field: SerializeField] public float SecondaryScreeningProbability { get; private set; }
        [field: SerializeField] public float MinSecondaryScreeningTime { get; private set; }
        [field: SerializeField] public float MaxSecondaryScreeningTime { get; private set; }


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
            float citizenRatio,
            float secondaryScreeningProbability,
            float minSecondaryScreeningTime,
            float maxSecondaryScreeningTime)
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
            SecondaryScreeningProbability = secondaryScreeningProbability;
            MinSecondaryScreeningTime = minSecondaryScreeningTime;
            MaxSecondaryScreeningTime = maxSecondaryScreeningTime;
        }

        /// <summary>
        /// Return a uniformly distributed processing time based on the Citizen/Foreigner and StageType.
        /// Returned time is based on the input params set before the start of the sim.
        /// </summary>
        /// <param name="travellerType"></param>
        /// <param name="stageType"></param>
        /// <returns></returns>
        public float GetProcessingTime(TravellerType travellerType, StageType stageType)
        {
            float min, max;
            if (stageType == StageType.Security)
            {
                min = travellerType == TravellerType.Citizen
                    ? CitizenMinSecurityProcessingTime
                    : ForeignerMinSecurityProcessingTime;
                max = travellerType == TravellerType.Citizen
                    ? CitizenMaxSecurityProcessingTime
                    : ForeignerMaxSecurityProcessingTime;
            }
            else
            {
                min = travellerType == TravellerType.Citizen
                    ? CitizenMinImmigrationProcessingTime
                    : ForeignerMinImmigrationProcessingTime;
                max = travellerType == TravellerType.Citizen
                    ? CitizenMaxImmigrationProcessingTime
                    : ForeignerMaxImmigrationProcessingTime;
            }
            return Random.Range(min, max);
        }

        public float GetSecondaryScreeningTime()
        {
            return Random.Range(MinSecondaryScreeningTime, MaxSecondaryScreeningTime);
        }
    }
}