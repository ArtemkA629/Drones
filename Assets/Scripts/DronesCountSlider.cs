using UnityEngine;
using UnityEngine.UI;

public class DronesCountSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void Init(DronesCountChanger[] dronesCountChangers)
    {
        foreach (var dronesCountChanger in dronesCountChangers)
        {
            _slider.onValueChanged.AddListener(dronesCountChanger.OnSliderChanged);
            dronesCountChanger.OnSliderChanged(_slider.value);
        }
    }
}
