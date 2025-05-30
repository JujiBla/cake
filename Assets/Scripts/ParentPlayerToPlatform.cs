using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayerToPlatform : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.SetParent(transform); //parents player to platform

            other.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
            //Interpolation Interpolate makes player move very slow on moving platform
            //none works great but gives jittery camera movement
        }   
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.SetParent(null);

            other.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;

            GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
