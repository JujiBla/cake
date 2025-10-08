using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public string mainMenuScene;

    public void MainMenu()
        {
        SceneManager.LoadScene(mainMenuScene);

        Time.timeScale = 1f;
    
        }
}
