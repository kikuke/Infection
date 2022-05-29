using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothController : Software
{
    public string name;

    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {

    }

    public override void SetUp()
    {
        base.SetUp();

        //SoftwareManager.softwareDroneNum += droneNum;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "블루투스" + "</color>";
        string description = null;

        description =
            name + "\n" +
            "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
            "드론 장착 슬롯 +" + int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "DroneNum")][softName].ToString());

        return description;
    }
}
