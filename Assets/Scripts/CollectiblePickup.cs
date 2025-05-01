using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    public int amount = 1;
    public int index = -1;

    public GameObject pickupEffect;
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(CollectiblesManager.instance != null)    //checks if collMan is there, can cause problems if not
            {
                CollectiblesManager.instance.GetCollectible(amount);

                gameObject.SetActive(false);

                CollectiblesManager.instance.collectedSinceLastCheckpoint.Add(index);

                Instantiate(pickupEffect, transform.position, Quaternion.identity);

                AudioManager.instance.PlaySFXPitched(9);
            }
        }
    }



}
