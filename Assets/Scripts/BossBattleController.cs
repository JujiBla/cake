using System;
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
    private bool nice;

    public Transform[] bossMovePoints;
    private int currentMovePoint;
    public float bossMoveSpeed;

    private int currentPhase;

    public GameObject deathEffect;

    public GameObject[] memories;

    private bool waitingForSkip = false;

    public float waitToEndLevel = 4f;

    public float fadeTime = 1f;

    public string nextLevel;

    public Collider2D bossCollider;

    public GameObject ashTrail;


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
        if (waitingForSkip && Input.GetMouseButtonDown(1))
        {
            Time.timeScale = 1f;
            waitingForSkip = false;

            for (int i = 0; i <= currentPhase; i++)
            {
                memories[i].SetActive(false);
            }

            MoveToNextPhase();
        }
        
        MoveBoss();
    }

    private void MoveBoss()
    {
        if (bossActive == false)
        { return; } 
        
        cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position,
            camPoint.position,
            cameraMoveSpeed * Time.deltaTime
        );

        if (projectileLauncher.transform.localScale != Vector3.one)
        {
            projectileLauncher.localScale = Vector3.MoveTowards(
                projectileLauncher.localScale,
                Vector3.one,
                bossGrowSpeed * Time.deltaTime);
        }

        launcherRotation += launcherRotateSpeed * Time.deltaTime;
        if (launcherRotation > 360f)
        {
            launcherRotation -= 360f;
            //otherwise it will add on the rotation until the game crashes bc nubers too big
        }
        projectileLauncher.transform.localRotation = Quaternion.Euler(0f, 0f, launcherRotation);
        //.euler converts quaternion into 3 values?
        //"Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis,
        //and y degrees around the y axis; applied in that order."


        //start shooting
        if (shootStartCounter > 0f)
        {
            shootStartCounter -= Time.deltaTime;
            if (shootStartCounter <= 0f)
            {
                shotCounter = timeBetweenShots;

                FireShot();
            }
        }

        if (shotCounter > 0f)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                shotCounter = timeBetweenShots;

                FireShot();
            }
        }

        if (isWeak == false)
        {
            theBoss.transform.position = Vector3.MoveTowards(
                theBoss.transform.position,
                bossMovePoints[currentMovePoint].position,
                bossMoveSpeed * Time.deltaTime
            );

            if (theBoss.transform.position == bossMovePoints[currentMovePoint].position)
            {
                currentMovePoint++;

                if (currentMovePoint >= bossMovePoints.Length)
                {
                    currentMovePoint = 0;
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
        Instantiate(
            projectileToFire,
            projectilePoints[currentShot].position,
            projectilePoints[currentShot].rotation
        );

        projectilePoints[currentShot].gameObject.SetActive(false);

        currentShot++;
        if (currentShot >= projectilePoints.Length)
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
        if (other.gameObject.CompareTag("Player"))
        {
            if (isWeak == false)
            {
                PlHealthController.instance.DamagePlayer();
            }
            else
            {
                if (other.transform.position.y > theBoss.position.y)
                {
                    bossAnim.SetTrigger("hit");

                    StartCoroutine(MoveCameraToPlayer(new Vector3(0f, 1f, -10f), 0.3f));

                    FindFirstObjectByType<PlayerController>().Jump();

                    for (int i = 0; i <= currentPhase; i++)
                    {
                        memories[i].SetActive(true);
                    }

                    StartCoroutine(GradualPause(0.5f));
                }

            }
        }
    }

    void MoveToNextPhase()
    {
        currentPhase++;

        if (currentPhase < 3)
        {
            isWeak = false;

            waitToStartShooting *= .5f;
            timeBetweenShots *= .75f;
            bossMoveSpeed *= 1.5f;

            shootStartCounter = waitToStartShooting;

            projectileLauncher.localScale = Vector3.zero;

            foreach (Transform point in projectilePoints)
            {
                point.gameObject.SetActive(true);
            }

            currentShot = 0;

            AudioManager.instance.PlaySFX(1);

            return;
        }

        
        // end the battle

        bossAnim.SetTrigger("nice");
        ashTrail.SetActive(false);

        StartCoroutine(EndLevelBoss());
        
        //gameObject.SetActive(false);
        blockers.SetActive(false);

        cameraController.enabled = true;

        //Instantiate(deathEffect, theBoss.position, Quaternion.identity);
        //change animation to healthy lumo
   
        bossCollider.enabled = false;

        AudioManager.instance.PlaySFX(0);

        AudioManager.instance.PlayLevelMusic(FindFirstObjectByType<LevelMusicPlayer>().trackToPlay);



    }

    IEnumerator GradualPause(float duration)
    {
        float startTime = Time.unscaledTime;
        float initialTimeScale = Time.timeScale;

        while (Time.unscaledTime < startTime + duration)
        {
            float time = (Time.unscaledTime - startTime) / duration;
            Time.timeScale = Mathf.Lerp(initialTimeScale, 0f, time);
            yield return null;
        }

        Time.timeScale = 0f;
        waitingForSkip = true;
        yield return null;
    }

    IEnumerator MoveCameraToPlayer(Vector3 offset, float duration)
    {
        Transform cam = Camera.main.transform;
        Vector3 startPos = cam.position;
        Vector3 targetPos = FindFirstObjectByType<PlayerController>().transform.position + offset;

        float time = 0f;
        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            cam.position = Vector3.Lerp(startPos, targetPos, time / duration);
            yield return null;
        }

        cam.position = targetPos;
    }

    IEnumerator EndLevelBoss()
    {
        yield return new WaitForSeconds(waitToEndLevel - fadeTime);

        UIController.instance.FadeToBlack();
      
        yield return new WaitForSeconds(fadeTime);
        
        SceneManager.LoadScene(nextLevel);
    }

}
