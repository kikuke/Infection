using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHeadController : Part
{
    private float time;
    public GameObject laser;

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
            Attack();
        }
        time += Time.deltaTime;
    }

    private void Attack()
    {
        GameObject closeEnemy = null;
        float distanceCompare = 100;

        Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, atkRange);
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i].CompareTag("Enemy") && (others[i].gameObject.transform.position - transform.position).magnitude < distanceCompare)
            {
                distanceCompare = (others[i].gameObject.transform.position - transform.position).magnitude;
                closeEnemy = others[i].gameObject;
            }
        }

        if (closeEnemy != null)
        {
            time = 0;
            GameObject _laser = Instantiate(laser, transform.position, Quaternion.identity, GameManager.player.transform) as GameObject;
            _laser.GetComponent<Laser>().target = closeEnemy;
            _laser.GetComponent<Laser>().damage = damage;
            _laser.GetComponent<Laser>().atkRange = atkRange;
            _laser.GetComponent<Laser>().attackTime = 2f;

        }
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "레이저 헤드" + "</color>";
        string description =
            showName + "\n" +
            "가까운 적을 향해\n" +
            "레이저 빔을 쏜다.\n" +
            "공격 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString()) +
            "\n방어 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Defend")][partName].ToString());

        return description;
    }
}
