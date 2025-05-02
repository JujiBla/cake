using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreelineMover : MonoBehaviour
{
    public float maxDistanceX = 22f;
    public float maxDistanceY = 22f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distancex = transform.position.x - Camera.main.transform.position.x;

        if (distancex > maxDistanceX)
        {
            transform.position -= new Vector3(maxDistanceX * 2f, 0f, 0f);
        }

        if (distancex < -maxDistanceX)
        {
            transform.position += new Vector3(maxDistanceX * 2f, 0f, 0f);
        }

        float distancey = transform.position.y - Camera.main.transform.position.y;

        if (distancey > maxDistanceY)
        {
            transform.position -= new Vector3(0f, maxDistanceY * 2f, 0f);
        }

        if (distancey < -maxDistanceY)
        {
            transform.position += new Vector3(0f, maxDistanceY * 2f, 0f);
        }
    }
}
