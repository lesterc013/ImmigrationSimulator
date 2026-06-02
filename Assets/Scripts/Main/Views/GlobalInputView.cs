using ImmigrationSim.Core;
using TMPro;
using UnityEngine;

namespace ImmigrationSim.Main.Views
{
    public class GlobalInputView : MonoBehaviour
    {
        [SerializeField] private SimConfig simConfig;

        [SerializeField] private TMP_Text travellersPerSecondText;
        [SerializeField] private TMP_Text simSpeedText;
        [SerializeField] private TMP_Text simDurationText;
        [SerializeField] private TMP_Text citizenRatioText;
        [SerializeField] private TMP_Text waitThresholdText;

        private void Start()
        {
            travellersPerSecondText.text = simConfig.TravellersPerSecond.ToString();
            simSpeedText.text = simConfig.SimSpeed.ToString();
            simDurationText.text = simConfig.SimDuration.ToString();
            citizenRatioText.text = simConfig.CitizenRatio.ToString();
            waitThresholdText.text = simConfig.WaitThreshold.ToString();
        }
    }
}
