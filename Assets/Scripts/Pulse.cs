using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulse : MonoBehaviour
{
    public bool isGoingUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color col = GetComponent<Image>().color;

        if (isGoingUp)
            GetComponent<Image>().color = new Color(col.r, col.g, col.b, col.a + 0.0625f);

        if (!isGoingUp)
            GetComponent<Image>().color = new Color(col.r, col.g, col.b, col.a - 0.0625f);

        if (col.a >= 0.35f)
            isGoingUp = false;
        if (col.a <= 0.0f)
            isGoingUp = true;
    }
}
