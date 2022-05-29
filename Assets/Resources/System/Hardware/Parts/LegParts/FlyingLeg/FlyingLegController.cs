using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingLegController : Part
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        GameManager.player.GetComponent<Collider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "플라잉 레그" + "</color>";
        string description =
            showName + "\n\n" +
            "적을 통과할 수 있다.\n단 대미지는 받는다.\n";
        //"이동속도 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "MoveSpeed")][partName].ToString());

        return description;
    }

    private void OnDestroy()
    {
        GameManager.player.GetComponent<Collider2D>().isTrigger = false;
    }
}
