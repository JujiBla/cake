using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;
    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float timePassed = 0.0f;

        while (timePassed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            timePassed += Time.deltaTime;

            yield return null;
            //Pause this Coroutine until the next frame, then continue from here
            //That's what makes the shake feel alive and spread across multiple frames.
            //If you didn't use yield return null;, everything in the Coroutine would just happen immediately,
            //in the same frame, making the screen shake so fast the player wouldn't even see it.
        }

        transform.localPosition = originalPos;


    }

}
