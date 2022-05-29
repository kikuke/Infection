using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackController : Software
{
    private float time = 0;
    public GameObject laser;
    public float attackTime;
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = 1f;
    }

    public override void SetUp()
    {
        base.SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= cooltime)
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
            GameObject _laser = Instantiate(laser, transform) as GameObject;
            _laser.GetComponent<SatelliteLaserController>().target = closeEnemy;
            _laser.GetComponent<SatelliteLaserController>().damage = damage;
            _laser.GetComponent<SatelliteLaserController>().atkRange = atkRange;
            _laser.GetComponent<SatelliteLaserController>().attackTime = attackTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "위성 레이저" + "</color>";
        string description = null;
        if (level + addLevel == 1)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n적에게 레이저를 쏜다.\n";
        }
        if (level + addLevel == 2)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n적에게 레이저를 쏜다.\n" +
                "공격 시간 " + attackTime + "% 증가";
        }
        if (level + addLevel == 3)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n적에게 레이저를 쏜다.\n" +
                "공격 시간 " + attackTime + "% 증가";
        }

        return description;
    }
}
