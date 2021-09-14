using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierLogic : MonoBehaviour
{
    public int lives = 2;
    public Sprite sprite1;
    public Sprite sprite2;
    public GameObject logic;
    public GameObject explosion;
    public GameObject smoke;
    bool smokeMade = false;
    GameObject smokeInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lives < 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite2; // Set destroyed sprite
            if(!smokeMade)
            {
                smokeInstance = Instantiate(smoke, transform.position, new Quaternion(270, 90, 0, 0));
                smokeMade = true;
            }
        }

        if (lives < 1)
        {
            Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0)); // Create an explosion
            //logic.GetComponent<GameLogic>().SlowDown(gameObject.transform.position);
            Destroy(smokeInstance);
            Destroy(gameObject);
        }
    }

    public void BarrierAudio()
    {
        GetComponent<AudioSource>().Play();
    }
    
}
