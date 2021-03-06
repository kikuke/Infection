using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunUpgradeController : Software
{
    public string name;
    public float baseBulletNum = 1f;
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

        bulletNum = baseBulletNum * level;
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "레일건 업그레이드" + "</color>";
        string description = null;
        description =
            name + "\n" +
            "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
            "\n레일건 발사 수 +" + bulletNum + "\n";

        return description;
    }
}
