using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    string name;
    public string showName;

    protected Dictionary<int, string> colorCode = new Dictionary<int, string>();

    //플레이어에게 영향을 주는 수치들만 넣기. 각 게임오브젝트에서 필요한 변수는
    //스크립트에서 새로 만들기.
    public float tempdamage;
    public float tempcooltime;
    public float tempatkRange;

    public int level;
    public float moveSpeed;//각 능력치들 추가시키기
    public float maxHp;
    public float defend;
    public float damage;
    public float cooltime;
    public float atkRange;
    public float cooltimePer;
    public float addDamPer;
    public float addAtkSpPer;
    public float addAtkRangePer;
    public float addMvSpPer;
    public float addDefendPer;
    public float addMaxHpPer;
    public int droneNum;

    // Start is called before the first frame update
    protected void Awake()
    {
        SetStat();
    }


    protected void Start()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = transform.parent.GetComponent<SpriteRenderer>().sortingLayerName;
        transform.localScale = GameObject.Find("Player").transform.localScale;

        if (GameManager.player.GetComponent<PlayerController>().isLeft == true)
        {
            if (transform.parent.name == "LeftArmPart")
            {
                transform.localPosition = Vector3.right * -1.37f;
                GetComponent<SpriteRenderer>().sortingLayerName = "RightArm";
            }
            if (transform.parent.name == "RightArmPart")
            {
                transform.localPosition = Vector3.right * 1.37f;
                GetComponent<SpriteRenderer>().sortingLayerName = "LeftArm";
            }
        }
        if (GameManager.player.GetComponent<PlayerController>().isLeft == false)
        {
            if (transform.parent.name == "LeftArmPart")
            {
                transform.localPosition = Vector3.right * 0;
                GetComponent<SpriteRenderer>().sortingLayerName = "LeftArm";
            }
            if (transform.parent.name == "RightArmPart")
            {
                transform.localPosition = Vector3.right * 0;
                GetComponent<SpriteRenderer>().sortingLayerName = "RightArm";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetStat()
    {
        name = gameObject.name.Substring(0, gameObject.name.IndexOf("(Clone)"));
        Debug.Log(name);
        float addStat = 1f;
        if (OperationLogManager.zone == "최종 미션 : 숙주 사냥" && (string)GlobalManager.hardwarelist[0][name] != "Drone")
        {
            addStat += 0.1f * (int)MainInformationManager.partsInformationlist[1][name];
            Debug.Log(name + "강화 레벨 : " + (int)MainInformationManager.partsInformationlist[1][name] + "추가 적용 능력치 : +" + (addStat - 1) * 100 + "%");
        }
        level = int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][name].ToString());
        moveSpeed = float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "MoveSpeed")][name].ToString());
        maxHp = addStat* float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "MaxHp")][name].ToString());
        defend = addStat* float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Defend")][name].ToString());
        tempdamage = addStat* float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][name].ToString());
        tempcooltime = float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Cooltime")][name].ToString());
        tempatkRange = addStat*float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AtkRange")][name].ToString());
        cooltimePer = float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "CooltimePer")][name].ToString());
        addDamPer = float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddDamPer")][name].ToString());
        addAtkSpPer = float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddAtkSpPer")][name].ToString());
        addAtkRangePer = float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddAtkRangePer")][name].ToString());
        addMvSpPer = float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddMvSpPer")][name].ToString());
        addDefendPer = float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddDefendPer")][name].ToString());
        addMaxHpPer = float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddMaxHpPer")][name].ToString());
        droneNum = int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "DroneNum")][name].ToString());
    }
    /*
        moveSpeed
        maxHp;
        defend;
        cooltimePer;
        addDamPer;
        addAtkSpPer;
        addAtkRangePer;
     * */

    public void Stat()
    {
        HardwareManager.hardwareMoveSpeed += moveSpeed;//각 능력치 더하기
        HardwareManager.hardwaremaxHp += maxHp;
        HardwareManager.hardwaredefend += defend;
        HardwareManager.hardwareDamage += tempdamage;
        HardwareManager.hardwarecooltime += tempcooltime;
        HardwareManager.hardwareAtkRange += tempatkRange;
        HardwareManager.hardwarecooltimePer += cooltimePer; //합연산
        HardwareManager.hardwareaddDamPer += addDamPer; //합
        HardwareManager.hardwareaddAtkSpPer += addAtkSpPer; //합
        HardwareManager.hardwareaddAtkRangePer += addAtkRangePer; //합
        HardwareManager.hardwareaddMvSpPer += addMvSpPer; //합
        HardwareManager.hardwareaddDefendPer += addDefendPer; //합
        HardwareManager.hardwareaddMaxHpPer += addMaxHpPer;
        HardwareManager.hardwareDroneNum += droneNum;
    }

    public void AddStat()
    {
        float addStat = 0;
        if (OperationLogManager.zone == "최종 미션 : 숙주 사냥" && (string)GlobalManager.hardwarelist[0][name] != "Drone")
            addStat = 0.05f * (int)MainInformationManager.partsInformationlist[1][name];
        if (GameManager.player.GetComponent<PlayerController>().cooltimePer + addStat < 0.7f)
            cooltime = tempcooltime - tempcooltime * (GameManager.player.GetComponent<PlayerController>().cooltimePer + addStat);
        else
            cooltime = tempcooltime - tempcooltime * 0.7f;
        damage = tempdamage * GameManager.player.GetComponent<PlayerController>().addDamPer;
        atkRange = tempatkRange * GameManager.player.GetComponent<PlayerController>().addAtkRangePer;
    }

    public virtual string Description()
    {
        colorCode.Clear();
        colorCode.Add(1, "<color=#FFFFFF>");
        colorCode.Add(2, "<color=#68D5ED>");
        colorCode.Add(3, "<color=#B36BFF>");
        colorCode.Add(4, "<color=#FF00FF>");
        colorCode.Add(5, "<color=#FFB400>");

        string partName = null;
        if (gameObject.name.Contains("(Clone)"))
            partName = gameObject.name.Substring(0, gameObject.name.IndexOf("(Clone)"));
        else
            partName = gameObject.name;
        return partName;
    }
}
