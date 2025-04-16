using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTracker : MonoBehaviour
{

    public static InfoTracker instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            transform.SetParent(null); //"dont destroy on load" doesnt work on childs
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public int currentLives, currentFruit;

    public void GetInfo()
    {
        if(LifeController.instance != null)
        {
            currentLives = LifeController.instance.currentLives;
        }

        if(CollectiblesManager.instance != null)
        {
            currentFruit = CollectiblesManager.instance.collectibleCount;
        }
    }
}
