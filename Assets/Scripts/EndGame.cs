using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public string mainMenu;
    public GameObject endGameDialog;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        { return; }

        if (endGameDialog.activeSelf == false)
        {
            endGameDialog.SetActive(true);

            Time.timeScale = 0f;
        }
    }


    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Cancel()
    {
        endGameDialog.SetActive(false);

        Time.timeScale = 1f;
    }
}
