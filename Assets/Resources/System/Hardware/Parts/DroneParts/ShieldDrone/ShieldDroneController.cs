using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDroneController : Part
{
    private float radius = 5;
    private float runningTime = 0;
    private float speed = 2;
    private float time = 0;

    public GameObject shield;
    private GameObject child;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.root.Find("Hardwares/Parts/DronePart").localScale = new Vector3(transform.root.Find("Hardwares").localScale.x, 1, 1);

        runningTime += Time.deltaTime * speed;
        Vector2 cycleVector = new Vector2(radius * Mathf.Cos(runningTime), radius * Mathf.Sin(runningTime));
        transform.localPosition = (Vector3)cycleVector;
        
        Shield();
    }

    private void Shield()
    {
        if(time >= cooltime)
        {
            time = 0;
            if (child == null)
                child = Instantiate(shield) as GameObject;
        }
        else
            time += Time.deltaTime;
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "실드 드론" + "</color>";
        string description =
            showName + "\n" +
            "보호 벽을 생성한다.\n";

        return description;
    }
}
