using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherUpgradeController : Software
{
    public string name;
    public float coolTime = 0.1f;
    public float explosionRange = 0.5f;
    public float bulletNum = 1f;

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
        if(transform.parent == null)
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "로켓런처 업그레이드" + "</color>";
        string description = null;
        if (level + addLevel == 1)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n로켓런처 쿨타임 -" + (int)(coolTime * 100f) + "%\n";
        }
        if (level + addLevel == 2)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n로켓런처 폭발 범위 +" + (int)(explosionRange * 100f) + "%\n";
        }
        if (level + addLevel == 3)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n로켓런처 발사 수 +" + bulletNum + "\n";
        }

        return description;
    }
}
