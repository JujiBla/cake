using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //remove start and update when not used to save processing time
    public int healthToAdd;
    public bool healFull;

    public GameObject pickupEffect;

    public int amountHP = 1;
    public int indexHP = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlHealthController.instance.currentHealth != PlHealthController.instance.maxHealth)
            {
                if (healFull == true)
                {
                    PlHealthController.instance.AddHealth(PlHealthController.instance.maxHealth);
                }
                else
                {
                    
                    PlHealthController.instance.AddHealth(healthToAdd);
                }

                gameObject.SetActive(false);

                CollectiblesManager.instance.collectedHPSinceLastCheckpoint.Add(indexHP);

                Instantiate(pickupEffect, transform.position, transform.rotation);
                //brings a copy of an object into the world

                AudioManager.instance.PlaySFXPitched(10);
            }
        }
    }
}
