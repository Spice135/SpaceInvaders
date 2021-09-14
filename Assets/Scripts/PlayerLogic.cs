using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    public GameObject playerBullet;
    public GameObject cam;
    public GameObject dashR;
    public GameObject dashL;
    public GameObject expl;
    public int lives = 3;
    public bool getDamage = false;
    public Image dmgImg;
    public GameObject hurt1;
    public GameObject hurt2;
    public Text livesText;

    private int ButtonCountL = 0;
    private float ButtonCoolerL = 0.5f;
    private int ButtonCountR = 0;
    private float ButtonCoolerR = 0.5f;
    private float speedL = 25.0f;
    private float speedR = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        livesText.text = "LIVES: " + lives;
        if (lives == 2)
        {
            livesText.color = new Color(1f, 1f, 0);
            hurt1.SetActive(true);
        }
        if (lives == 1)
        {
            livesText.color = new Color(1f, 0f, 0);
            hurt1.SetActive(false);
            hurt2.SetActive(true);
        }


        if (lives <= 0)
        {
            cam.GetComponent<ScreenShake>().TriggerShake(); // Screen shake
            Instantiate(expl, transform.position, new Quaternion(0, 0, 0, 0));
            Invoke("Reset", 1.25f);
            
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (ButtonCoolerL > 0 && ButtonCountL == 1)
            {
                // Has double tapped
                speedL = 125.0f;
                Instantiate(dashL, gameObject.transform.position, new Quaternion(90, 90, 0, 0));
                Invoke("ResetL", 0.1f);
            }
            else
            {
                ButtonCoolerL = 0.15f;
                ButtonCountL += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (ButtonCoolerR > 0 && ButtonCountR == 1)
            {
                // Has double tapped
                speedR = 125.0f;
                Instantiate(dashR, gameObject.transform.position, new Quaternion(-90, 90, 0, 0));
                Invoke("ResetR", 0.1f);
            }
            else
            {
                ButtonCoolerR = 0.15f;
                ButtonCountR += 1;
            }
        }

        // Reset values
        if (ButtonCoolerL > 0)
            ButtonCoolerL -= 1 * Time.deltaTime;
        else
            ButtonCountL = 0;

        // Reset values
        if (ButtonCoolerR > 0)
            ButtonCoolerR -= 1 * Time.deltaTime;
        else
            ButtonCountR = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Move left
        {
            if(gameObject.transform.position.x > -4.5f)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - (0.005f * Time.timeScale * speedL), gameObject.transform.position.y, 0);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Move right
        {
            if (gameObject.transform.position.x < 4.5f)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + (0.005f * Time.timeScale * speedR), gameObject.transform.position.y, 0);
        }


        if (Input.GetKeyDown(KeyCode.Space)) // Shoot bullet
        {
            if (GameObject.FindGameObjectsWithTag("Bullet").Length == 0)
            {
                Instantiate(playerBullet, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.25f, 0), new Quaternion(0, 0, 0, 0));
            }
        }

        if (getDamage)
        {
            Color Opaque = new Color(1, 0, 0, 1);
            dmgImg.color = Color.Lerp(dmgImg.color, Opaque, 2 * Time.deltaTime);
            if (dmgImg.color.a >= 0.8) //Almost Opaque, close enough
            {
                getDamage = false;
            }
        }
        if (!getDamage)
        {
            Color Transparent = new Color(1, 0, 0, 0);
            dmgImg.color = Color.Lerp(dmgImg.color, Transparent, 2 * Time.deltaTime);
        }
    }

    private void Reset()
    {
        // Reset scene here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetL()
    {
        speedL = 25.0f;
    }

    void ResetR()
    {
        speedR = 25.0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reset scene
    }
}
