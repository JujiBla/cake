using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    [HideInInspector]
    public bool isDefeated;

    public float waitToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        // anim = GetComponent<Animator>(); my animator is on the sprite, so this doesnt work like the tutorial - but this way you can find something thats on the same obj
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;

            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);

                AudioManager.instance.PlaySFX(5);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other) //collsion2d is an event thats created when 2 collider hit each other
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
        if(other.CompareTag("Player"))
        {
            FindFirstObjectByType<PlayerController>().Jump();

            anim.SetTrigger("defeated");
            isDefeated = true;

            AudioManager.instance.PlaySFX(6);

        }
    }
}
