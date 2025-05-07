using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveStateManager : MonoBehaviour
{
    public static SaveStateManager instance;
    private void Awake()
    {
        instance = this;
    }

    public List<int> killedSinceLastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        EnemyController[] Enemies = Object.FindObjectsOfType<EnemyController>(true);
        for (int i = 0; i < Enemies.Count(); i++)
        {
            Enemies[i].index = i;
        }
        killedSinceLastCheckpoint = new List<int>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
