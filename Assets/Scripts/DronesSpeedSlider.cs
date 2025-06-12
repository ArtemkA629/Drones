using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DronesSpeedSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [Inject]
    public void Init(DronesSpeedController dronesSpeedController)
    {
        _slider.onValueChanged.AddListener(dronesSpeedController.OnSliderChanged);
        dronesSpeedController.OnSliderChanged(_slider.value);
    }
}