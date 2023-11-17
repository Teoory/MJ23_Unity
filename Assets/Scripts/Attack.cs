using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int AttackDamage = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Attacks();
        }
    }

    public void Attacks()
    {
        KarakterKontrol playerHealth = FindObjectOfType<KarakterKontrol>();
        if (playerHealth != null)
            {
                playerHealth.TakeDamage(AttackDamage); //Player a hasar verme
                // Debug.Log("Mevcut Can :" + playerHealth.health);
                Debug.Log("Hasar verdi :" + AttackDamage);
            }
    }
}
