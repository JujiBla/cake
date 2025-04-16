using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    private bool bossActive;

    public GameObject blockers;

    public Transform camPoint;
    private CamController cameraController;

    public float cameraMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindFirstObjectByType<CamController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossActive == true)
        {
            cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position,
                camPoint.position,
                cameraMoveSpeed * Time.deltaTime);
        }
    }

    public void ActivateBattle()
    {
        bossActive = true;

        blockers.SetActive(true);

        cameraController.enabled = false;
    }
}
