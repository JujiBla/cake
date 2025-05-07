using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator anim;
       
    public bool isDefeated;

    public float waitToDestroy;
    private float currentWaitToDestroy;

    public int amount = 1;
    public int index = -1;

    [HideInInspector]
    public bool hasBouncedPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeated == true)
        {
            currentWaitToDestroy -= Time.deltaTime;

            if (currentWaitToDestroy <= 0)
            {
                gameObject.SetActive(false);

                SaveStateManager.instance.killedSinceLastCheckpoint.Add(index);

                AudioManager.instance.PlaySFX(5);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(isDefeated == false)
            { 
                PlHealthController.instance.DamagePlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !hasBouncedPlayer)
        {
            FindFirstObjectByType<PlayerController>().Jump();
            hasBouncedPlayer = true;

            anim.SetTrigger("defeated");

            isDefeated = true;
            GetComponent<Collider2D>().enabled = false;
            currentWaitToDestroy = waitToDestroy;

            AudioManager.instance.PlaySFX(6);

        }
    }
}
