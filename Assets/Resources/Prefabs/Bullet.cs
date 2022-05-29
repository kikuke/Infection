using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetVector;
    public float damage;
    // Start is called before the first frame update
    public void Start()
    {
        targetVector = target.transform.position - transform.position;

        float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetVector.normalized * 30f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnDamage(damage);
            SpawnManager.Instance.KeepObject(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            SpawnManager.Instance.KeepObject(gameObject);
    }
}
