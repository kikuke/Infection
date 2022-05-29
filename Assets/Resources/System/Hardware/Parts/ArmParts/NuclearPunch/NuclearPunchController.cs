using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NuclearPunchController : Part
{
    private float time;
    public GameObject nuclear;
    public GameObject nuclearPunchAddForce;
    private Vector3 targetVector;
    private Slider coolTimeSlider;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        transform.localPosition = Vector3.zero;

        time = cooltime;

        coolTimeSlider = transform.root.Find("PlayerUI/" + transform.parent.name.Substring(0, transform.parent.name.IndexOf("Arm")) + "Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= cooltime)
        {
            time = 0;
            Attack();
        }
        time += Time.deltaTime;

        if (GameManager.player.GetComponent<PlayerController>().moveVector != Vector3.zero)
            targetVector = GameManager.player.GetComponent<PlayerController>().moveVector.normalized;

        CooltimeSlider();
    }

    private void Attack()
    {
        float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        GameObject _nuclear = Instantiate(nuclear, transform.root.transform.position, Quaternion.Euler(0, 0, rotateDegree - 90), GameObject.Find("Bullet Holder").transform) as GameObject;
        _nuclear.GetComponent<NuclearController>().damage = damage;
        _nuclear.GetComponent<NuclearController>().targetVector = targetVector;
        GameObject _nuclearPunchAddForce = Instantiate(nuclearPunchAddForce, transform.root) as GameObject;
        _nuclearPunchAddForce.GetComponent<NuclearPunchAddForceController>().targetVector = targetVector;
        GameObject baseArm = Resources.Load("System/Hardware/Parts/ArmParts/BaseArm/BaseArm") as GameObject;
        GameManager.player.GetComponent<PlayerController>().MountPart(transform.parent.gameObject, Instantiate(baseArm));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "핵펀치" + "</color>";
        string descriptioon =
            showName + "\n" +
            "전방을 향해 핵펀치를 날린다\n" +
            "*한 번 사용하면 끝이다*\n" +
            "공격력 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString());

        return descriptioon;
    }

    private void CooltimeSlider()
    {
        coolTimeSlider.value = time / cooltime * 100;
    }
}
