using ImmigrationSim.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ImmigrationSim.StartMenu
{
    public class FormHandler : MonoBehaviour
    {
        [SerializeField]
        private SimConfig simConfig;

        [Header("General")]
        [SerializeField] private TMP_InputField travellersPerSecondField;
        [SerializeField] private TMP_InputField simSpeedField;
        [SerializeField] private TMP_InputField simDurationField;
        [SerializeField] private TMP_InputField citizenRatioField;
        [SerializeField] private TMP_InputField waitThresholdField;

        [Header("Security")]
        [SerializeField] private TMP_InputField securityServerCountField;
        [SerializeField] private TMP_InputField minSecurityProcessingTimeCitizenField;
        [SerializeField] private TMP_InputField maxSecurityProcessingTimeCitizenField;
        [SerializeField] private TMP_InputField minSecurityProcessingTimeForeginerField;
        [SerializeField] private TMP_InputField maxSecurityProcessingTimeForeginerField;

        [Header("Immigration")]
        [SerializeField] private TMP_InputField immigrationServerCountField;
        [SerializeField] private TMP_InputField minImmigrationProcessingTimeCitizenField;
        [SerializeField] private TMP_InputField maxImmigrationProcessingTimeCitizenField;
        [SerializeField] private TMP_InputField minImmigrationProcessingTimeForeginerField;
        [SerializeField] private TMP_InputField maxImmigrationProcessingTimeForeginerField;
        [SerializeField] private TMP_InputField secondaryScreeningProbabilityField;
        [SerializeField] private TMP_InputField minSecondaryScreeningTimeField;
        [SerializeField] private TMP_InputField maxSecondaryScreeningTimeField;

        private void Awake()
        {
            PopulateForm();
        }

        private void PopulateForm()
        {
            travellersPerSecondField.text = simConfig.TravellersPerSecond.ToString();
            simSpeedField.text = simConfig.SimSpeed.ToString();
            simDurationField.text = simConfig.SimDuration.ToString();
            citizenRatioField.text = simConfig.CitizenRatio.ToString();
            waitThresholdField.text = simConfig.WaitThreshold.ToString();

            securityServerCountField.text = simConfig.SecurityServerCount.ToString();
            minSecurityProcessingTimeCitizenField.text = simConfig.CitizenMinSecurityProcessingTime.ToString();
            maxSecurityProcessingTimeCitizenField.text = simConfig.CitizenMaxSecurityProcessingTime.ToString();
            minSecurityProcessingTimeForeginerField.text = simConfig.ForeignerMinSecurityProcessingTime.ToString();
            maxSecurityProcessingTimeForeginerField.text = simConfig.ForeignerMaxSecurityProcessingTime.ToString();

            immigrationServerCountField.text = simConfig.ImmigrationServerCount.ToString();
            minImmigrationProcessingTimeCitizenField.text = simConfig.CitizenMinImmigrationProcessingTime.ToString();
            maxImmigrationProcessingTimeCitizenField.text = simConfig.CitizenMaxImmigrationProcessingTime.ToString();
            minImmigrationProcessingTimeForeginerField.text = simConfig.ForeignerMinImmigrationProcessingTime.ToString();
            maxImmigrationProcessingTimeForeginerField.text = simConfig.ForeignerMaxImmigrationProcessingTime.ToString();
            secondaryScreeningProbabilityField.text = simConfig.SecondaryScreeningProbability.ToString();
            minSecondaryScreeningTimeField.text = simConfig.MinSecondaryScreeningTime.ToString();
            maxSecondaryScreeningTimeField.text = simConfig.MaxSecondaryScreeningTime.ToString();
        }

        public void StartSim()
        {
            WriteToConfig();
            SceneManager.LoadScene("Main");
        }

        private void WriteToConfig()
        {
            float.TryParse(travellersPerSecondField.text, out float tps);
            float.TryParse(simSpeedField.text, out float simSpeed);
            float.TryParse(simDurationField.text, out float simDuration);
            float.TryParse(citizenRatioField.text, out float citizenRatio);
            float.TryParse(waitThresholdField.text, out float waitThreshold);

            int.TryParse(securityServerCountField.text, out int securityCount);
            float.TryParse(minSecurityProcessingTimeCitizenField.text, out float minSecCitizen);
            float.TryParse(maxSecurityProcessingTimeCitizenField.text, out float maxSecCitizen);
            float.TryParse(minSecurityProcessingTimeForeginerField.text, out float minSecForeigner);
            float.TryParse(maxSecurityProcessingTimeForeginerField.text, out float maxSecForeigner);

            int.TryParse(immigrationServerCountField.text, out int immigrationCount);
            float.TryParse(minImmigrationProcessingTimeCitizenField.text, out float minImmCitizen);
            float.TryParse(maxImmigrationProcessingTimeCitizenField.text, out float maxImmCitizen);
            float.TryParse(minImmigrationProcessingTimeForeginerField.text, out float minImmForeigner);
            float.TryParse(maxImmigrationProcessingTimeForeginerField.text, out float maxImmForeigner);
            float.TryParse(secondaryScreeningProbabilityField.text, out float secondaryScreeningProbability);
            float.TryParse(minSecondaryScreeningTimeField.text, out float minSecondaryScreeningTime);
            float.TryParse(maxSecondaryScreeningTimeField.text, out float maxSecondaryScreeningTime);


            simConfig.ApplyConfig(
                travellersPerSecond: tps,
                simSpeed: simSpeed,
                simDuration: simDuration,
                securityServerCount: securityCount,
                minSecurityProcessingTimeCitizen: minSecCitizen,
                maxSecurityProcessingTimeCitizen: maxSecCitizen,
                minSecurityProcessingTimeForeigner: minSecForeigner,
                maxSecurityProcessingTimeForeigner: maxSecForeigner,
                immigrationServerCount: immigrationCount,
                minImmigrationProcessingTimeCitizen: minImmCitizen,
                maxImmigrationProcessingTimeCitizen: maxImmCitizen,
                minImmigrationProcessingTimeForeigner: minImmForeigner,
                maxImmigrationProcessingTimeForeigner: maxImmForeigner,
                citizenRatio: citizenRatio,
                waitThreshold: waitThreshold,
                secondaryScreeningProbability: secondaryScreeningProbability,
                minSecondaryScreeningTime: minSecondaryScreeningTime,
                maxSecondaryScreeningTime: maxSecondaryScreeningTime
            );
        }
    }
}
