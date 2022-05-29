using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotGunController : Part
{
    public GameObject bullet;

    private float time;
    private int bulletNum = 5;
    private float rotateAngle = 0.1f;
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
        if(time < cooltime)
            time += Time.deltaTime;

        CooltimeSlider();
    }

    private void Attack()
    {
        GameObject closeEnemy = null;
        float distanceCompare = 100;

        Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, FindObjectOfType<ShotGunUpgradeController>() != null && FindObjectOfType<ShotGunUpgradeController>().level == 1 ? atkRange * (FindObjectOfType<ShotGunUpgradeController>().attackRange + 1) : atkRange);
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
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("isAttack");
            rotateAngle = FindObjectOfType<ShotGunUpgradeController>() != null && FindObjectOfType<ShotGunUpgradeController>().level == 2 ? rotateAngle - rotateAngle * FindObjectOfType<ShotGunUpgradeController>().degree : rotateAngle;
            bulletNum = FindObjectOfType<ShotGunUpgradeController>() != null && FindObjectOfType<ShotGunUpgradeController>().level == 3 ? bulletNum + FindObjectOfType<ShotGunUpgradeController>().bulletNum : bulletNum;
            int startBulletNum =  -(bulletNum / 2);
            for (int i = 0; i < bulletNum; i++)
            {
                Vector3 targetVector = (closeEnemy.transform.position - transform.position).normalized; 
                targetVector += new Vector3(rotateAngle * startBulletNum, rotateAngle * startBulletNum, 0);
                float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
                GameObject _bullet = SpawnManager.Instance.FindKeepObject(bullet);
                if (_bullet == null)
                    _bullet = Instantiate(bullet, GameObject.Find("Bullet Holder").transform) as GameObject;
                _bullet.transform.position = transform.position;
                _bullet.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
                _bullet.GetComponent<ShotGunBulletController>().damage = damage;
                _bullet.GetComponent<ShotGunBulletController>().targetVector = targetVector;
                startBulletNum++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "샷건" + "</color>";
        string descriptioon =
            showName + "\n" +
            "암 파츠입니다.\n" +
            "총알을 산란형으로 발사합니다.\n" +
            "공격 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString());

        return descriptioon;
    }

    private void CooltimeSlider()
    {
        coolTimeSlider.value = time / cooltime * 100;
    }
}
