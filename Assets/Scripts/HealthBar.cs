using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText;
    private KarakterKontrol karakterKontrol;
    private int maxHealthSlider;
    private int currentHealthSlider;


    private void Start()
    {
        karakterKontrol = KarakterKontrol.Instance;
        SetMaxHealth(karakterKontrol.maxHealth);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        UpdateHealthText(health);
    }

    private void UpdateHealthText(int health)
    {
        healthText.text = "" + health.ToString();

        if(health <= 10)
        {
            healthText.color = new Color(360, 89, 65);
        }else healthText.color = Color.white;
    }
}