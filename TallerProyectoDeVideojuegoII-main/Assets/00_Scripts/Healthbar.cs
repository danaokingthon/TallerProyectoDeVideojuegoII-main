using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Health playerHealth;
    public Slider slider;
    
    void Start()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = playerHealth.maxHealth;
        slider.value = playerHealth.currentHealth;

    }

    void Update()
    {
        slider.value = playerHealth.currentHealth;
    }
}
