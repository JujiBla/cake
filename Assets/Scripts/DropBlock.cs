using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DropBlock : MonoBehaviour
{
    private Transform player;

    public float activationRange = .5F; //how close player is horizontally (x)

    public float fallSpeed, raiseSpeed;

    public Transform dropPoint;

    private bool activated;

    private Vector3 startPoint;

    public float waitToFall, waitToRaise;
    private float fallCounter, raiseCounter;

    public Animator anim;

    private float t; // interpolation timer;

    public float fallDuration = 0.5f;
    public float raiseDuration = 0.5f;

    private bool isFalling;
    private bool isRaising;

    // Start is called before the first frame update
    void Start()
    {
        player = PlHealthController.instance.transform;

        dropPoint.SetParent(null);

        startPoint = transform.position;

        fallCounter = waitToFall;
        raiseCounter = waitToRaise;

    }

    // Update is called once per frame
    void Update()
    {
        if(activated == false && transform.position == startPoint)
        {
            isRaising = false;
            if (Mathf.Abs(transform.position.x - player.position.x) <= activationRange && player.position.y < transform.position.y) //Mathf.abs removes - when player is to the right of obj
            {
                activated = true;

                anim.SetTrigger("blink");
            }
        }

        if(activated == true)
        {
            if (fallCounter > 0)
            {
                fallCounter -= Time.deltaTime;
            }
            else
            {
                if (!isFalling)
                {
                    isFalling = true;
                    t = 0;
                }

                if (t < 1)
                {
                    
                    t += Time.deltaTime / fallDuration;
                    float easedT = Mathf.SmoothStep(0, 1, t);
                    transform.position = Vector3.Lerp(startPoint, dropPoint.position, easedT);
                }
                else
                {
                    transform.position = dropPoint.position;
                    ActivateHit();
                    isFalling = false;


                }
            }
        } 
        else
        {
            if (raiseCounter > 0)
            {
                raiseCounter -= Time.deltaTime;
                
            }
            else
            {
                if (transform.position != startPoint)
                {
                    
                    if (isRaising == false)

                    {
                        isRaising = true;
                        t = 0;
                    }

                    if (t < 1)
                    {
                        t += Time.deltaTime / raiseDuration;
                        float easedT = Mathf.SmoothStep(0, 1, t);
                        transform.position = Vector3.Lerp(dropPoint.position, startPoint, easedT);
                    }
                    else
                    {
                        transform.position = startPoint;
                        isRaising = false;
                    }
                }
            }
        }
    }

    void ActivateHit()
    {
        activated = false;

        fallCounter = waitToFall;
        raiseCounter = waitToRaise;

        anim.SetTrigger("hit");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ActivateHit();

            PlHealthController.instance.DamagePlayer();
        }
    }

}
