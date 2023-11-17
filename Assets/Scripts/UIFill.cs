using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFill : MonoBehaviour
{
    public float maxValue;
    public GameObject PickUpItem;
    public bool PickUpReady;
    [SerializeField] private Slider staminaSlider;
    private float maxStamina = 100;
    public float stamina;

    public float currentValue;

    private void Awake() {
        // maxStamina = GetComponentInParent<KarakterKontrol>().maxStamina;
        maxStamina = FindObjectOfType<KarakterKontrol>().maxStamina;
    }

    private void Start() 
    {
        currentValue = maxValue;
        staminaSlider.maxValue = maxStamina;
    }

    private void Update() 
    {
        PickUpReady = FindObjectOfType<PickupController>().holding;
        stamina = FindObjectOfType<KarakterKontrol>().stamina;
        

        if(PickUpReady)
        {
            PickUpItem.SetActive(true);
        }else
        {
            PickUpItem.SetActive(false);
        }

        staminaSlider.value = stamina;

        if (stamina == maxStamina)
            {staminaSlider.gameObject.SetActive(false);}
        if (stamina >= 0 && stamina <= 99)
            {staminaSlider.gameObject.SetActive(true);}
    }

    
    private void OnGUI() {
        float t = Time.deltaTime / 1f;
        staminaSlider.value = Mathf.Lerp(staminaSlider.value, stamina, t);
    }
}
