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
            var sideSpeed = PlayerPrefs.GetFloat("SideSpeed");
            var forwardSpeed = PlayerPrefs.GetFloat("ForwardSpeed");

            var hasSideSpeed = PlayerPrefs.HasKey("SideSpeed");
            var hasForwardSpeed = PlayerPrefs.HasKey("ForwardSpeed");
            SideSpeedSliderUI.value = hasSideSpeed?sideSpeed:3;
            ForwardSpeedSliderUI.value = hasForwardSpeed?forwardSpeed:3;

            if(!hasSideSpeed)
                PlayerPrefs.SetFloat("SideSpeed",3);
            if(!hasForwardSpeed)
                PlayerPrefs.SetFloat("ForwardSpeed",3);

            PlayerPrefs.Save();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(Constants.MainMenuScene);
        }
    }
}