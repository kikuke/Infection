using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearController : MonoBehaviour
{
    public bool isPlayer = true;
    public float damage;
    public Vector3 targetVector;
    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetVector.normalized * 120f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayer)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().OnDamage(damage);
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>().OnDamage(damage);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
