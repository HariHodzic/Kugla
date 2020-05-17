using UnityEngine;

namespace Assets.Scripts.UI.OptionsMenu
{
    public class Slider : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.Slider SliderUI;

        void Start()
        {
            SliderUI.value = PlayerPrefs.GetFloat("SideSpeed");
        }
    }
}
