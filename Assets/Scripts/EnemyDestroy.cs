using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(explosion, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(gameObject, 0.9f); // Kill youself after a second
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
