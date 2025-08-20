using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgePlayer : MonoBehaviour
{
    private PlHealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        //healthController = FindFirstObjectByType<PlHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Damage Player Triggered" + gameObject);
        if (other.CompareTag("Player"))
        {
            // other.gameObject.SetActive(false); //deactivates player in inspector
            PlHealthController.instance.DamagePlayer();
        }
    }
}
