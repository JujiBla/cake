using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager instance;
        private void Awake()
    {
        instance = this;
    }

    public int collectibleCount;
    public List<int> collectedSinceLastCheckpoint;
    public List<int> collectedHPSinceLastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        collectibleCount = InfoTracker.instance.currentFruit;

        if (UIController.instance != null)
        {
            UIController.instance.UpdateCollectibles(collectibleCount);
        }

        CollectiblePickup[] collectedPickups = Object.FindObjectsOfType<CollectiblePickup>(true);
        for (int i = 0; i < collectedPickups.Count(); i++)
        {
            CollectiblePickup pickup = collectedPickups[i];

            pickup.index = i;
        }
        collectedSinceLastCheckpoint = new List<int>();

        HealthPickup[] collectedHPPickups = Object.FindObjectsOfType<HealthPickup>(true);
        for (int i = 0; i < collectedHPPickups.Count(); i++)
        {
            
            HealthPickup pickupHP = collectedHPPickups[i];
            Debug.Log("mistigermist2");
            pickupHP.indexHP = i;
            Debug.Log("mistigermist");
        }
        collectedHPSinceLastCheckpoint = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetCollectible(int amount)
    {
        collectibleCount += amount;
        
        if(UIController.instance != null)
        {
            UIController.instance.UpdateCollectibles(collectibleCount);
        }
    }

}
