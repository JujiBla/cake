using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneActivator : MonoBehaviour
{
    public CutSceneController cutSceneCont;

    private CamController cameraController;
    private PlayerController thePlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player")) //&& ist Level 1
        {
            cutSceneCont.ActivateIntro();

        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player")) //&& ist Level 1
    //    {
    //        cutSceneCont.DeactivateIntro();
    //        gameObject.SetActive(false); 
    //    }
    //}
}
