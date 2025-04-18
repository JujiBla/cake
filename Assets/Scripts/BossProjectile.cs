using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 8f;
    private Vector3 direction;

    public float lifetime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        direction = (PlHealthController.instance.transform.position - transform.position).normalized;
        //Makes this vector have a magnitude of 1. When normalized, a vector keeps the same
        //direction but its length is 1.0.
        //Instead of going the whole distance from point a to b, it goes in the direction
        //with a magnitude of one every frame.. so one unity unit per frame instead the whole
        //thing at once?

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * (Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlHealthController.instance.DamagePlayer();

            Destroy(gameObject);
        }
    }
}
