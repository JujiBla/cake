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

    public int currentLives = 3;

    public GameObject deathEffect, respawnEffect;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindFirstObjectByType<PlayerController>();

        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        thePlayer.gameObject.SetActive(false);
        thePlayer.theRB.velocity = Vector2.zero; //if player falls from cliff, it can gain a lot of momentum, if they spawn with that momentum the can potentialy go through the ground

        currentLives--;

        if(currentLives > 0)
        {
            StartCoroutine(RespawnCo()); //Coroutines work parallel to everything else in the function, if there was more in this function it would go through while doing the coroutine at the same time
        } else
        {
            currentLives = 0;

            StartCoroutine(GameOverCo());
        }

        UpdateDisplay();

        Instantiate(deathEffect, thePlayer.transform.position, deathEffect.transform.rotation);

    }

    public IEnumerator RespawnCo()  //coroutine, needs to have a delay, makes player spawn after a certain amount of time
    {
        yield return new WaitForSeconds(respawnDelay);      //yield = wait until return information
        
        thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;

        PlHealthController.instance.AddHealth(PlHealthController.instance.maxHealth);

        thePlayer.gameObject.SetActive(true);

        Instantiate(respawnEffect, thePlayer.transform.position, Quaternion.identity); //rotations are handled as quaternions, they have 4 axies, quaternion.identity gives it 0 rotation
    }

    public IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(respawnDelay);

        if(UIController.instance != null)  //check if theres a UIController in the scene
        {
            UIController.instance.ShowGameOver();
        }
    }

    public void AddLife()
    {
        currentLives++;

        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (UIController.instance != null)
        {
            UIController.instance.UpdateLivesDisplay(currentLives);
        }
    }
}
