using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketLauncherController : Part
{
    public GameObject missile;
    private int missileNum = 3;
    private float time;
    private Slider coolTimeSlider;

    // Start is called before the first frame update
    void Start() // 유도 미사일 추가
    {
        base.Start();
        transform.localPosition = Vector3.zero;

        time = cooltime;

        coolTimeSlider = transform.root.Find("PlayerUI/" + transform.parent.name.Substring(0, transform.parent.name.IndexOf("Arm")) + "Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= (FindObjectOfType<RocketLauncherUpgradeController>() != null && FindObjectOfType<RocketLauncherUpgradeController>().level == 1 ? cooltime - cooltime * FindObjectOfType<RocketLauncherUpgradeController>().coolTime : cooltime))
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

    IEnumerator CoAttack()
    {
        for(int count = 0; count < (FindObjectOfType<RocketLauncherUpgradeController>() != null && FindObjectOfType<RocketLauncherUpgradeController>().level == 3 ? missileNum + FindObjectOfType<RocketLauncherUpgradeController>().bulletNum : missileNum); count++)
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
                GameObject _missile = SpawnManager.Instance.FindKeepObject(missile);
                if (_missile == null)
                    _missile = Instantiate(missile, GameObject.Find("Bullet Holder").transform) as GameObject;
                _missile.transform.position = transform.position;
                _missile.GetComponent<RocketController>().target = closeEnemy;
                _missile.GetComponent<RocketController>().damage = damage;
                _missile.GetComponent<RocketController>().Start();
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
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "로켓런처" + "</color>";
        string descriptioon =
            showName + "\n" +
            "미사일 3발을 발사한다.\n" +
            "공격 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString());

        return descriptioon;
    }

    private void CooltimeSlider()
    {
        coolTimeSlider.value = time / cooltime * 100;
    }
}
