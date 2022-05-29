using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverclockingController : Software
{
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void SetUp()
    {
        base.SetUp();

        SoftwareManager.softwarecooltimePer += cooltimePer * level;//10퍼 강화
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "오버클럭" + "</color>";
        string description = null;
        if (level + addLevel == 1)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n쿨타임 -" + (int)(float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "CooltimePer")][softName].ToString()) * (level + addLevel) * 100f) + "%\n";
        }
        if (level + addLevel == 2)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n쿨타임 -" + (int)(float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "CooltimePer")][softName].ToString()) * (level + addLevel) * 100f) + "%\n";
        }
        if (level + addLevel == 3)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n쿨타임 -" + (int)(float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "CooltimePer")][softName].ToString()) * (level + addLevel) * 100f) + "%\n";
        }

        return description;
    }
}
