using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint;

    public float moveSpeed;

    public float timeAtPoints;
    private float waitCounter;

    public Animator anim;

    public EnemyController theEC;

    public bool shouldChasePlayer;
    private bool isChasing;
    public float distanceToChasePlayer;
    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform _transform in patrolPoints)
        {
            _transform.SetParent(null);  // removes Points from parent, otherwise they would move with the enemy and he never reaches his destination
        }

        waitCounter = timeAtPoints;

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        anim.SetBool("isMoving", true);  // "isMoving must be spelled the same as in the editor

        if (shouldChasePlayer == true)
        {
            thePlayer = FindFirstObjectByType<PlayerController>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (theEC.isDefeated == false)
        { 
            if (shouldChasePlayer == true)
            {
                if (isChasing == false)
                {
                    if(Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToChasePlayer) //gets distance between two points
                    {
                        isChasing = true;
                    }                 
                } 
                else
                {
                    if (Vector3.Distance(transform.position, thePlayer.transform.position) > distanceToChasePlayer) 
                    {
                        isChasing = false;

                        if (transform.position.x < patrolPoints[currentPoint].position.x)
                        {
                            transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        else
                        {
                            transform.localScale = Vector3.one;
                        }
                    }
                }
            }

            if (shouldChasePlayer == false || (shouldChasePlayer == true && isChasing == false) ) // || = or
            {                
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    
                if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 0.001f)
                {
                    waitCounter -= Time.deltaTime;

                    anim.SetBool("isMoving", false);

                    if (waitCounter <= 0)
                    {
                        currentPoint++;
                        if (currentPoint >= patrolPoints.Length)
                        {
                            currentPoint = 0;
                        }

                        waitCounter = timeAtPoints;

                        anim.SetBool("isMoving", true);

                        if (transform.position.x < patrolPoints[currentPoint].position.x)
                        {
                            transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        else
                        {
                            transform.localScale = Vector3.one;
                        }
                    }
                }
            }
            else if (isChasing == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * 1.5f * Time.deltaTime);

                if (transform.position.x > thePlayer.transform.position.x)
                {
                    transform.localScale = Vector3.one;
                }
                else
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
        }
    }
}
