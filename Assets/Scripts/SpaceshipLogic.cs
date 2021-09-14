using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipLogic : MonoBehaviour
{
    private bool goingLeft = false;
    private bool isDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.x > 4.5f || transform.position.x < -4.5f) && isDone)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            isDone = false;
        }
        else if ((transform.position.x <= 4.5f && transform.position.x >= -4.5f) && !isDone)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<AudioSource>().Play();
            isDone = true;
        }

        if (!goingLeft)
            transform.position = new Vector3(transform.position.x + (0.375f * Time.timeScale), transform.position.y, 0f);

        if (goingLeft)
            transform.position = new Vector3(transform.position.x - (0.375f * Time.timeScale), transform.position.y, 0f);

        if (transform.position.x >= 50.0f)
            goingLeft = true;

        if (transform.position.x <= -50.0f)
            goingLeft = false;
    }
}
