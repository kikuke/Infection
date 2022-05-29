using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UpgradePartsSystem : MonoBehaviour
{
    public static bool isset;
    private GameObject select;
    private GameObject change;
    private GameObject drone;
    private GameObject[] part = new GameObject[3];//인덱스 이용해서 선택
    private GameObject[] button = new GameObject[3];
    private GameObject showInformation;
    private int sellectbuttonnum;
    Queue<Transform> colorSlot = new Queue<Transform>();
    // Start is called before the first frame update
    private void Awake()
    {
        select = transform.Find("Select").gameObject;
        showInformation = select.transform.Find("ShowInformation").gameObject;
        change = transform.Find("Change").gameObject;
        drone = transform.Find("Drone").gameObject;

        for (int i = 0; i < 3; i++)
        {
            button[i] = transform.Find("Select/Part" + i).gameObject;
        }

        sellectbuttonnum = -1;

        select.SetActive(false);
        change.SetActive(false);
        drone.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0f;
        if (isset)
            UpgradeParts();
    }

    private void UpgradeParts()
    {
        select.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            part[i] = RandomPart(RndPartLevel(GameManager.supplylevel));//각 파츠들 초기화했음 인스턴트화 필요함 아직은 리소스상태
            Debug.Log(part[i]);
            //button[i].GetComponent<Image>().sprite = part[i].GetComponent<SpriteRenderer>().sprite;//나중에 스프라이트 여기서 바꾸기 밑의 showinformation에서 텍스트 받아오는것처럼 폴더에 이미지생성시켜서 사용
            button[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/" + part[i].tag + "Parts/" + part[i].name + "/" + part[i].name + "Image");
        }

        for (int i = 0; i < change.transform.childCount; i++)//위에꺼 이용해서 장비 보여주기
        {
            if (change.transform.GetChild(i).tag != "Untagged" && change.transform.GetChild(i).tag != "Drone")//드론은 기본이미지 만들어서 사용하기
            {
                //change.transform.GetChild(i).GetComponent<Image>().sprite = GameManager.player.transform.Find("Hardwares/Parts/" + change.transform.GetChild(i).name).GetChild(0).GetComponent<SpriteRenderer>().sprite;
                change.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/" + change.transform.GetChild(i).tag + "Parts/" + PartName(change.transform.GetChild(i).name) + "/" + PartName(change.transform.GetChild(i).name) + "Image");
            }//스프라이트 이미지 바꾸기 폴더옆에 이미지 따로 만들어서경로로 찾기
            else if(change.transform.GetChild(i).tag == "Drone")
            {
                for (int count = 0; count < 3; count++)
                {
                    if (GameManager.player.transform.Find("Hardwares/Parts/DronePart/Drone" + count).childCount != 0)
                        transform.Find("Drone/Drone" + count).GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/DroneParts/" + PartName("DronePart/Drone" + count) + "/" + PartName("DronePart/Drone" + count) + "Image");
                }
            }
        }

        //드론 표시도 따로 만들기

        isset = false;
        Time.timeScale = 0;//느리게 가게 할 수도 있음
    }

    //현재 장비 보여주는 함수 만들기//비슷하게해서 showhardwaresystem에도 적용시키기

    public void ChangePart(string parts)//장비 정보 보여주는  ui만들고 밑에거 괄호로 전부 묶어서 버튼 누르면 실행하는 걸로 바꾸기
    {
        {
            if (parts == "LeftArmPart" || parts == "RightArmPart")
            {
                if (part[sellectbuttonnum].tag == "Arm")
                {
                    GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.transform.Find("Hardwares/Parts/" + parts).gameObject, Instantiate(part[sellectbuttonnum]));
                    sellectbuttonnum = -1;
                    ShowInformation();
                    SelectColor(false);
                    change.SetActive(false);
                    gameObject.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    Debug.Log("잘못된 교체 선택입니다.");
                }
            }
            else if (parts.Substring(0, parts.IndexOf("Part")) != part[sellectbuttonnum].tag)
            {
                Debug.Log("잘못된 교체 선택입니다.");
            }
            else if (parts == "DronePart")//아직 만드는중
            {
                ShowInformation();
                change.SetActive(false);
                drone.SetActive(true);
                for (int i = 0; i < 3; i++)
                    drone.transform.Find("Drone" + i).gameObject.SetActive(true);

                for (int i = GameManager.player.GetComponent<PlayerController>().maxDroneNum; i > GameManager.player.GetComponent<PlayerController>().droneNum;)
                {
                    i -= 1;
                    drone.transform.Find("Drone" + i).gameObject.SetActive(false);
                }
            }
            else
            {
                GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.transform.Find("Hardwares/Parts/" + parts).gameObject, Instantiate(part[sellectbuttonnum]));
                sellectbuttonnum = -1;
                ShowInformation();
                SelectColor(false);
                change.SetActive(false);
                gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }     
    }
    
    public void ChangeDronePart(string parts)
    {
        GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.transform.Find("Hardwares/Parts/DronePart/" + parts).gameObject, Instantiate(part[sellectbuttonnum]));
        sellectbuttonnum = -1;
        ShowInformation();
        SelectColor(false);
        drone.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CancelDronePart()
    {
        drone.SetActive(false);
        change.SetActive(true);
    }

    public void SelectPart(int buttonnum)
    {//선택한 장비 능력치 보여주는 ui만들기 선택시 그것 사용하게 하기
        sellectbuttonnum = buttonnum;
        ShowInformation();
    }

    public void ShowInformation()//장비정보는 텍스트파일 따로 받아오기
    {
        if(sellectbuttonnum == -1)//선택하지 않았을 경우임
        {
            showInformation.transform.Find("PlayerInformation/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Alpha Image");
            showInformation.transform.Find("PlayerInformation/Text").GetComponent<Text>().text = null;
            showInformation.transform.Find("PartsInformation/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Alpha Image");
            showInformation.transform.Find("PartsInformation/Text").GetComponent<Text>().text = null;
        }
        else
        {
            if (part[sellectbuttonnum].tag == "Arm")//왼팔만 나옴
            {
                showInformation.transform.Find("PlayerInformation/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/ArmParts/" + PartName("LeftArmPart") + "/" + PartName("LeftArmPart") + "Image");
                showInformation.transform.Find("PlayerInformation/Text").GetComponent<Text>().text = GameManager.player.transform.Find("Hardwares/Parts/LeftArmPart").GetChild(0).GetComponent<Part>().Description();
            }
            else if(part[sellectbuttonnum].tag == "Drone")
            {
                int i = 0;
                while (i <= 2)
                {
                    if (GameManager.player.transform.Find("Hardwares/Parts/" + part[sellectbuttonnum].tag + "Part/Drone" + i).childCount == 0)
                        i++;
                    else
                        break;
                }

                if (i != 3)
                {
                    showInformation.transform.Find("PlayerInformation/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/" + part[sellectbuttonnum].tag + "Parts/" + PartName(part[sellectbuttonnum].tag + "Part/Drone" + i) + "/" + PartName(part[sellectbuttonnum].tag + "Part/Drone" + i) + "Image");
                    showInformation.transform.Find("PlayerInformation/Text").GetComponent<Text>().text = GameManager.player.transform.Find("Hardwares/Parts/" + part[sellectbuttonnum].tag + "Part/Drone" + i).GetChild(0).GetComponent<Part>().Description();
                }
            }
            else
            {
                showInformation.transform.Find("PlayerInformation/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/" + part[sellectbuttonnum].tag + "Parts/" + PartName(part[sellectbuttonnum].tag + "Part") + "/" + PartName(part[sellectbuttonnum].tag + "Part") + "Image");
                showInformation.transform.Find("PlayerInformation/Text").GetComponent<Text>().text = GameManager.player.transform.Find("Hardwares/Parts/" + part[sellectbuttonnum].tag + "Part").GetChild(0).GetComponent<Part>().Description();
            }
            GameObject changeObject = Resources.Load("System/Hardware/Parts/" + part[sellectbuttonnum].tag + "Parts/" + part[sellectbuttonnum].name + "/" + part[sellectbuttonnum].name) as GameObject;
            showInformation.transform.Find("PartsInformation/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/" + part[sellectbuttonnum].tag + "Parts/" + part[sellectbuttonnum].name + "/" + part[sellectbuttonnum].name + "Image");
            showInformation.transform.Find("PartsInformation/Text").GetComponent<Text>().text = changeObject.GetComponent<Part>().Description();
        }
    }

    private string PartName(string _partType)
    {
        string partName = GameManager.player.transform.Find("Hardwares/Parts/" + _partType).GetChild(0).name;
        partName = partName.Substring(0, partName.IndexOf("(Clone)"));

        return partName;
    }

    public void CancelButton()
    {
        sellectbuttonnum = -1;
        ShowInformation();
        SelectColor(false);
        select.SetActive(false);
        change.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SelectButton()
    {
        if (sellectbuttonnum != -1)
        {
            select.SetActive(false);
            change.SetActive(true);
            SelectColor(true);
        }
        else
        {
            Debug.Log("장비를 선택해 주세요.");
        }
    }

    private void SelectColor(bool setColor)
    {
        if (setColor)
        {
            if (part[sellectbuttonnum].tag == "Arm")
            {
                change.transform.Find("LeftArmPart/Image").GetComponent<Image>().color = new Color(0 / 255f, 170 / 255f, 255 / 255f);
                change.transform.Find("RightArmPart/Image").GetComponent<Image>().color = new Color(0 / 255f, 170 / 255f, 255 / 255f);
                colorSlot.Enqueue(change.transform.Find("LeftArmPart/Image"));
                colorSlot.Enqueue(change.transform.Find("RightArmPart/Image"));
            }
            else
            {
                change.transform.Find(part[sellectbuttonnum].tag + "Part/Image").GetComponent<Image>().color = new Color(0 / 255f, 170 / 255f, 255 / 255f);
                colorSlot.Enqueue(change.transform.Find(part[sellectbuttonnum].tag + "Part/Image"));
            }
        }
        else
        {
            int queueSize = colorSlot.Count;
            for(int i = 0; i < queueSize; i++)
            {
                colorSlot.Dequeue().GetComponent<Image>().color = Color.white;
            }
            colorSlot.Clear();
        }
    }

    private int RndPartLevel(int playerlevel)//조정이 필요
    {
        float i;
        i = Random.Range(0,1f);

        //return 0;//임시 실험용

        //나중에 어느정도 만들어지면 밑에거 쓰기
        if (i > 0.95f)
            return 5;
        else if (i > 0.85f)
            return 4;
        else if (i > 0.65f)
            return 3;
        else if (i > 0.35f)
            return 2;
        else
            return 1;
    }

    private GameObject RandomPart(int level)//몇레벨 장비 뽑을건지
    {
        //레벨 범위 검사하는 함수 만들기 리스트에 없는 범위면 무한루프 뜸
        List<string> listkeys = new List<string>();//나중에 하드웨어 매니저로 옮기기?
        
        int i;

        int maxkey = 0;
        string partname;//장비이름
        string parts = null;//어느부위 장비인지 체크

        for (i = 0; i < GlobalManager.hardwarelist.Count; i++)
            if ((string)GlobalManager.hardwarelist[i]["Option"] == "Level")
                break;

        if (OperationLogManager.zone != "최종 미션 : 숙주 사냥")
        {
            foreach (string key in GlobalManager.hardwarelist[0].Keys)
            {
                listkeys.Add(key);//잘되나 체크하기
                maxkey++;
            }
        }
        else
        {
            foreach (KeyValuePair<string, object> kv in GlobalManager.hardwarelist[0])
            {
                if ((string)kv.Value == "Drone")
                {
                    listkeys.Add(kv.Key);//잘되나 체크하기
                    maxkey++;
                }
            }
        }
        
        do
        {
            partname = listkeys[Random.Range(1, maxkey)];
            if (OperationLogManager.zone == "최종 미션 : 숙주 사냥")
                break;
        }
        while ((int)GlobalManager.hardwarelist[i][partname] != level);

        for (i = 0; i < GlobalManager.hardwarelist.Count; i++)
            if ((string)GlobalManager.hardwarelist[i]["Option"] == "Part")
                break;

        parts = (string)GlobalManager.hardwarelist[i][partname];

        return Resources.Load("System/Hardware/Parts/" + parts + "Parts/" + partname + "/" + partname) as GameObject;//게임오브젝트 경로 찾는거 다른데서 필요하면 응용하기
    }

    public void InformationButton(string name)
    {
        transform.Find(name).gameObject.SetActive(!transform.Find(name).gameObject.activeSelf);
    }
}
