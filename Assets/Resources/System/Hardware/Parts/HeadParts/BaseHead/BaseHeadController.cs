using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHeadController : Part
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
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "기계 헤드" + "</color>";
        string description =
            showName + "\n\n" +
            "기본적인 헤드다.\n";
            //"공격 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString()) +
            //"\n방어 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Defend")][partName].ToString());

        return description;
    }
}
