using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderHeadController : Part
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
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "레이더 헤드" + "</color>";
        string description =
            showName + "\n" +
            "공격 범위를 " + (int)(float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddAtkRangePer")][partName].ToString()) * 100) + "% 늘려준다.\n";

        return description;
    }
}
