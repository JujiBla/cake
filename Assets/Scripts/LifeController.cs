using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public static LifeController instance;
    private void Awake()
    {
        instance = this;
    }

    private PlayerController thePlayer;

    public float respawnDelay = 2f;

    public GameObject deathEffect, respawnEffect;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        thePlayer.gameObject.SetActive(false);
        thePlayer.theRB.velocity = Vector2.zero; //if player falls from cliff, it can gain a lot of momentum, if they spawn with that momentum the can potentialy go through the ground

         StartCoroutine(RespawnCo());

        Instantiate(deathEffect, thePlayer.transform.position, deathEffect.transform.rotation);

        AudioManager.instance.PlaySFXPitched(11);
    }

    public IEnumerator RespawnCo()  //coroutine, needs to have a delay, makes player spawn after a certain amount of time
    {
        yield return new WaitForSeconds(respawnDelay);      //yield = wait until return information
        
        thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;

        PlHealthController.instance.AddHealth(PlHealthController.instance.maxHealth);

        thePlayer.gameObject.SetActive(true);

        Instantiate(respawnEffect, thePlayer.transform.position, Quaternion.identity); 
        //rotations are handled as quaternions, they have 4 axies, quaternion.identity gives it
        //0 rotation
    }
 
}
