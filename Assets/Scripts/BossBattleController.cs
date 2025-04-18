using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    private bool bossActive;

    public GameObject blockers;

    public Transform camPoint;
    private CamController cameraController;
    public float cameraMoveSpeed;

    public Transform theBoss;
    public float bossGrowSpeed = 2f;

    public Transform projectileLauncher;
    public float launcherGrowSpeed = 2f;

    public float launcherRotateSpeed = 90f;
    private float launcherRotation;

    public GameObject projectileToFire;
    public Transform[] projectilePoints;

    public float waitToStartShooting, timeBetweenShots;
    private float shootStartCounter, shotCounter;
    private int currentShot;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindFirstObjectByType<CamController>();

        shootStartCounter = waitToStartShooting;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossActive == true)
        {
            cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position,
                camPoint.position,
                cameraMoveSpeed * Time.deltaTime);

            if(theBoss.localScale != Vector3.one)
            {
                theBoss.localScale = Vector3.MoveTowards(
                    theBoss.localScale,
                    Vector3.one,
                    bossGrowSpeed * Time.deltaTime);
            }

            if(projectileLauncher.transform.localScale != Vector3.one)
            {
                projectileLauncher.localScale = Vector3.MoveTowards(
                    projectileLauncher.localScale,
                    Vector3.one,
                    bossGrowSpeed * Time.deltaTime);
            }

            launcherRotation += launcherRotateSpeed * Time.deltaTime;
            if(launcherRotation > 360f)
            {
                launcherRotation -= 360f; 
                //otherwise it will add on the rotation until the game crashes bc nubers too big
            }
            projectileLauncher.transform.localRotation = Quaternion.Euler(0f, 0f, launcherRotation);
            //.euler converts quaternion into 3 values?
            //"Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis,
            //and y degrees around the y axis; applied in that order."
        

            //start shooting
            if(shootStartCounter > 0f)
            {
                shootStartCounter -= Time.deltaTime;
                if(shootStartCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShot();                
                }
            }

            if(shotCounter > 0f) 
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShot();
                }
            }

        }

    }

    public void ActivateBattle()
    {
        bossActive = true;

        blockers.SetActive(true);

        cameraController.enabled = false;
    }

    void FireShot()
    {
        //Debug.Log("Fires shot at " + Time.time);

        Instantiate(projectileToFire, projectilePoints[currentShot].position, 
            projectilePoints[currentShot].rotation);

        projectilePoints[currentShot].gameObject.SetActive(false);

        currentShot++;
        if(currentShot >= projectilePoints.Length)
        {
            shotCounter = 0f;
        }

    }
}
