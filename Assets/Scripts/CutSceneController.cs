using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    private CamController cameraController;
    private Animator playerAnim;
    private PlayerController thePlayer;

    private bool isSleeping = true;
    public float sleepingTime = 3f;
    private bool isWakingUp = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindFirstObjectByType<CamController>();
    
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<Animator>();
        thePlayer = player.GetComponent<PlayerController>();

    }

    void Update()
    {
        if(isSleeping == true)
        {            
            sleepingTime -= Time.deltaTime;

            if (sleepingTime <= 0f)
            {
                isSleeping = false;
                isWakingUp = true;
                playerAnim.SetTrigger("isWakingUp");
            }
        }

        if(isWakingUp == true)
        {

        }

    }

    public void ActivateIntro()
    {
        isSleeping = true;
        //thePlayer.canMove = false;
        cameraController.enabled = false;
        playerAnim.SetTrigger("isSleeping");
    }
}
