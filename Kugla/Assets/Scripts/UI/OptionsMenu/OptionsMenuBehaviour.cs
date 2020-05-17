using UnityEngine;

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

            SideSpeedSliderUI.value = sideSpeed;
            ForwardSpeedSliderUI.value = PlayerPrefs.GetFloat("ForwardSpeed");
        }
    }
}