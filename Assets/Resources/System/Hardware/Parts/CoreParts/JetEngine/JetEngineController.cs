using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetEngineController : Part
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
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "제트엔진" + "</color>";
        string description =
            showName + "\n" +
            "\n이동속도 " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddMvSpPer")][partName].ToString()) * 100 + "% 증가";

        return description;
    }
}
