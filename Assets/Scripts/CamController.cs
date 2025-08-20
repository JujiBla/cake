using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public bool freezeVertical, freezeHorizontal;
    private Vector3 positionStore;

    public bool clampPosition;
    public Transform clampMin;
    public Transform clampMax;
    private float halfWidth;
    private float halfHeight;
    public Camera theCam;

    private Vector3 targetPoint = Vector3.zero;

    public PlayerController player;

    public float moveSpeed;

    public float lookAheadDistance = 5f;
    public float lookAheadSpeed = 3f;

    private float lookOffsetX;
    private float lookOffsetY;

    private bool isFalling;
    private bool isJumping;
    public float maxVertOffset = 5f;

    private bool justLoaded = true;
    private bool lerpY = true;
    private Vector3 lerpFallingOffset = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPoint = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z); 

        positionStore = transform.position;

        clampMax.SetParent(null);
        clampMin.SetParent(null);

        halfHeight = theCam.orthographicSize;
        halfWidth = theCam.orthographicSize * theCam.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position.y - player.transform.position.y < maxVertOffset)
        {
            isJumping = true;
        }

        if (isJumping)
        {
            targetPoint.y = player.transform.position.y;

            if (player.isGrounded)
            {
                isJumping = false;
            }
        }

        if (transform.position.y - player.transform.position.y > maxVertOffset)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            targetPoint.y = player.transform.position.y;

            if (player.isGrounded)
            {
                isFalling = false;
                lerpY = true;
            }
        }

        //if (freezeVertical == true)
        //{
        //    transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z);
        //}
        //if (freezeHorizontal == true)
        //{
        //    transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z);
        //}
        
        // (player.theRB.velocity == Vector2.zero)
        if (player.theRB.velocity.x == 0f && player.theRB.velocity.y == 0f)
        {
            if (Input.GetKey(KeyCode.S))
            {
                lookOffsetY = Mathf.Lerp(lookOffsetY, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
                targetPoint.y = player.transform.position.y + lookOffsetY;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                lookOffsetY = Mathf.Lerp(lookOffsetY, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
                targetPoint.y = player.transform.position.y + lookOffsetY;
            }
            else
            {
                lookOffsetY = 0;
            }
        }
        else
        {
            lookOffsetY = 0;

            if (player.theRB.velocity.x > 0f)
            {
                lookOffsetX = Mathf.Lerp(lookOffsetX, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
            }
            else if (player.theRB.velocity.x < 0f)
            {
                lookOffsetX = Mathf.Lerp(lookOffsetX, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
            }
            
            targetPoint.x = player.transform.position.x + lookOffsetX;
        }


        if (justLoaded) //camera stands still after intro cutscene, before it was moving a tiny bit
        {

            transform.position = targetPoint;
            justLoaded = false;
            lerpY = true;
            //print("just loaded, force position " + transform.position);
        }
        else
        {
            if (lerpY)
            {
                transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
                
                // if mimi gets out of cam while falling force hard following
                if (theCam.WorldToScreenPoint(player.transform.position).y <= 40f)
                {
                    lerpY = false;
                    lerpFallingOffset = transform.position - targetPoint;
                    //transform.position = targetPoint + lerpFallingOffset;
                    //transform.position = Vector3.Lerp(transform.position, targetPoint + lerpFallingOffset, 100 * Time.deltaTime);
                }
            }
            else
            {
                //transform.position = targetPoint + lerpFallingOffset + new Vector3(0f, -1f, 0f);
                transform.position = Vector3.Lerp(transform.position, targetPoint + lerpFallingOffset + new Vector3(0f, -1f, 0f), 40 * Time.deltaTime);
            }
            //print("no longer just loaded, force position " + transform.position + ", wanted target " + targetPoint + ", halfwidth " + halfWidth + ", halfheight " + halfHeight);
        }
        

        if (clampPosition == true)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth),
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight),
                transform.position.z
            );
        }
        //print("target " + targetPoint + ", clampmin " + clampMin.position + ", clampmax " + clampMax.position + ", transform" + transform.position);


        if (ParallaxBackground.instance != null)
        {
            ParallaxBackground.instance.MoveBackground();
        }
    }

    private void OnDrawGizmos() //Gizmos are things drawn in the scene view (grid, camera outline) you cant see in game
    {
        if (clampPosition == false)
        { return; }

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
        Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));

        Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
        Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
    }

}