using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadbandHeadController : Part
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "광대역 헤드" + "</color>";
        string description =
            showName + "\n\n" +
            "드론 +" + int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "DroneNum")][partName].ToString()) + "\n";

        return description;
    }
}
