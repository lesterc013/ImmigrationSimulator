using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.STP;

namespace ImmigrationSim.Setup
{
    public class FormHandler : MonoBehaviour
    {
        [SerializeField]
        private SimConfig simConfig;

        [Header("Input Fields")]
        [SerializeField] private TMP_InputField travellersPerSecondField;
        [SerializeField] private TMP_InputField simSpeedField;
        [SerializeField] private TMP_InputField simDurationField;
        [SerializeField] private TMP_InputField securityServerCountField;
        [SerializeField] private TMP_InputField immigrationServerCountField;
        [SerializeField] private TMP_InputField citizenRatioField;

        private void Awake()
        {
            PopulateForm();
        }

        private void PopulateForm()
        {
            travellersPerSecondField.text = simConfig.TravellersPerSecond.ToString();
            simSpeedField.text = simConfig.SimSpeed.ToString();
            simDurationField.text = simConfig.SimDuration.ToString();
            securityServerCountField.text = simConfig.SecurityServerCount.ToString();
            immigrationServerCountField.text = simConfig.ImmigrationServerCount.ToString();
            citizenRatioField.text = simConfig.CitizenRatio.ToString();
        }

        public void StartSim()
        {
            WriteToConfig();
            SceneManager.LoadScene("Main");
        }

        private void WriteToConfig()
        {
            float.TryParse(travellersPerSecondField.text, out simConfig.TravellersPerSecond);
            float.TryParse(simSpeedField.text, out simConfig.SimSpeed);
            float.TryParse(simDurationField.text, out simConfig.SimDuration);
            int.TryParse(securityServerCountField.text, out simConfig.SecurityServerCount);
            int.TryParse(immigrationServerCountField.text, out simConfig.ImmigrationServerCount);
            float.TryParse(citizenRatioField.text, out simConfig.CitizenRatio);
        }
    }
}
