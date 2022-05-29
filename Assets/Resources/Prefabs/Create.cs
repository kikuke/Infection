using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject part;
    private GameObject targetPart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputButton()
    {
        if (part.tag == "Arm")
            targetPart = GameManager.player.transform.Find("Hardwares/Parts/LeftArmPart").gameObject;
        else if(part.tag == "Drone")
            targetPart = GameManager.player.transform.Find("Hardwares/Parts/DronePart/Drone0").gameObject;
        else
            targetPart = GameManager.player.transform.Find("Hardwares/Parts/" + part.tag + "Part").gameObject;


        GameManager.player.GetComponent<PlayerController>().MountPart(targetPart, Instantiate(part));
    }
}
