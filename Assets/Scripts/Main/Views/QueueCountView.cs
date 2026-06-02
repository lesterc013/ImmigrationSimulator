using ImmigrationSim.Main.QueueController;
using TMPro;
using UnityEngine;

namespace ImmigrationSim.Main.Views
{
    public class QueueCountView : MonoBehaviour
    {
        [SerializeField] private QueueControllerEntity queueController;
        [SerializeField] private TMP_Text queueCountText;

        private void Update()
        {
            queueCountText.text = queueController.QueueCount.ToString();
        }
    }
}
