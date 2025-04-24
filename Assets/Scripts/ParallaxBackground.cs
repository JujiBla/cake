using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public static ParallaxBackground instance;

    private void Awake()
    {
        instance = this;
    }

    private Transform theCam;

    public Transform sky, treeline;
    [Range(0f, 1f)]
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main.transform; //back in the day camera.main was bad to use in update, bc it would search everything every frame - uses lot of processing power, today stored in memory after been done once, no prob!
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    public void MoveBackground()
    {
        sky.position = new Vector3(
            theCam.position.x * parallaxSpeed * .8f, 
            sky.position.y, 
            sky.position.z);

        treeline.position = new Vector3(
            theCam.position.x * parallaxSpeed,
            treeline.position.y,
            treeline.position.z);
    }
}
