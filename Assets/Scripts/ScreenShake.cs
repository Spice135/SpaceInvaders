using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Transform trans;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.15f;
    private float dampingSpeed = 0.5f;
    private bool isDone = false;
    Vector3 initialPosition;
    void Awake()
    {
        if (trans == null)
        {
            trans = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = trans.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            isDone = false;
            trans.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else if(shakeDuration <= 0 && isDone == false)
        {
            isDone = true;
            shakeDuration = 0f;
            trans.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 0.5f;
    }
}
