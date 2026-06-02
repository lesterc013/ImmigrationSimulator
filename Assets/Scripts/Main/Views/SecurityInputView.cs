using ImmigrationSim.Core;
using TMPro;
using UnityEngine;

namespace ImmigrationSim.Main.Views
{
    public class SecurityInputView : MonoBehaviour
    {
        [SerializeField] private SimConfig simConfig;

        [SerializeField] private TMP_Text serverCountText;
        [SerializeField] private TMP_Text citizenMinProcessTime;
        [SerializeField] private TMP_Text citizenMaxProcessTime;
        [SerializeField] private TMP_Text foreignMinProcessTime;
        [SerializeField] private TMP_Text foreignMaxProcessTime;

        private void Start()
        {
            serverCountText.text = simConfig.SecurityServerCount.ToString();
            citizenMinProcessTime.text = simConfig.CitizenMinSecurityProcessingTime.ToString();
            citizenMaxProcessTime.text = simConfig.CitizenMaxSecurityProcessingTime.ToString();
            foreignMinProcessTime.text = simConfig.ForeignerMinSecurityProcessingTime.ToString();
            foreignMaxProcessTime.text = simConfig.ForeignerMaxSecurityProcessingTime.ToString();
        }
    }
}
