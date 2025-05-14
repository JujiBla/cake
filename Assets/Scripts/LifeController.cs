using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        InfoTracker.instance.currentFruit = PlayerPrefs.GetInt("fruit");
        if (CollectiblesManager.instance != null)    //checks if collMan is there, can cause problems if not
        {
            CollectiblesManager.instance.collectibleCount = 0;
            CollectiblesManager.instance.GetCollectible(InfoTracker.instance.currentFruit);
        }

        CollectiblePickup[] collectedPickups = Object.FindObjectsOfType<CollectiblePickup>(true);
        for (int i = 0; i < collectedPickups.Count(); i++)
        {
            if (CollectiblesManager.instance.collectedSinceLastCheckpoint.Contains(i))
            {
                CollectiblePickup pickup = collectedPickups[i];
                pickup.gameObject.SetActive(true);
            }
        }
        CollectiblesManager.instance.collectedSinceLastCheckpoint.Clear();

        EnemyController[] killedEnemies = Object.FindObjectsOfType<EnemyController>(true);
        
        for (int i = 0; i < killedEnemies.Count(); i++)
        {            
            if (SaveStateManager.instance.killedSinceLastCheckpoint.Contains(i))
            {
                EnemyController killedEnemy = killedEnemies[i];

                killedEnemy.isDefeated = false;
                killedEnemy.hasBouncedPlayer = false;
                killedEnemy.GetComponent<Collider2D>().enabled = true;
                killedEnemy.gameObject.SetActive(true);
            }
        }
        SaveStateManager.instance.killedSinceLastCheckpoint.Clear();

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "level_boss")
        {
            SceneManager.LoadScene(sceneName);
        }

    }




}
