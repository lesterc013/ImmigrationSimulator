using ImmigrationSim.Core;
using TMPro;
using UnityEngine;

namespace ImmigrationSim.Main.Views
{
    public class ImmigrationInputView : MonoBehaviour
    {
        [SerializeField] private SimConfig simConfig;

        [SerializeField] private TMP_Text serverCountText;
        [SerializeField] private TMP_Text citizenMinProcessTime;
        [SerializeField] private TMP_Text citizenMaxProcessTime;
        [SerializeField] private TMP_Text foreignMinProcessTime;
        [SerializeField] private TMP_Text foreignMaxProcessTime;
        [SerializeField] private TMP_Text secondaryScreeningProb;
        [SerializeField] private TMP_Text secondaryScreeningMinTime;
        [SerializeField] private TMP_Text secondaryScreeningMaxTime;

        private void Start()
        {
            serverCountText.text = simConfig.ImmigrationServerCount.ToString();
            citizenMinProcessTime.text = simConfig.CitizenMinImmigrationProcessingTime.ToString();
            citizenMaxProcessTime.text = simConfig.CitizenMaxImmigrationProcessingTime.ToString();
            foreignMinProcessTime.text = simConfig.ForeignerMinImmigrationProcessingTime.ToString();
            foreignMaxProcessTime.text = simConfig.ForeignerMaxImmigrationProcessingTime.ToString();
            secondaryScreeningProb.text = simConfig.SecondaryScreeningProbability.ToString();
            secondaryScreeningMinTime.text = simConfig.MinSecondaryScreeningTime.ToString();
            secondaryScreeningMaxTime.text = simConfig.MaxSecondaryScreeningTime.ToString();
        }
    }
}
