using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectIncreaseController : Software
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

        SoftwareManager.softwareaddAtkRangePer += addAtkRangePer * level;
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "공격 범위" + "</color>";
        string description = null;
        description =
            name + "\n" +
            "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
            "\n공격 범위 +" + (int)(float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddAtkRangePer")][softName].ToString()) * (level + addLevel) * 100f) + "%\n";

        return description;
    }
}
