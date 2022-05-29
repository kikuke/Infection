using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= transform.position.y + 16)
        {
            transform.position += Vector3.up * 16;
        }
        if (player.transform.position.x <= transform.position.x - 16)
        {
            transform.position += Vector3.left * 16;
        }
        if (player.transform.position.x >= transform.position.x + 16)
        {
            transform.position += Vector3.right * 16;
        }
        if (player.transform.position.y <= transform.position.y - 16)
        {
            transform.position += Vector3.down * 16;
        }
    }
}
