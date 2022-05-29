using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportingFireDroneController : Part
{
    private float radius = 5;
    private float runningTime = 0;
    private float speed = 2;
    private float time = 0;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        time = cooltime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.root.Find("Hardwares/Parts/DronePart").localScale = new Vector3(transform.root.Find("Hardwares").localScale.x, 1, 1);

        runningTime += Time.deltaTime * speed;
        Vector2 cycleVector = new Vector2(radius * Mathf.Cos(runningTime), radius * Mathf.Sin(runningTime));
        transform.localPosition = (Vector3)cycleVector;

        if(time >= cooltime)
        {
            time = 0;
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
            GameObject _bullet = SpawnManager.Instance.FindKeepObject(bullet);
            if (_bullet == null)
                _bullet = Instantiate(bullet, GameObject.Find("Bullet Holder").transform) as GameObject;
            _bullet.transform.position = transform.position;
            _bullet.GetComponent<Bullet>().target = closeEnemy;
            _bullet.GetComponent<Bullet>().damage = damage;
            _bullet.GetComponent<Bullet>().Start();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }


    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "지원 사격 드론" + "</color>";
        string description =
            showName + "\n" +
            "범위 내 적을 향해 총알을 발사한다.\n";

        return description;
    }
}
