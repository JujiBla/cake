using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    //public Transform target;

    public bool freezeVertical, freezeHorizontal;
    private Vector3 positionStore;

    public bool clampPosition;
    public Transform clampMin, clampMax;
    private float halfWidth, halfHeight;
    public Camera theCam;

    private Vector3 targetPoint = Vector3.zero;

    public PlayerController player;

    public float moveSpeed;

    public float lookAheadDistance = 5f, lookAheadSpeed = 3f;

    private float lookOffset;

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
        //targetPoint.x = player.transform.position.x;
        targetPoint.y = player.transform.position.y;       


        if (freezeVertical == true)
        {
            transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z);
        }
        if (freezeHorizontal == true) //bool can only be true or false, == true not needed, but makes it more readable
        {
            transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z);
        }

        if (clampPosition == true)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth),
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight),
                transform.position.z);

        }

        if(player.theRB.velocity.x > 0f)
        {
            lookOffset = Mathf.Lerp(lookOffset, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }

        if (player.theRB.velocity.x < 0f)
        {
            lookOffset = Mathf.Lerp(lookOffset, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }

        targetPoint.x = player.transform.position.x + lookOffset;
                
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        
        if (ParallaxBackground.instance != null)
        {
            ParallaxBackground.instance.MoveBackground();
        }

    }

    private void OnDrawGizmos() //Gizmos are things drawn in the scene view (grid, camera outline) you cant seein game
    {
        if (clampPosition == true)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));

            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
        }
    }

}