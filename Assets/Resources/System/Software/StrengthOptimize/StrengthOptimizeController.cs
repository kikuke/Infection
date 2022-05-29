using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthOptimizeController : Software
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

        SoftwareManager.softwareaddDamPer += addDamPer * level;//10퍼 강화
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "힘 증폭" + "</color>";
        string description = null;
        description =
            name + "\n" +
            "Lv. " + ((level + addLevel).ToString() == "10" ? "Max" : (level + addLevel).ToString()) +
            "\n피해량이" + (int)(float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddDamPer")][softName].ToString()) * (level + addLevel) * 100f) + "%증가한다\n";

        return description;
    }
}
