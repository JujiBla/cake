using UnityEngine;

public class ClickableImage : MonoBehaviour
{
    public GameObject bigImageUI; 

    void OnMouseDown()
    {
        bigImageUI.SetActive(true);
    }
}