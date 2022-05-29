using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlamethrowerController : Part
{
    public GameObject fire;

    private float time;
    private float heatTime = 2f;
    private float checkHeatTime = 0;
    private bool isAttack = false;

    private Slider coolTimeSlider;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        time = cooltime;

        coolTimeSlider = transform.root.Find("PlayerUI/" + transform.parent.name.Substring(0, transform.parent.name.IndexOf("Arm")) + "Slider").GetComponent<Slider>();
    }

    private void Update()
    {
        if(time >= cooltime && isAttack == false)
        {
            Attack();
        }
        time += Time.deltaTime;

        CooltimeSlider(time);
    }

    private void Attack()
    {
        isAttack = true;
        GameObject _fire = Instantiate(fire, transform.position, Quaternion.identity, transform.Find("Fire Holder").transform);
        _fire.GetComponent<FireController>().damage = damage;
        _fire.transform.localScale *= FindObjectOfType<FlamethrowerUpgradeController>() != null ? FindObjectOfType<FlamethrowerUpgradeController>().attackRange + 1 : 1f;
        StartCoroutine(Fire(_fire));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "화염 방사기" + "</color>";
        string descriptioon =
            showName + "\n" +
            "전방으로 타오르는 불길을 뿜습니다.\n" +
            "공격 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString()) +
            "\n쿨타임 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Cooltime")][partName].ToString());

        return descriptioon;
    }

    IEnumerator Fire(GameObject _fire)
    {
        float t = cooltime;
        while (t > 0)
        {
            t -= Time.deltaTime;
            CooltimeSlider(t);
            _fire.GetComponent<FireController>().target = GameManager.player.GetComponent<PlayerController>().moveVector.normalized;
            yield return null;
        }
        Destroy(_fire);
        time = 0;
        isAttack = false;
    }

    private void CooltimeSlider(float time)
    {
        coolTimeSlider.value = time / cooltime * 100;
    }
}
