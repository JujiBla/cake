using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dissolve : MonoBehaviour
{
    Material material;

    bool isDissolving = true;
    float dissolveAmount = 0.6f;
    public float dissolveTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<TilemapRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount -= Time.deltaTime * dissolveTime;

            if (dissolveAmount <= 0.5f)
            {
                dissolveAmount = 0.5f;
                isDissolving = false;
            }

            material.SetFloat("_DissolveAmount", dissolveAmount);
        }

        if (!isDissolving)
        {
            dissolveAmount += Time.deltaTime * dissolveTime;

            if (dissolveAmount >= 0.6f)
            {
                dissolveAmount = 0.6f;
                isDissolving = true;
            }

            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }
}
