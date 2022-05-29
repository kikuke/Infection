using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Software : MonoBehaviour
{
    public int level = 0;//레벨 3일경우 선택지에 나오지 않게 하기
    protected bool issetup = false;//한번만 작동하게 하기위해 넣음

    protected Dictionary<int, string> colorCode = new Dictionary<int, string>();

    public float tempdamage = 0;
    public float tempcooltime = 0;
    public float tempatkRange = 0;

    public float damage = 0;
    public float cooltime = 0;
    public float atkRange = 0;
    public float cooltimePer = 0;
    public float addDamPer = 0;
    public float addAtkRangePer = 0;
    public float addMvSpPer = 0;
    public float addDefendPer = 0;
    public float addMaxHpPer = 0;
    public int droneNum = 0;

    public virtual void SetUp()//능력치 상승을 위해 실행되는 함수//if로 레벨 검사 넣어서 능력치 올리기
    {
        string name = gameObject.name.Substring(0, gameObject.name.IndexOf("(Clone)"));
        tempdamage = float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Damage")][name].ToString());
        tempcooltime = float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "Cooltime")][name].ToString());
        tempatkRange = float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AtkRange")][name].ToString());
        cooltimePer = float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "CooltimePer")][name].ToString());
        addDamPer = float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddDamPer")][name].ToString());
        addAtkRangePer = float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddAtkRangePer")][name].ToString());
        addMvSpPer = float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddMvSpPer")][name].ToString());
        addDefendPer = float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddDefendPer")][name].ToString());
        addMaxHpPer = float.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "AddMaxHpPer")][name].ToString());
        droneNum = int.Parse(GlobalManager.softwarelist[GlobalManager.FindOption(GlobalManager.softwarelist, "DroneNum")][name].ToString());

        Stat();
    }

    public void Stat()
    {
        SoftwareManager.softwareDroneNum += droneNum;
    }

    public void AddStat()
    {
        cooltime = tempcooltime - tempcooltime * GameManager.player.GetComponent<PlayerController>().cooltimePer;
        damage = tempdamage * GameManager.player.GetComponent<PlayerController>().addDamPer;
        atkRange = tempatkRange * GameManager.player.GetComponent<PlayerController>().addAtkRangePer;
    }

    public virtual IEnumerator Active()//주기적으로 실행되는 함수
    {
        yield return new WaitForSeconds(0);
    }

    public virtual string Description(int addLevel)
    {
        colorCode.Clear();
        colorCode.Add(1, "<color=#FFFFFF>");
        colorCode.Add(2, "<color=#68D5ED>");
        colorCode.Add(3, "<color=#B36BFF>");
        //colorCode.Add(4, "<color=#FF00FF>");
        //colorCode.Add(5, "<color=#FFB400>");

        string partName = null;
        if (gameObject.name.Contains("(Clone)"))
            partName = gameObject.name.Substring(0, gameObject.name.IndexOf("(Clone)"));
        else
            partName = gameObject.name;
        return partName;
    }
}
