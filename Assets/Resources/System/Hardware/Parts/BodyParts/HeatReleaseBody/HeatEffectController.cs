using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatEffectController : MonoBehaviour
{
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Enemy>().OnDamage(damage);
    }
}
