using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpetualMotionMachineController : Part
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        GameManager.player.GetComponent<PlayerController>().playerHp *= addMaxHpPer + 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "무한동력" + "</color>";
        string description =
            showName + "\n\n" +
            "모든 능력치 " + (int)(float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "CooltimePer")][partName].ToString()) * 100) + "% 증가";

        return description;
    }
}
