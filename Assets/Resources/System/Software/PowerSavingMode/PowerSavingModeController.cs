﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSavingModeController : Software
{
    public string name;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void SetUp()
    {
        base.SetUp();

        SoftwareManager.softwareaddMaxHpPer += addMaxHpPer * level;
        SoftwareManager.softwareaddMvSpPer -= addMvSpPer * level;
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "절전 모드" + "</color>";
        string description = null;
        description =
            name + "\n" +
            "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
            "\n최대 체력 +" + (int)(float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddMaxHpPer")][softName].ToString()) * (level + addLevel) * 100f) + "%\n" +
            "\n이동 속도 -" + (int)(float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddMvSpPer")][softName].ToString()) * (level + addLevel) * 100f) + "%\n";

        return description;
    }
}
