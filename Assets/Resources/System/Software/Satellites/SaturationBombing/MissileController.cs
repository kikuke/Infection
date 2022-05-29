using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public bool isPlayer = true;
    public float damage;
    // Start is called before the first frame update
    public void Start()
    {
        Invoke("Destroy", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.one;
    }

    void Destroy()
    {
        SpawnManager.Instance.KeepObject(gameObject);
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
}
