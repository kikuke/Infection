using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRecoveryController : Software
{
    private float time;
    private float restoreHp = 0.01f;
    private float addRestoreHp = 0.005f;
    public string name;

    // Start is called before the first frame update
    private void Awake()
    {
        restoreHp = 0.01f;
    }
    void Start()
    {
        
    }

    public override void SetUp()
    {
        base.SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= cooltime)
        {
            time = 0;
            transform.root.GetComponent<PlayerController>().Restore((level >= 2 ? restoreHp + addRestoreHp : restoreHp));
        }
        time += Time.deltaTime;

        if (level == 2)
            addRestoreHp = 0.005f;
        if (level == 3)
            addRestoreHp = 0.01f;
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "자가 치료" + "</color>";
        string description = null;
        if (level + addLevel == 1)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n쿨타임 : " + float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Cooltime")][softName].ToString()) + "초\n" +
                "체력을 " + restoreHp + "씩 회복한다\n";
        }
        if (level + addLevel == 2)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n쿨타임 : " + float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Cooltime")][softName].ToString()) + "초\n" +
                "체력을 " + (restoreHp + addRestoreHp) + "씩 회복한다\n";
        }
        if (level + addLevel == 3)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n쿨타임 : " + float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Cooltime")][softName].ToString()) + "초\n" +
                "체력을 " + (restoreHp + addRestoreHp + addRestoreHp) + "씩 회복한다\n";
        }

        return description;
    }
}
