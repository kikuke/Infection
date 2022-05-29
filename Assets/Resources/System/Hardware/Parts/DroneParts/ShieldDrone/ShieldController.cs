using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : Shield
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        shield = 1;
        GameManager.player.GetComponent<PlayerController>().shield = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
