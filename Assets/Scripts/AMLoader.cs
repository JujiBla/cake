using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMLoader : MonoBehaviour
{
    public AudioManager theAM; 

    private void Awake()
    {
        if(AudioManager.instance == null)
        {
            Instantiate(theAM).SetupAudioManager();    
            //bc of the way the AM is brought in, there might be an issue bc its awake function will be called
            //after start function of other objects. (for example: level music pl goes first, AM is not there, then AM 
            //gets instantiated and the music wont play and you get an error. 
}
        }
    }

    //this whole script exists so you can have an audio manager if you dont start the game with the main menu.
    //when developing you start in a certain scene, this scene will not have an audio manager
    //with they way the am is set up. This is only relevant when working in the editor. 


