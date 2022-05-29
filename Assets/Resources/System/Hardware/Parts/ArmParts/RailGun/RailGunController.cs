using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailGunController : Part
{
    public GameObject bullet;

    private float time;
    private int bulletNum = 1;
    private Slider coolTimeSlider;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        time = cooltime;

        coolTimeSlider = transform.root.Find("PlayerUI/" + transform.parent.name.Substring(0, transform.parent.name.IndexOf("Arm")) + "Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= cooltime)
        {
            Attack();
        }
        if (time < cooltime)
            time += Time.deltaTime;

        CooltimeSlider();
    }

    private void Attack()
    {
        StartCoroutine(CoAttack());
    }

    private IEnumerator CoAttack()
    {
        for (int count = 0; count < (FindObjectOfType<RailGunBulletController>() != null ? bulletNum + FindObjectOfType<RailGunUpgradeController>().bulletNum : bulletNum); count++)
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
                _bullet.GetComponent<RailGunBulletController>().target = closeEnemy;
                _bullet.GetComponent<RailGunBulletController>().damage = damage;
                _bullet.GetComponent<RailGunBulletController>().Start();
                time = 0;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "레일건" + "</color>";
        string descriptioon =
            showName + "\n" +
            "빠른속도로 적을 관통하는 탄환을 발사합니다.\n" +
            "공격력 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString());

        return descriptioon;
    }

    private void CooltimeSlider()
    {
        coolTimeSlider.value = time / cooltime * 100;
    }
}
