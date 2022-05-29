using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthBodyController : Part
{
    private float time = 0;
    private float stealthCoolTime = 3;
    public GameObject stealth;

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
            StartCoroutine(Stealth());
        }
        time += Time.deltaTime;
    }

    private IEnumerator Stealth()
    {
        GameManager.player.GetComponent<PlayerController>().invincibility = true;
        GameObject _stealth = Instantiate(stealth, transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(stealthCoolTime);
        time = 0;

        GameManager.player.GetComponent<PlayerController>().invincibility = false;
        Destroy(_stealth);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "스텔스 바디" + "</color>";
        string description =
            showName + "\n\n" +
            "스텔스 상태일 때는 적에게 공격을 받지 않는다.\n" +
            "방어력 +" + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Defend")][partName].ToString());

        return description;
    }
}
