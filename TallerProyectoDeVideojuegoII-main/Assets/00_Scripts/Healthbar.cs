using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Health playerHealth;
    public Slider slider;
    
    void Start()
    {
        slider = GetComponent<Slider>();

        if (playerHealth != null)
        {
            slider.minValue = 0;
            slider.maxValue = playerHealth.maxHealth;
            slider.value = playerHealth.currentHealth;
        }
        

    }

    void Update()
    {
        if (playerHealth != null)
        {
            slider.value = playerHealth.currentHealth;
        }
        
    }
}
