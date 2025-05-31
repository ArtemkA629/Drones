using UnityEngine;
using UnityEngine.UI;

public class DronesSpeedSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private int _defaultValue;

    public void Init(DronesSpeedController dronesSpeedController)
    {
        _slider.wholeNumbers = true;
        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;
        _slider.value = _defaultValue;

        _slider.onValueChanged.AddListener(dronesSpeedController.OnSliderChanged);
        dronesSpeedController.OnSliderChanged(_slider.value);
    }
}