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

            SideSpeedSliderUI.value = sideSpeed<3?3:sideSpeed;
            ForwardSpeedSliderUI.value = forwardSpeed<3?3:forwardSpeed;

            if(sideSpeed<3)
                PlayerPrefs.SetFloat("SideSpeed",3);
            if(forwardSpeed<3)
                PlayerPrefs.SetFloat("ForwardSpeed",3);

            PlayerPrefs.Save();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(Constants.MainMenuScene);
        }
    }
}