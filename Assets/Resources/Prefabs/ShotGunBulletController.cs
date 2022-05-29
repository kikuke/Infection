using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBulletController : MonoBehaviour
{
    public bool isPlayer = true;
    public Vector3 targetVector;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetVector.normalized * 30f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayer)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().OnDamage(damage);
                SpawnManager.Instance.KeepObject(gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>().OnDamage(damage);
                SpawnManager.Instance.KeepObject(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        if(gameObject.activeSelf)
            SpawnManager.Instance.KeepObject(gameObject);
    }
}
