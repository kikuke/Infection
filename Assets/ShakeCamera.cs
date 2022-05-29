using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance = null;
    Vector3 originalPosition;


    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake(float shakeAmount, float shakeTime)
    {
        StartCoroutine(ShakeCo(shakeAmount, shakeTime));
    }

    public IEnumerator ShakeCo(float shakeAmount, float shakeTime)
    {
        while (shakeTime > 0)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * shakeAmount + originalPosition;

            shakeTime -= Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }

    public void Push(Vector2 pushVector, float pushAmount, float pushTime)
    {
        StartCoroutine(PushCo(pushVector, pushAmount, pushTime));
    }

    public IEnumerator PushCo(Vector2 pushVector, float pushAmount, float pushTime)
    {
        float pushValue = pushTime;
        while (pushTime > 0)
        {
            transform.localPosition = -(Vector3)pushVector * pushAmount * (pushTime / pushValue) + originalPosition;

            pushTime -= Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
