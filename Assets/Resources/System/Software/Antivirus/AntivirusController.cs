using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntivirusController : Software
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
        SoftwareManager.softwareaddDefendPer += addDefendPer * level;//10퍼 강화
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "안티 바이어스" + "</color>";
        string description = null;
        description =
            name + "\n" +
            "Lv. " + ((level + addLevel).ToString() == "5" ? "Max" : (level + addLevel).ToString()) +
            "\n방어력 +" + (int)(float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddDefendPer")][softName].ToString()) * (level + addLevel) * 100f) + "%\n";
        return description;
    }
}
