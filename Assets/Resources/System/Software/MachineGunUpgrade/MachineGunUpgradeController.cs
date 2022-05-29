using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunUpgradeController : Software
{
    public string name;
    public float baseBulletNum = 10f;
    public float bulletNum = 10f;

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

        if (level == 1)
            bulletNum = baseBulletNum;
        else if (level == 2)
            bulletNum = baseBulletNum + 5;
        else if (level == 3)
            bulletNum = baseBulletNum + 5 + 5;
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "머신건 업그레이드" + "</color>";
        string description = null;
        description =
            name + "\n" +
            "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
            "\n머신건 탄창 +" + bulletNum + "\n";

        return description;
    }
}
