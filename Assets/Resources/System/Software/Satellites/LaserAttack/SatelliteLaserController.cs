using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteLaserController : MonoBehaviour
{
    public GameObject target;
    public float damage;
    public float atkRange;
    public float attackTime;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, FindObjectOfType<LaserUpgradeController>() != null ? attackTime * (FindObjectOfType<LaserUpgradeController>().attackTime + 1) : attackTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.parent.name == "Enemy Keep Holder")
            target = null;

        if (target != null)
        {
            transform.position = target.transform.position;
        }

        if(target == null)
            FindEnemy();
    }

    void FindEnemy()
    {
        float distanceCompare = 0;

        Collider2D[] others = Physics2D.OverlapCircleAll(transform.parent.position, atkRange);
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
        if (collision.CompareTag("Enemy") && time >= 0.1f)
        {
            collision.GetComponent<Enemy>().OnDamage(damage);
        }
        time += Time.deltaTime;
    }
}
