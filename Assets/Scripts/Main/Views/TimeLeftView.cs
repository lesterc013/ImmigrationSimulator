using ImmigrationSim.Core;
using TMPro;
using UnityEngine;

namespace ImmigrationSim.Main.Views
{
    public class TimeLeftView : MonoBehaviour
    {
        [SerializeField] private SimConfig simConfig;

        [SerializeField] private TMP_Text simTimeLeftText;
        [SerializeField] private TMP_Text realTimeLeftText;
        [SerializeField] private TMP_Text simCompletedText;

        private void Start()
        {
            simCompletedText.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (SimClock.Instance.IsFinished)
            {
                simCompletedText.gameObject.SetActive(true);
                simTimeLeftText.text = "0";
                realTimeLeftText.text = "0";
                return;
            }

            float simTimeLeft = simConfig.SimDuration - SimClock.Instance.TotalSimTimeElapsed < 0 ? 0 : simConfig.SimDuration - SimClock.Instance.TotalSimTimeElapsed;
            float realTimeLeft = simTimeLeft / simConfig.SimSpeed;

            simTimeLeftText.text = simTimeLeft.ToString("F2");
            realTimeLeftText.text = realTimeLeft.ToString("F2");
        }
    }
}
