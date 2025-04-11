using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) //collsion2d is an event thats created when 2 collider hit each other
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlHealthController.instance.DamagePlayer();
        }
    }
}
