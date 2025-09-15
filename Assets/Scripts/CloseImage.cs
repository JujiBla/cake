using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseImage : MonoBehaviour
{
    void Update()
    {        
        if (Input.GetButtonDown("Jump"))
        {
            gameObject.SetActive(false); 
        }
    }
}
