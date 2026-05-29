using UnityEngine;
using UnityEngine.SceneManagement;

namespace ImmigrationSim.Main
{
    public class SceneChanger : MonoBehaviour
    {
        public void BackToSetup()
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
