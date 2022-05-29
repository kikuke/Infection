using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberUpgradeController : Software
{
    public string name;
    public float baseAttackRange = 0.1f;
    public float attackRange = 0.1f;

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

        attackRange = baseAttackRange * level;
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "광선검 업그레이드" + "</color>";
        string description = null;
        description =
            name + "\n" +
            "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
            "\n광선검 길이 +" + (int)(attackRange * (level + addLevel) * 100f) + "%\n";

        return description;
    }
}
