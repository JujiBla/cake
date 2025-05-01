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

            transform.SetParent(null);      //"dont destroy on load" doesnt work on childs
            DontDestroyOnLoad(gameObject);

            if(PlayerPrefs.HasKey("fruit")) //checks if anything is in the player prefs, gives out zero when nothing is there
            { 
                currentFruit = PlayerPrefs.GetInt("fruit");
            }
        } else
        {
            Destroy(gameObject);
        }
    }

    public int currentFruit;

    public void GetInfo()
    {

        if(CollectiblesManager.instance != null)
        {
            currentFruit = CollectiblesManager.instance.collectibleCount;
        }
    }

    public void SaveInfo()
    {
         PlayerPrefs.SetInt("fruit", currentFruit);
    }
}
