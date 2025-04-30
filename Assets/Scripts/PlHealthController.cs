using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlHealthController : MonoBehaviour
{
    //creating a singleton
    public static PlHealthController instance; //public but bc its static doesnt show in unity, stored in memory of unity

    private void Awake() //awake happens bevor start function
    {
        instance = this; //there can only be one instance, makes it easy to access, doesnt have to search all objects in hirarchy (no long load times if you have a lot of objects)
    }


    public int currentHealth, maxHealth;   //whole number, no decimals

    public float invincibilityLength = 1f;
    private float invincibilityCounter;

    public SpriteRenderer theSR;
    public Color normalColor, fadeColor;

    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GetComponent<PlayerController>();

        currentHealth = maxHealth;

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime; 
            if(invincibilityCounter <= 0)
            {
                theSR.color = normalColor;
            }
        }

#if UNITY_EDITOR  //only works in editor,  not in built game

        if(Input.GetKeyDown(KeyCode.H))
        {
            AddHealth(1);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            AddHealth(-1);
        }
#endif
    }

    public void DamagePlayer()
    {
        if(invincibilityCounter <= 0)
        {
            currentHealth--; // -- takes one away
        
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                                
                LifeController.instance.Respawn();
                               
            } else
            {
                invincibilityCounter = invincibilityLength;

                theSR.color = fadeColor;

                thePlayer.KnockBack();

                AudioManager.instance.PlaySFXPitched(13);
            }

                UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
        }
        
    }

    public void AddHealth(int amountToAdd)
    {
        currentHealth += amountToAdd;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
}
