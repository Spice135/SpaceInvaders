using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLogic : MonoBehaviour
{
    public GameObject enemy1_prefab; // Prefab for enemy on row 1
    public GameObject enemy2_prefab; // Prefab for enemy on row 2 & 3
    public GameObject enemy3_prefab; // Prefab for enemy on row 4 & 5
    public GameObject bullet_prefab; // Enemy bullet

    public int x_maxSteps; // Max x movements before y movement
    public int y_maxSteps; // Max y movements before death

    private float x_stepSize = 0.5f; // X movement
    private float y_stepSize = 0.5f; // Y movement
    private int current_stepSize = 0; // Current steps taken

    // Initial x and y positions
    private float row1_x = -4.25f;
    private float row1_y = 4.0f;
    private float row2_x = -4.25f;
    private float row2_y = 3.5f;
    private float row3_x = -4.5f;
    private float row3_y = 2.5f;
    private float time = 0.0f;

    private GameObject[] enemy_1; // Enemy type 1
    private GameObject[,] enemy_2; // Enemy type 2
    private GameObject[,] enemy_3; // Enemy type 3

    // Start is called before the first frame update
    void Start()
    {
        enemy_1 = new GameObject[10]; // 10 enemies on the top row
        enemy_2 = new GameObject[2, 10]; // 2x10 enemies on the next 2 rows
        enemy_3 = new GameObject[2, 11]; // 2x11 enemies on the last 2 rows

        for(int i = 0; i < 10; ++i)
        {
            enemy_1[i] = Instantiate(enemy1_prefab, new Vector3(row1_x + (x_stepSize * i), row1_y, 0.0f), new Quaternion(0, 0, 0, 0)); // Create first row of enemies, store in array
        }

        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                enemy_2[i, j] = Instantiate(enemy2_prefab, new Vector3(row2_x + (x_stepSize * j), row2_y - (y_stepSize * i), 0.0f), new Quaternion(0, 0, 0, 0)); // Create second set of enemies, store in array
            }
        }

        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 11; ++j)
            {
                enemy_3[i, j] = Instantiate(enemy3_prefab, new Vector3(row3_x + (x_stepSize * j), row3_y - (y_stepSize * i), 0.0f), new Quaternion(0, 0, 0, 0)); // Create third set of enemies, store in array
            }
        }

        //InvokeRepeating("MoveEnemy", 1f, 1f);  // Run MoveEnemy every second
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(time > 1.0f - (0.015 * (52 - numEnemies)))
        {
            time = 0.0f;
            MoveEnemy();
        }

        GameObject[] bulletArr = GameObject.FindGameObjectsWithTag("EnemyBullet");
        // Randomly fire a bullet
        if (bulletArr.Length == 0)
        {
            int rand = Random.Range(0, 3);
            if(rand == 0)
            {
                int rand2 = Random.Range(0, 10);
                if(enemy_1[rand2] != null && enemy_1[rand2].activeSelf)
                    Instantiate(bullet_prefab, enemy_1[rand2].transform.position, new Quaternion(0,0,0,0));
            }
            else if(rand == 1)
            {
                int rand2 = Random.Range(0, 2);
                int rand3 = Random.Range(0, 10);
                if(enemy_2[rand2, rand3] != null && enemy_2[rand2, rand3].activeSelf)
                    Instantiate(bullet_prefab, enemy_2[rand2, rand3].transform.position, new Quaternion(0, 0, 0, 0));
            }
            else // if (rand == 2)
            {
                int rand2 = Random.Range(0, 2);
                int rand3 = Random.Range(0, 11);
                if(enemy_3[rand2, rand3] != null && enemy_3[rand2, rand3].activeSelf)
                    Instantiate(bullet_prefab, enemy_3[rand2, rand3].transform.position, new Quaternion(0, 0, 0, 0));
            }

        }
        else 
        {
            foreach(GameObject bullet in bulletArr)
            {
                if(bullet.transform.position.y < -5.0f)
                {
                    Destroy(bullet); // Destroy off screen bullets
                }
            }
        }

        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Move all the enemies
    void MoveEnemy()
    {
        ++current_stepSize; // Increment the steps taken
        if (current_stepSize > x_maxSteps) // If more steps than max, move down
        {
            current_stepSize = 0; // Reset step size
            x_stepSize *= -1.0f; // Reverse x direction

            // For all gameobjects, reduce y by y stepsize
            for (int i = 0; i < 10; ++i)
            {
                if(enemy_1[i])
                    enemy_1[i].transform.position = new Vector3(enemy_1[i].transform.position.x, enemy_1[i].transform.position.y - y_stepSize, 0);
            }

            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    if(enemy_2[i,j])
                        enemy_2[i, j].transform.position = new Vector3(enemy_2[i, j].transform.position.x, enemy_2[i, j].transform.position.y - y_stepSize, 0);
                }
            }

            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 11; ++j)
                {
                    if (enemy_3[i, j])
                        enemy_3[i, j].transform.position = new Vector3(enemy_3[i, j].transform.position.x, enemy_3[i, j].transform.position.y - y_stepSize, 0);
                }
            }
        }
        else
        {
            // For all gameobjects, move by x stepsize
            for (int i = 0; i < 10; ++i)
            {
                if (enemy_1[i])
                    enemy_1[i].transform.position = new Vector3(enemy_1[i].transform.position.x + x_stepSize, enemy_1[i].transform.position.y, 0);
            }

            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    if (enemy_2[i, j])
                        enemy_2[i, j].transform.position = new Vector3(enemy_2[i, j].transform.position.x + x_stepSize, enemy_2[i, j].transform.position.y, 0);
                }
            }

            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 11; ++j)
                {
                    if (enemy_3[i, j])
                        enemy_3[i, j].transform.position = new Vector3(enemy_3[i, j].transform.position.x + x_stepSize, enemy_3[i, j].transform.position.y, 0);
                }
            }
        }


    }
}
