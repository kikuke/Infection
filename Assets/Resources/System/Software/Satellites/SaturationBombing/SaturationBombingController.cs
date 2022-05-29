using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaturationBombingController : Software
{
    private float time = 0;
    public GameObject missile;
    private int missileNum = 3;
    private int addExplosionRange = 10;
    public string name;

    // Start is called before the first frame update
    void Start()
    {

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
            time = 0;
            for (int i = 0; i < missileNum + level; i++)
            {
                GameObject _missile = SpawnManager.Instance.FindKeepObject(missile);
                if (_missile == null)
                    _missile = Instantiate(missile, GameObject.Find("Bullet Holder").transform) as GameObject;
                _missile.GetComponent<MissileController>().Start();
                _missile.transform.position = GameManager.player.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
                _missile.transform.localScale = new Vector3(7, 7, 1);
                _missile.GetComponent<MissileController>().damage = level * damage;
                if(level == 3)
                {
                    _missile.transform.localScale = new Vector3(10, 10, 1);
                }
            }
        }
        time += Time.deltaTime;
    }

    public override string Description(int addLevel)
    {
        string softName = base.Description(0);
        name = colorCode[int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Rareness")][softName].ToString())] + "폭격 지원" + "</color>";
        string description = null;
        if (level + addLevel == 1)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "\n무작위 위치에 폭탄을 투하한다.\n";
        }
        if (level + addLevel == 2)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) +
                "폭탄 수 " + level + " 증가\n";
        }
        if (level + addLevel == 3)
        {
            description =
                name + "\n" +
                "Lv. " + ((level + addLevel).ToString() == "3" ? "Max" : (level + addLevel).ToString()) + 
                "폭탄 폭발 범위 " + addExplosionRange + "% 증가";
        }

        return description;
    }
}
