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
        [SerializeField] private TMP_Text securityServerAvgUtilRate;
        [SerializeField] private TMP_Text securityMaxQueueCount;

        [Header("Immigration")]
        [SerializeField] private TMP_Text immigrationAvgWaitTimeText;
        [SerializeField] private TMP_Text immigrationServerAvgUtilRate;
        [SerializeField] private TMP_Text immigrationMaxQueueCount;

        private void Update()
        {
            totalCompletedText.text = analyticsEngine.TotalTravellersCompleted.ToString();
            avgTimeInSystemText.text = analyticsEngine.AvgTotalTimeInSystem.ToString("F2");
            throughputText.text = analyticsEngine.Throughput.ToString("F2");
            percentAboveWaitThresholdText.text = analyticsEngine.PercentageAboveWaitThreshold.ToString("F2");

            securityAvgWaitTimeText.text = analyticsEngine.SecurityAvgWaitTime.ToString("F2");
            securityServerAvgUtilRate.text = analyticsEngine.SecurityServerAvgUtilisationRate.ToString("F2");
            securityMaxQueueCount.text = analyticsEngine.SecurityMaxQueueCount.ToString();

            immigrationAvgWaitTimeText.text = analyticsEngine.ImmigrationAvgWaitTime.ToString("F2");
            immigrationServerAvgUtilRate.text = analyticsEngine.ImmigrationServerAvgUtilisationRate.ToString("F2");
            immigrationMaxQueueCount.text = analyticsEngine.ImmigrationMaxQueueCount.ToString();
        }
    }
}
