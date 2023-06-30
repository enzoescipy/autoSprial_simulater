using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doubleSlider : MonoBehaviour
{
    public GameObject lowerSlider;
    public GameObject upperSlider;

    private Slider lowerSlider_slider;
    private Slider upperSlider_slider;

    // Start is called before the first frame update
    void Start()
    {
        lowerSlider_slider = lowerSlider.GetComponent<Slider>();
        upperSlider_slider = upperSlider.GetComponent<Slider>();

        lowerSlider_slider.onValueChanged.AddListener(lowerSlider_onValueChanged);
        upperSlider_slider.onValueChanged.AddListener(upperSlider_onValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void lowerSlider_onValueChanged(float lowerValue)
    {
        if (upperSlider_slider.value < lowerValue )
        {
            upperSlider_slider.value = lowerValue;
        }
    }

    void upperSlider_onValueChanged(float upperValue)
    {
        if (lowerSlider_slider.value > upperValue)
        {
            lowerSlider_slider.value = upperValue;
        }
    }
}
