using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
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
    protected void Update()
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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Supply();

    }

    public virtual void Supply()
    {

    }

    protected void OnBecameInvisible()
    {
        isInvisible = true;
    }

    protected void OnBecameVisible()
    {
        isInvisible = false;
        time = 0;
    }
}
