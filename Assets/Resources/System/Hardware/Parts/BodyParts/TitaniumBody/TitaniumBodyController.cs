using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitaniumBodyController : Part
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
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "티타늄 바디" + "</color>";
        string description =
            showName + "\n\n" +
            "티타늄 소재 바디이다.\n" +
            "방어력 +" + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Defend")][partName].ToString());

        return description;
    }
}
