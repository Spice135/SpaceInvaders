using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopOnEnd : MonoBehaviour
{
    public AudioClip loop;
    private bool isDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDone && GetComponent<AudioSource>().isPlaying == false)
        {
            isDone = true;
            GetComponent<AudioSource>().clip = loop;
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
            
    }
}
