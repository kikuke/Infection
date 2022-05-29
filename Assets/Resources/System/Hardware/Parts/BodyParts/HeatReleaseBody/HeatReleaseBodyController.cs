using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatReleaseBodyController : Part
{
    private float time = 0;
    public GameObject heat;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        time = cooltime;
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= cooltime)
        {
            time = 0;
            Attack();
        }
        time += Time.deltaTime;
    }

    private void Attack()
    {
        GameObject _heat = Instantiate(heat, transform.root) as GameObject;
        _heat.GetComponent<HeatEffectController>().damage = damage;
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "열 방출 바디" + "</color>";
        string description =
            showName + "\n\n" +
            "열을 방출하여 플레이어 주변 적에게 피해를 입힌다.\n" +
            "방어력 +" + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Defend")][partName].ToString());

        return description;
    }
}
