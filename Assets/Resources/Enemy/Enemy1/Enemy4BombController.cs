using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4BombController : MonoBehaviour
{
    public float damage;

    private void OnEnable()
    {
        Invoke("KeepObject", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void KeepObject()
    {
        SpawnManager.Instance.KeepObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().OnDamage(damage);
        }
    }
}
