using UnityEngine;
using UnityEngine.UI;

public class DronesCountSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private int _defaultValue;

    public void Init(DronesCountController[] dronesCountControllers)
    {
        _slider.wholeNumbers = true;
        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;
        _slider.value = _defaultValue;

        foreach (var dronesCountController in dronesCountControllers)
        {
            _slider.onValueChanged.AddListener(dronesCountController.OnSliderChanged);
            dronesCountController.OnSliderChanged(_slider.value);
        }
    }
}
