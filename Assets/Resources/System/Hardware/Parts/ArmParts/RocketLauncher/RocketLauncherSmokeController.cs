﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherSmokeController : MonoBehaviour
{
    public Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(SetPos());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = originPos;
    }

    private IEnumerator SetPos()
    {
        while (transform.parent.gameObject.activeSelf)
        {
            originPos = transform.parent.position;

            yield return new WaitForSeconds(2f);
        }
    }
}
