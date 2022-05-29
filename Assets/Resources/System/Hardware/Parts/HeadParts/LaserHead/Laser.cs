using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetVector;
    private Quaternion previousAngle;
    private float rotateSpeed = 10;
    public float damage;
    public float atkRange;
    public float attackTime;
    // Start is called before the first frame update
    void Start()
    {
        targetVector = target.transform.position - transform.position;

        float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        previousAngle = transform.rotation = Quaternion.Euler(0, 0, rotateDegree);

        Destroy(gameObject, FindObjectOfType<LaserUpgradeController>() != null ? attackTime * (FindObjectOfType<LaserUpgradeController>().attackTime + 1) : attackTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            targetVector = target.transform.position - transform.position;
        
            float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
            previousAngle = transform.rotation = Quaternion.Slerp(previousAngle, Quaternion.Euler(0, 0, rotateDegree), Time.deltaTime * rotateSpeed);
        }

        FindEnemy();
    }

    void FindEnemy()
    {
        float distanceCompare = 0;

        Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, atkRange);
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i].CompareTag("Enemy") && (others[i].gameObject.transform.position - transform.position).magnitude > distanceCompare)
            {
                distanceCompare = (others[i].gameObject.transform.position - transform.position).magnitude;
                target = others[i].gameObject;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnDamage(damage * Time.deltaTime);
        }
    }
}
