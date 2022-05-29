using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int shield;
    // Start is called before the first frame update
    public void Start()
    {
        transform.SetParent(GameManager.player.transform);
        transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    public void Update()
    {
        if (shield <= 0)
            Destroy(gameObject);
    }
}
