using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data3Controller : Data
{
    private float supply = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override void Supply()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().Supply(supply);
        SpawnManager.Instance.KeepObject(gameObject);
    }

    new private void OnBecameInvisible()
    {
        base.OnBecameInvisible();
    }
    new private void OnBecameVisible()
    {
        base.OnBecameVisible();
    }
}
