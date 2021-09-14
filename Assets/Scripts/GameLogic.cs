using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public int roundCounter = 0; // Current round
    public int scoreTotal = 0; // Current score
    public int score = 0; // Current score
    public GameObject explosion;
    public GameObject camera;
    public GameObject bgAudio;
    public Text scoreText;
    private bool paused = false;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 30;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckScore", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Time.timeScale = 0f;
            AudioSource[] arr = (AudioSource[])(FindObjectsOfType(typeof(AudioSource)));
            foreach (var vol in arr)
                    vol.volume = 0f;
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Time.timeScale = 1f;
            AudioSource[] arr = (AudioSource[])(FindObjectsOfType(typeof(AudioSource)));
            foreach (var vol in arr)
                vol.volume = 1f;
            paused = false;
        }
        // Max score without spaceship = 920
        scoreText.color = Color.Lerp(Color.white, Color.green, score/920f);
        scoreText.text = "SCORE: " + score;
    }

    void CheckScore()
    {
        if (score < scoreTotal)
            score++;
    }

    public void SlowDown(Vector3 pos)
    {
        StartCoroutine(SlowDownCoroutine());
        Instantiate(explosion, pos, new Quaternion(0, 0, 0, 0)); // Create an explosion
        StartCoroutine(ScreenShake());
    }

    public void AddScore(int points)
    {
        scoreTotal += points;
    }

    IEnumerator ScreenShake()
    {
        yield return new WaitForSeconds(0.15f);
        camera.GetComponent<ScreenShake>().TriggerShake();
    }

    IEnumerator SlowDownCoroutine()
    {
        Time.timeScale = 0.1f;
        AudioSource[] arr = (AudioSource[])(FindObjectsOfType(typeof(AudioSource)));
        foreach(var pitch in arr)
        {
            //if(pitch.pitch > 0.1f && pitch.pitch <= 1.0f)
            {
                pitch.pitch = 0.1f;
            }
        }
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1.0f;

        AudioSource[] arr2 = (AudioSource[])(FindObjectsOfType(typeof(AudioSource)));
        foreach (var pitch in arr2)
        {
            //if (pitch.pitch < 1.0f && pitch.pitch >= 0.1f)
            {
                pitch.pitch = 1.00f;
            }
        }
    }
}
