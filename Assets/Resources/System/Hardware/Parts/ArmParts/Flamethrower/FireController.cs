using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public Vector3 target;
    private Vector3 targetVector;
    private Quaternion previousAngle;
    public float damage;
    private float time = 0;
    private float delayTime = 1;
    private float rotateSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = Vector3.up * 5;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        targetVector = target;

        float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        previousAngle = transform.parent.rotation = Quaternion.Euler(0, 0, rotateDegree - 90);
    }

    // Update is called once per frame

    private void Update()
    {
        if (target != Vector3.zero)
        {
            targetVector = target;
            float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
            previousAngle = transform.parent.rotation = Quaternion.Slerp(previousAngle, Quaternion.Euler(0, 0, rotateDegree - 90), Time.deltaTime * rotateSpeed);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        collision.GetComponent<Enemy>().OnDamage(damage);
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Enemy>().OnDamage(damage);
    }
}
