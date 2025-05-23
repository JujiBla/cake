using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Animator bossAnim;
    private bool isWeak;

    public Transform[] bossMovePoints;
    private int currentMovePoint;
    public float bossMoveSpeed;

    private int currentPhase;

    public GameObject deathEffect;


    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindFirstObjectByType<CamController>();

        shootStartCounter = waitToStartShooting;

        blockers.transform.SetParent(null);

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

            if(isWeak == false)
            {
                
                    theBoss.transform.position = Vector3.MoveTowards(
                    theBoss.transform.position,
                    bossMovePoints[currentMovePoint].position,
                    bossMoveSpeed * Time.deltaTime);

                if(theBoss.transform.position == bossMovePoints[currentMovePoint].position)
                {
                    currentMovePoint++;

                    if(currentMovePoint >= bossMovePoints.Length)
                    {
                        currentMovePoint = 0;
                    }
                }
            }
                      

        }

    }

    public void ActivateBattle()
    {
        bossActive = true;

        blockers.SetActive(true);

        cameraController.enabled = false;

        AudioManager.instance.PlayBossMusic();
    }

    void FireShot()
    {
        Instantiate(projectileToFire, 
            projectilePoints[currentShot].position, 
            projectilePoints[currentShot].rotation);

        projectilePoints[currentShot].gameObject.SetActive(false);

        currentShot++;
        if(currentShot >= projectilePoints.Length)
        {
            shotCounter = 0f;

            MakeWeak();
        }
        AudioManager.instance.PlaySFX(2);
    }

    void MakeWeak()
    {
        bossAnim.SetTrigger("isWeak");
        isWeak = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Hit");

            if(isWeak == false)
            {
                PlHealthController.instance.DamagePlayer();
            } else
            {
                if(other.transform.position.y > theBoss.position.y) //remove if boss is supposed to be damaged from all sides
                {
                    bossAnim.SetTrigger("hit");

                    FindFirstObjectByType<PlayerController>().Jump();
                    //thePlayer.Jump();

                    MoveToNextPhase();
                }
                                
            }
        }
    }

    void MoveToNextPhase()
    {
        currentPhase++;

        if(currentPhase < 3)
        {
            isWeak = false;

            waitToStartShooting *= .5f;
            timeBetweenShots *= .75f;
            bossMoveSpeed *= 1.5f;

            shootStartCounter = waitToStartShooting;

            projectileLauncher.localScale = Vector3.zero;

            foreach(Transform point in projectilePoints)
            {
                point.gameObject.SetActive(true);
            }

            currentShot = 0;

            AudioManager.instance.PlaySFX(1);


        } else
        {
            //end the battle

            gameObject.SetActive(false);
            blockers.SetActive(false);

            cameraController.enabled = true;

            Instantiate(deathEffect, theBoss.position, Quaternion.identity);

            AudioManager.instance.PlaySFX(0);

            AudioManager.instance.PlayLevelMusic(FindFirstObjectByType<LevelMusicPlayer>().trackToPlay);
        }
    }
}
