using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Checkpoint : MonoBehaviour
{
    private bool isActive;
    public Animator anim;

    private GameObject lightObject;

    [HideInInspector]
    public CheckpointManager cpMan;

    private void Awake()
    {
        lightObject = transform.Find("Light").gameObject;
        lightObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive == false) 
        {
            cpMan.SetActiveCheckpoint(this);

            anim.SetBool("flagActive", true);
            lightObject.SetActive(true);

            isActive = true;

            AudioManager.instance.PlaySFX(3);

            InfoTracker.instance.GetInfo();
            InfoTracker.instance.SaveInfo();

            CollectiblesManager.instance.collectedSinceLastCheckpoint.Clear();
            SaveStateManager.instance.killedSinceLastCheckpoint.Clear();
        }
    }

    public void DeactivateCheckpoint()
    {
        anim.SetBool("flagActive", false);
        isActive = false;
        lightObject.SetActive(false);
    }
}
