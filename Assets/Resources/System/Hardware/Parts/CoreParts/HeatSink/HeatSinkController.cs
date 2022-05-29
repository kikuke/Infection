using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSinkController : Part
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
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "냉각 쿨러" + "</color>";
        string description =
            showName + "\n" +
            "공격 및 스킬 쿨타임을 " + (int)(float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "CooltimePer")][partName].ToString()) * 100) + "% 줄여준다.\n";

        return description;
    }
}
