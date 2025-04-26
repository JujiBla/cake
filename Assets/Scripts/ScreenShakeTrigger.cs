using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeTrigger : MonoBehaviour
{

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.3f;

    private bool hasShaken = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasShaken && other.CompareTag("Player")) 
        {
            hasShaken = true;

            if (ScreenShake.Instance != null)
            {
                StartCoroutine(ScreenShake.Instance.Shake(shakeDuration, shakeMagnitude));
                
            }
        }
    }
}
    

