using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineGunController : Part
{
    public GameObject bullet;

    private float time;
    private int count = 0;
    private int maxcount = 30;
    private float HeatTime = 1f;
    private float checkHeatTime = 0;
    private bool isHeat = false;
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
        if (isHeat == false && time >= cooltime && count < (FindObjectOfType<MachineGunUpgradeController>() != null ? maxcount + FindObjectOfType<MachineGunUpgradeController>().bulletNum : maxcount))
        {
            Debug.Log(FindObjectOfType<MachineGunUpgradeController>() != null ? maxcount + FindObjectOfType<MachineGunUpgradeController>().bulletNum : maxcount);
            time = 0;
            Attack();
        }

        time += Time.deltaTime;
        checkHeatTime += Time.deltaTime;

        if (checkHeatTime >= HeatTime || count >= (FindObjectOfType<MachineGunUpgradeController>() != null ? maxcount + FindObjectOfType<MachineGunUpgradeController>().bulletNum : maxcount))
            StartCoroutine(Heat());

        CooltimeSlider();
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
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("isAttack");
            count += 1;
            checkHeatTime = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "머신건" + "</color>";
        string descriptioon =
            showName + "\n" +
            "암 파츠입니다.\n" +
            "빠르게 총알을 발사합니다.\n" +
            "공격 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString());

        return descriptioon;
    }

    private IEnumerator Heat()
    {
        isHeat = true;

        while (count > 0 && checkHeatTime >= HeatTime)
        {
            count--;
            yield return new WaitForSeconds(0.5f);
        }

        isHeat = false;
    }

    private void CooltimeSlider()
    {
        coolTimeSlider.value = (float)count / (float)(FindObjectOfType<MachineGunUpgradeController>() != null ? maxcount + FindObjectOfType<MachineGunUpgradeController>().bulletNum : maxcount) * 100;
    }
}
