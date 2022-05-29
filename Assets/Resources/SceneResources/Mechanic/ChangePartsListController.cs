using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePartsListController : MonoBehaviour
{
    public static string parts;//불러올 부위들
    GameObject title;
    GameObject content;
    public static GameObject partsInformation;
    public static string changePart;

    public static int rfGold;

    private void Awake()
    {
        partsInformation = transform.Find("PartsInformation").gameObject;
        partsInformation.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int RfGold()//강화 요금 책정
    {
        return (rfGold = 500 * ((int)MechanicManager.partsInformation[1][changePart] + 1) * ((int)MechanicManager.partsInformation[1][changePart] + 1));
    }

    public void ReinforceButton()//체인지 버튼 따라서 하기//돈 체크해서 하기
    {
        if (MainInformationManager.Gold(-RfGold()) != -1)//골드 표시 보여주기
        {
            MechanicManager.partsInformation[1][changePart] = (int)MechanicManager.partsInformation[1][changePart] + 1;
            JsonWriter.Write(MechanicManager.partsInformation, PathManager.FindPath("PartsInformation"));
            partsInformation.transform.Find("Level").GetComponent<Text>().text = "Level : " + MechanicManager.partsInformation[1][changePart].ToString() + "\nNext : " + RfGold() + "Gold\n" + MainInformationManager.Gold(0) + "Gold";
        }
    }

    public void ChangeButton()
    {
        int i;
        for(i = 0; i < MainInformationManager.mainInformationlist.Count; i++)
        {
            if ((string)MainInformationManager.mainInformationlist[i]["Option"] == parts + "Part")
                break;
        }

        MainInformationManager.mainInformationlist[i]["Player"] = changePart;

        JsonWriter.Write(MainInformationManager.mainInformationlist, PathManager.FindPath("MainInformation"));

        ReSetting();
        partsInformation.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ReSetting()
    {
        for (int i = 0; i < content.transform.childCount; i++)
            Destroy(content.transform.GetChild(i).gameObject);
        GameObject.Find("MechanicManager").GetComponent<MechanicManager>().SetRobot();
        ChangePartsController.Parts();
        GameObject.Find("MechanicManager").GetComponent<MechanicManager>().MainInform();
    }

    public void SelfCancelButton()
    {
        for (int i = 0; i < content.transform.childCount; i++)
            Destroy(content.transform.GetChild(i).gameObject);
        gameObject.SetActive(false);
    }

    public void CancelButton()
    {
        GameObject.Find("MechanicManager").GetComponent<MechanicManager>().MainInform();
        partsInformation.SetActive(false);
    }

    public void SetContents()
    {
        title = transform.Find("Title").gameObject;
        content = transform.Find("Viewport/Content").gameObject;
        title.transform.Find("Text").GetComponent<Text>().text = parts + "Parts";
        GameObject temp;
        foreach (KeyValuePair<string, object> kv in MechanicManager.partsInformation[0])
        {
            Debug.Log(kv.Value);
            if((string)kv.Value == "Arm")
            {
                if (parts == "LeftArm" || parts == "RightArm")
                {
                    temp = Instantiate(Resources.Load("SceneResources/Mechanic/ChangePart"), content.transform) as GameObject;
                    Debug.Log(temp);
                    temp.GetComponent<ChangePartController>().SetChangePart(kv.Key);
                }
            }
            else if((string)kv.Value == parts)
            {
                temp = Instantiate(Resources.Load("SceneResources/Mechanic/ChangePart"), content.transform) as GameObject;
                temp.GetComponent<ChangePartController>().SetChangePart(kv.Key);
            }
        }
    }
}
