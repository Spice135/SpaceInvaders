using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpriteLogic : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite deadSprite;
    public GameObject logic;
    public int score;
    private Image dmgImg;
    public bool isDead = false;
    public bool getDamage = false;
    private bool scoreDone = false;
    // Start is called before the first frame update
    void Start()
    {
        // Change sprite every second
        InvokeRepeating("Sprite1", 2f, 2f);
        InvokeRepeating("Sprite2", 1f, 2f);

        logic = GameObject.FindGameObjectWithTag("Logic");
        dmgImg = GameObject.FindGameObjectWithTag("EnemyImg").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(getDamage && !scoreDone)
        {
            logic.GetComponent<GameLogic>().AddScore(score);
            scoreDone = true;
        }
        if (getDamage)
        {   
            Color Opaque = new Color(0, 1, 0, 1);
            dmgImg.color = Color.Lerp(dmgImg.color, Opaque, 10 * Time.deltaTime);
            if (dmgImg.color.a >= 0.8) //Almost Opaque, close enough
            {
                getDamage = false;
            }
        }
        if (!getDamage)
        {
            Color Transparent = new Color(0, 1, 0, 0);
            dmgImg.color = Color.Lerp(dmgImg.color, Transparent, 10 * Time.deltaTime);
        }
    }

    void Sprite1()
    {
        if(isDead)
            gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
    }

    void Sprite2()
    {
        if (isDead)
            gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
    }
}
