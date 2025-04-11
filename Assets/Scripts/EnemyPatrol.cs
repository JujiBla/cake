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

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in patrolPoints)
        {
            t.SetParent(null);      //removes Points from parent, otherwise they would move with the enemy and he never reaches his destination
        }

        waitCounter = timeAtPoints;

        

        anim.SetBool("isMoving", true); //"isMoving must be spelled the same as in the editor
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    
        if(Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .001f)
        {
            waitCounter -= Time.deltaTime;

            anim.SetBool("isMoving", false);

            if(waitCounter <= 0)
            { 

                currentPoint++;
                if(currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }

                waitCounter = timeAtPoints;

                anim.SetBool("isMoving", true);
            }
        }
    }
}
