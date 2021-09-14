using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletLogic : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public GameObject gameLogic;
    private GameObject player;
    private bool isDone = false;
    public GameObject NMiss;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        // Change sprite every 1/4 second
        InvokeRepeating("Sprite1", 0.2f, 0.2f);
        InvokeRepeating("Sprite2", 0.1f, 0.2f);

        // Move bullet every .1 second
        InvokeRepeating("MoveBullet", 0.1f, 0.1f);

        gameLogic = GameObject.FindGameObjectWithTag("Logic"); // Get game logic
        player = GameObject.FindGameObjectWithTag("Player"); // Get player
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if(Vector3.Distance(transform.position, player.transform.position) < 0.6f && !isDone) // Near miss
        {
            Time.timeScale = 0.1f;
            isDone = true;
            Invoke("ResetTime", 0.2f);
            Invoke("NearMiss", 0.2f);
        }
    }

    void ResetTime()
    {
        Time.timeScale = 1.0f;
    }

    void NearMiss()
    {
        Instantiate(NMiss, player.transform.position, new Quaternion(0, 0, 0, 0)); // Create near miss text
    }

    void Sprite1()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
    }

    void Sprite2()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
    }

    void MoveBullet()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.35f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameLogic.GetComponent<GameLogic>().SlowDown(gameObject.transform.position);
            collision.gameObject.GetComponent<PlayerLogic>().lives -= 1; // Reduce lives by 1
            collision.gameObject.GetComponent<PlayerLogic>().getDamage = true; // Player has been damaged
            Time.timeScale = 1.0f;
            Destroy(gameObject); // Destroy bullet
        }

        if(collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "Barrier")
        {
            Time.timeScale = 1.0f;
            collision.gameObject.GetComponent<BarrierLogic>().BarrierAudio();
            collision.gameObject.GetComponent<BarrierLogic>().lives -= 1;
            Destroy(gameObject); // Destroy bullet
        }
    }
}
