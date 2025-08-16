using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField]
    private Slider sliderBar;

    private void Awake()
    {
        sliderBar = GetComponent<Slider>();
    }

    public void UpdateSliderBar(float value)
    {
        sliderBar.value = value;
    }
}
