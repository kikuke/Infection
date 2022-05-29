using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float time;
    private float coolTime = 0.3f;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        time = coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= coolTime)
        {
            GameObject _bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            time = 0;
        }
    }
}
