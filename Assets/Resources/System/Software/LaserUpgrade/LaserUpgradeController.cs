using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUpgradeController : Software
{
    public string name;
    public float baseAttackTime = 0.1f;
    public float attackTime = 0.1f;

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

        attackTime = baseAttackTime * level;
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "레이저 업그레이드" + "</color>";
        string description = null;
        description =
            name + "\n" +
            "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
            "\n레이저 지속 시간 +" + (int)(attackTime * (level + addLevel) * 100f) + "%\n";

        return description;
    }
}
