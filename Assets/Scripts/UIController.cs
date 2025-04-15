using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;                    //singleton?
    }


    public Image[] heartIcons;                //adds using UnityEngine.UI at the top 
                                              // [] makes something an array

    public Sprite heartFull, heartEmpty;

    public TMP_Text livesText, collectibleText;              //adds using TMPro at the top

    public GameObject gameOverScreen;

    public GameObject pauseScreen;

    public string mainMenuScene;

    public Image fadeScreen;
    public float fadeSpeed;
    public bool fadingToBlack, fadingFromBlack;

    // Start is called before the first frame update
    void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

        if(fadingFromBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        if (fadingToBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }

    public void UpdateHealthDisplay(int health, int maxHealth)
    {
        for(int i = 0; i < heartIcons.Length; i++)      //create i, set to 0, is i smaller thant number of hearts? yes (bc its 0), jumps to loop, than does add one to i, goes back to check if i < length
                                                        //loops throu array and looks at each point in the list
        {
            heartIcons[i].enabled = true; //.enabled turnes images on/off in unity inspector

            /*if(health <= i)
            {
                heartIcons[i].enabled = false;
            }*/

            if(health > i)
            {
                heartIcons[i].sprite = heartFull;
            } else
            {
                heartIcons[i].sprite = heartEmpty;

                if(maxHealth <= i)
                {
                    heartIcons[i].enabled = false;
                }
            }
        }
        
    }

    public void UpdateLivesDisplay(int currentLives)
    {
        livesText.text = currentLives.ToString();
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        //Debug.Log("Restarting");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Get active scene name. also adds unity scene manager on top

        Time.timeScale = 1f;
    }

    public void UpdateCollectibles(int amount)
    {
        collectibleText.text = amount.ToString();
    }

    public void PauseUnpause()
    {
        if(pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        { 
            pauseScreen.SetActive(false);

            Time.timeScale = 1f;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);

        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("I Quit");
    }

    public void FadeFromBlack()
    {
        fadingToBlack = false;
        fadingFromBlack = true;
    }

    public void FadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }
}
