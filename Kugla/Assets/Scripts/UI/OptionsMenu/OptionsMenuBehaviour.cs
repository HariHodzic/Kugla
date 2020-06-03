using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.OptionsMenu
{
    public class OptionsMenuBehaviour : MonoBehaviour
    {
#pragma warning disable 0649

        [SerializeField]
        private UnityEngine.UI.Slider SideSpeedSliderUI;

        [SerializeField]
        private UnityEngine.UI.Slider ForwardSpeedSliderUI;

#pragma warning restore 0649

        private void Start()
        {
            SideSpeedSliderUI.value = PlayerPrefs.HasKey("SideSpeed")?PlayerPrefs.GetFloat("SideSpeed"):4;
            ForwardSpeedSliderUI.value = PlayerPrefs.HasKey("ForwardSpeed")?PlayerPrefs.GetFloat("ForwardSpeed"):4;
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(Constants.MainMenuScene);
        }
    }
}