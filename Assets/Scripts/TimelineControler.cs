using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineControler : MonoBehaviour
{
    PlayableDirector playableDirector;

    public void Play(float time)
    {
        playableDirector.time = time;
    }

}
