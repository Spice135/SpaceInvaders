using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletLogic : MonoBehaviour
{
    public GameObject enemyDeath_prefab;
    public GameObject explosion;
    public Sprite enemyDeath_Sprite;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        // Move bullet every .25 second
        InvokeRepeating("MoveBullet", 0.05f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y > 5.0f)
            Destroy(gameObject); // If off screen, kys
    }

    void MoveBullet()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.35f, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            //Instantiate(enemyDeath_prefab, collision.gameObject.transform.position, new Quaternion(0, 0, 0, 0)); // Create death sprite
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = enemyDeath_Sprite;
            collision.gameObject.GetComponent<EnemySpriteLogic>().isDead = true;
            collision.gameObject.GetComponent<EnemySpriteLogic>().getDamage = true;
            Instantiate(explosion, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(collision.gameObject, 1.5f); // Destroy enemy
            Destroy(gameObject); // Destroy bullet
        }

        if (collision.gameObject.tag == "Spaceship")
        {
            Instantiate(explosion, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>().AddScore(50); // Add 50 to score
            Destroy(collision.gameObject, 1.0f); // Destroy enemy
            Destroy(gameObject); // Destroy bullet
        }

        if (collision.gameObject.tag == "Barrier")
        {
            collision.gameObject.GetComponent<BarrierLogic>().BarrierAudio();
            collision.gameObject.GetComponent<BarrierLogic>().lives -= 1;
            Destroy(gameObject); // Destroy bullet
        }
    }
}
