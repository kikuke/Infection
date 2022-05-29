using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDataController : Software
{
    private float addDataPer = 0.05f;
    public string name;

    // Start is called before the first frame update
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
        if (level == 1)
            GameManager.player.GetComponent<PlayerController>().addDataPer = addDataPer;
        if (level == 2)
            GameManager.player.GetComponent<PlayerController>().addDataPer = addDataPer + addDataPer;
        if (level == 3)
            GameManager.player.GetComponent<PlayerController>().addDataPer = addDataPer + addDataPer + addDataPer + addDataPer;
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "읽기 속도 증가" + "</color>";
        string description = null;
        if (level + addLevel == 1)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n데이터 획득량 " + (int)(addDataPer * 100) + "% 증가\n";
        }
        if (level + addLevel == 2)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n데이터 획득량 " + (int)((addDataPer + addDataPer) * 100) + "% 증가\n";
        }
        if (level + addLevel == 3)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n데이터 획득량 " + (int)((addDataPer + addDataPer + addDataPer + addDataPer) * 100) + "% 증가\n";
        }

        return description;
    }
}
