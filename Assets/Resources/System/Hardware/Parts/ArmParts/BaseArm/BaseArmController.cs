using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseArmController : Part
{
    public GameObject bullet;

    private float time;
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
        if(time >= cooltime)
        {
            Attack();
        }
        if(time < cooltime)
            time += Time.deltaTime;

        CooltimeSlider();
    }

    private void Attack()
    {
        GameObject closeEnemy = null;
        float distanceCompare = 100;

        Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, atkRange);
        for(int i = 0; i < others.Length; i++)
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
            GameObject _bullet = SpawnManager.Instance.FindKeepObject(bullet);
            if(_bullet == null)
                _bullet = Instantiate(bullet, GameObject.Find("Bullet Holder").transform) as GameObject;
            _bullet.transform.position = transform.position;
            _bullet.GetComponent<Bullet>().target = closeEnemy;
            _bullet.GetComponent<Bullet>().damage = damage;
            _bullet.GetComponent<Bullet>().Start();
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("isAttack");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "기계 팔" + "</color>";
        string descriptioon =
            showName + "\n\n" +
            "기본적인 팔이다.\n";
            //"공격 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString());

        return descriptioon;
    }

    private void CooltimeSlider()
    {
        coolTimeSlider.value = time / cooltime * 100;
    }
}
