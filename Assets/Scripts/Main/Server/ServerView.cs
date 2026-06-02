using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ImmigrationSim.Main.Server
{
    public class ServerView : MonoBehaviour
    {
        [SerializeField] private ServerEntity serverEntity;
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text idNumberText;

        private void Start()
        {
            idNumberText.text = serverEntity.Id.ToString();
        }

        private void Update()
        {
            if (serverEntity.IsAvailable)
            {
                image.color = Color.green;
            } else
            {
                image.color = Color.red;
            }
        }
    }
}
