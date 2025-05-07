using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Checkpoint : MonoBehaviour
{
    private bool isActive;
    public Animator anim;

    [HideInInspector]
    public CheckpointManager cpMan;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive == false)  //&& = both need to be true
        {
            cpMan.SetActiveCheckpoint(this);

            anim.SetBool("flagActive", true);

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
    }
}
