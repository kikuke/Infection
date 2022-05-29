using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunUpgradeController : Software
{
    public string name;
    public float degree = 0.2f;
    public float attackRange = 0.3f;
    public int bulletNum = 2;

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
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "샷건 업그레이드" + "</color>";
        string description = null;
        if (level + addLevel == 1)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n샷건 사정거리 +" + (int)(attackRange * 100f) + "\n";
        }
        if (level + addLevel == 2)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) + 
                "\n샷탄 확산률 -" + (int)(degree * 100f) + "%\n";
        }
        if (level + addLevel == 3)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) + 
                "\n탄환 +" + bulletNum + "\n";
        }

        return description;
    }
}
