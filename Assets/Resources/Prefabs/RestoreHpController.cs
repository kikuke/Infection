using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHpController : MonoBehaviour
{
    private float restoreValue = 2;
    private float time = 0;
    private float destroyTime = 10;
    private bool isInvisible = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        isInvisible = false;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvisible == true)
        {
            if (time >= destroyTime)
            {
                SpawnManager.Instance.KeepObject(gameObject);
            }
            time += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Restore(restoreValue);
            SpawnManager.Instance.KeepObject(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        isInvisible = true;
    }

    private void OnBecameVisible()
    {
        isInvisible = false;
        time = 0;
    }
}
