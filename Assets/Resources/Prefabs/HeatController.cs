using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatController : MonoBehaviour
{
    bool isLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = 1;
        if(gameObject.transform.parent.CompareTag("Enemy"))
        {
            if(!isLeft)
                xPos = -2;
            else
                xPos = 1;
        }
        else if (GameManager.player.GetComponent<PlayerController>().isLeft == true)
        {
            if (transform.parent.parent.name == "LeftArmPart")
                xPos = -2;
            else if (transform.parent.parent.name == "RightArmPart")
                xPos = 1;
        }
        else if (GameManager.player.GetComponent<PlayerController>().isLeft == false)
        {
            if (transform.parent.parent.name == "LeftArmPart")
                xPos = 1;
            else if (transform.parent.parent.name == "RightArmPart")
                xPos = -2;
        }
        transform.localPosition = new Vector3(xPos, 0, 0);
    }
}
