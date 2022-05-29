using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExoskeletonBodyController : Part
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
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "외골격 바디" + "</color>";
        string description =
            showName + "\n\n" +
            "외골격으로 된 바디이다.\n" +
            "방어 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Defend")][partName].ToString()) + "\n" +
            "이동속도 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "MoveSpeed")][partName].ToString()) + "\n" +
            "공격력 : " + (int)(float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddDamPer")][partName].ToString()) * 100) + " % 증가";

        return description;
    }
}
