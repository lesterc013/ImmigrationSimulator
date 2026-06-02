using ImmigrationSim.Main.Analytics;
using UnityEngine;
using TMPro;

namespace ImmigrationSim.Main.Views
{
    public class OutputView : MonoBehaviour
    {
        [SerializeField] private AnalyticsEngine analyticsEngine;

        [Header("Global")]
        [SerializeField] private TMP_Text totalCompletedText;
        [SerializeField] private TMP_Text avgTimeInSystemText;
        [SerializeField] private TMP_Text throughputText;
        [SerializeField] private TMP_Text percentAboveWaitThresholdText;

        [Header("Security")]
        [SerializeField] private TMP_Text securityAvgWaitTimeText;
        [SerializeField] private TMP_Text securityServerUtilRate;

        [Header("Immigration")]
        [SerializeField] private TMP_Text immigrationAvgWaitTimeText;
        [SerializeField] private TMP_Text immigrationServerUtilRate;

        private void Update()
        {
            totalCompletedText.text = analyticsEngine.TotalTravellersCompleted.ToString();
            avgTimeInSystemText.text = analyticsEngine.AvgTotalTimeInSystem.ToString();
            throughputText.text = analyticsEngine.Throughput.ToString();
            percentAboveWaitThresholdText.text = analyticsEngine.PercentageAboveWaitThreshold.ToString();

            securityAvgWaitTimeText.text = analyticsEngine.SecurityAvgWaitTime.ToString();
            securityServerUtilRate.text = analyticsEngine.SecurityServerUtilisationRate.ToString();

            immigrationAvgWaitTimeText.text = analyticsEngine.ImmigrationAvgWaitTime.ToString();
            immigrationServerUtilRate.text = analyticsEngine.ImmigrationServerUtilisationRate.ToString();
        }
    }
}
