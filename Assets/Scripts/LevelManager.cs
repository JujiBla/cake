using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (InfoTracker.instance != null)
        {
            InfoTracker.instance.OnLevelStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
