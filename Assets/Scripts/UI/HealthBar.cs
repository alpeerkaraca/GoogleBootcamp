using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image healthBarCheck;

    public void SetHealth(float HP)
    {
        slider.value = HP;
        healthBarCheck.color = gradient.Evaluate(slider.normalizedValue);
    }
    

    public void SetMaxHP(float maxHP)
    { 
        slider.maxValue = maxHP;
        slider.value = maxHP;
        healthBarCheck.color = gradient.Evaluate(1f);
    }
}
