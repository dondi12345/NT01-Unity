using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessBar : LoadBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public bool isGradient = false;

    public void SetValue(float value){
        this.slider.value = value;
        if(isGradient){
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

    public void SetColor(Color color){
        this.fill.color = color;
    }
}
