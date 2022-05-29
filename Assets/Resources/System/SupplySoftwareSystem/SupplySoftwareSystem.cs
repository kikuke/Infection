using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SupplySoftwareSystem : MonoBehaviour
{
    public static bool isset;//레벨업할때 집어넣기 setactive도
    private GameObject select;
    private GameObject softwareContent;

    private GameObject[] soft = new GameObject[3];//인덱스 이용해서 선택
    private GameObject[] button = new GameObject[3];
    private GameObject softwareInformation;
    private int sellectbuttonnum;
    private bool[] isSatellite = new bool[3];

    // Start is called before the first frame update
    private void Start()
    {
        softwareContent = transform.root.Find("PlayerInformationSystem/Softwares").gameObject;

        select = transform.Find("Select").gameObject;
        softwareInformation = select.transform.Find("SoftwareInformation").gameObject;
        for (int i = 0; i < 3; i++)
        {
            button[i] = transform.Find("Select/Soft" + i).gameObject;
            isSatellite[i] = false;
        }

        sellectbuttonnum = -1;

        select.SetActive(false);
        softwareContent.SetActive(false);
    }
    /*
    void Start()
    {
        
    }
    */

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0f;
        if (isset)
            SupplySoftware();
    }

    private void SupplySoftware()
    {
        select.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            isSatellite[i] = false;
        }

        for (int i = 0; i < 3; i++)
        {
            while(GetSoftLevel(soft[i] = RandomSoft(RndSoftLevel(GameManager.supplylevel), i), i) >= FindMaxLevel(soft[i].name));//각 파츠들 초기화했음 인스턴트화 필요함 아직은 리소스상태
            //Debug.Log(soft[i].name);
            if (isSatellite[i])
                button[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/Satellites/" + soft[i].name + "/" + soft[i].name + "Image");//나중에 스프라이트 여기서 바꾸기 밑의 showinformation에서 텍스트 받아오는것처럼 폴더에 이미지생성시켜서 사용
            else
                button[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/" + soft[i].name + "/" + soft[i].name + "Image");

        }

        isset = false;
        Time.timeScale = 0;//느리게 가게 할 수도 있음
    }

    public int FindMaxLevel(string softname)
    {
        int i;
        for (i = 0; i < GlobalManager.softwarelist.Count; i++)
            if ((string)GlobalManager.softwarelist[i]["Option"] == "MaxLevel")
                break;

        return int.Parse(GlobalManager.softwarelist[i][softname].ToString());
    }

    //버튼들 전부 세팅하기
    public void SelectSoft(int buttonnum)
    {//선택한 장비 능력치 보여주는 ui만들기 선택시 그것 사용하게 하기
        sellectbuttonnum = buttonnum;
        ShowInformation();
    }

    public void ShowInformation()//장비정보는 텍스트파일 따로 받아오기
    {
        if (sellectbuttonnum == -1)//선택하지 않았을 경우임
        {
            softwareInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Alpha Image");
            softwareInformation.transform.Find("Text").GetComponent<Text>().text = null;
        }
        else if(SoftwareManager.softwares.transform.Find(soft[sellectbuttonnum].name + "(Clone)") != null || SoftwareManager.satellites.transform.Find(soft[sellectbuttonnum].name + "(Clone)") != null)
        {
            if(isSatellite[sellectbuttonnum])
            {
                softwareInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/Satellites/" + soft[sellectbuttonnum].name + "/" + soft[sellectbuttonnum].name + "Image");
                softwareInformation.transform.Find("Text").GetComponent<Text>().text = SoftwareManager.satellites.transform.Find(soft[sellectbuttonnum].name + "(Clone)").GetComponent<Software>().Description(1);
            }
            else
            {
                softwareInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/" + soft[sellectbuttonnum].name + "/" + soft[sellectbuttonnum].name + "Image");
                softwareInformation.transform.Find("Text").GetComponent<Text>().text = SoftwareManager.softwares.transform.Find(soft[sellectbuttonnum].name + "(Clone)").GetComponent<Software>().Description(1);
            }
        }
        else//이미 있을경우 레벨 검사 해서 다른정보주기
        {
            if (isSatellite[sellectbuttonnum])
            {
                softwareInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/Satellites/" + soft[sellectbuttonnum].name + "/" + soft[sellectbuttonnum].name + "Image");
                GameObject changeObject = Resources.Load("System/Software/Satellites/" + soft[sellectbuttonnum].name + "/" + soft[sellectbuttonnum].name) as GameObject;
                softwareInformation.transform.Find("Text").GetComponent<Text>().text = changeObject.GetComponent<Software>().Description(1);
            }
            else
            {
                softwareInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/" + soft[sellectbuttonnum].name + "/" + soft[sellectbuttonnum].name + "Image");
                GameObject changeObject = Resources.Load("System/Software/" + soft[sellectbuttonnum].name + "/" + soft[sellectbuttonnum].name) as GameObject;
                softwareInformation.transform.Find("Text").GetComponent<Text>().text = changeObject.GetComponent<Software>().Description(1);
            }
           //if(GetSoftLevel(soft[sllectbuttonnum]) == 1)//이런식이어야함 0은 없을경우
        }
    }

    private int GetSoftLevel(GameObject software, int indexnum)
    {
        GameObject targetSoft;
        if(isSatellite[indexnum])
        {
            Debug.Log(software.name + "(Clone)");
            if(SoftwareManager.satellites.transform.Find(software.name +"(Clone)") != null)
            {
                targetSoft = SoftwareManager.satellites.transform.Find(software.name + "(Clone)").gameObject;
                return targetSoft.GetComponent<Software>().level;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (SoftwareManager.softwares.transform.Find(software.name + "(Clone)") != null)
            {
                targetSoft = SoftwareManager.softwares.transform.Find(software.name + "(Clone)").gameObject;
                return targetSoft.GetComponent<Software>().level;
            }
            else
            {
                return 0;
            }
        }
    }

    public void CancelButton()
    {
        sellectbuttonnum = -1;
        ShowInformation();
        select.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SelectButton()
    {
        if (sellectbuttonnum != -1)//소프트웨어 인스턴트 생성하는거 만들기
        {
            //레벨검사 함수 만들기 없을 경우에만 생성. 아닐경우 레벨 올리기
            if (GetSoftLevel(soft[sellectbuttonnum], sellectbuttonnum) == 0)
            {
                if (isSatellite[sellectbuttonnum])
                    Instantiate(soft[sellectbuttonnum], SoftwareManager.satellites.transform);
                else
                    Instantiate(soft[sellectbuttonnum], SoftwareManager.softwares.transform);

                SoftwareContent();
            }
            if (isSatellite[sellectbuttonnum])
            {
                SoftwareManager.satellites.transform.Find(soft[sellectbuttonnum].name + "(Clone)").gameObject.GetComponent<Software>().level++;
            }
            else
            {
                SoftwareManager.softwares.transform.Find(soft[sellectbuttonnum].name + "(Clone)").gameObject.GetComponent<Software>().level++;
            }
            sellectbuttonnum = -1;
            ShowInformation();
            select.SetActive(false);
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            SoftwareManager.RestartSoftware();
        }
        else
        {
            Debug.Log("소프트웨어를 선택해 주세요.");
        }
    }

    private void SoftwareContent()
    {
        if (isSatellite[sellectbuttonnum])
        {
            GameObject software = Instantiate(Resources.Load("System/UI/Software"), softwareContent.transform.Find("SoftwareScrollView/Viewport/Content")) as GameObject;
            software.name = soft[sellectbuttonnum].name;
            software.tag = "Satellite";
            software.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/Satellites/" + soft[sellectbuttonnum].name + "/" + soft[sellectbuttonnum].name + "Image");
            software.transform.Find("Text").GetComponent<Text>().text = soft[sellectbuttonnum].GetComponent<Software>().Description(1);
        }

        else
        {
            GameObject software = Instantiate(Resources.Load("System/UI/Software"), softwareContent.transform.Find("SoftwareScrollView/Viewport/Content")) as GameObject;
            software.name = soft[sellectbuttonnum].name;
            software.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/" + soft[sellectbuttonnum].name + "/" + soft[sellectbuttonnum].name + "Image");
            software.transform.Find("Text").GetComponent<Text>().text = soft[sellectbuttonnum].GetComponent<Software>().Description(1);
        }
    }

    private int RndSoftLevel(int playerlevel)//조정이 필요
    {
        float i;
        i = Random.Range(0, 1f);

        if (i > 0.93f)
            return 3;
        else if (i > 0.70f)
            return 2;
        else
            return 1;
    }

    public static string GetSoftName(string software)
    {
        int i;
        bool isSatellite = false;

        GameObject targetSoft;

        for (i = 0; i < GlobalManager.softwarelist.Count; i++)
            if ((string)GlobalManager.softwarelist[i]["Option"] == "IsSatellite")
            {
                if ((string)GlobalManager.softwarelist[i][software] == "TRUE")
                    isSatellite = true;
                break;
            }
        if (isSatellite)
        {
            targetSoft = Resources.Load("System/Software/Satellites/" + software + "/" + software) as GameObject;
            return targetSoft.GetComponent<Software>().Description(1);
        }
        else
        {
            targetSoft = Resources.Load("System/Software/" + software + "/" + software) as GameObject;
            return targetSoft.GetComponent<Software>().Description(1);
        }
    }

    public static void BaseSoft(string software)
    {
        int i;
        bool isSatellite = false;
        GameObject baseSoft;

        for (i = 0; i < GlobalManager.softwarelist.Count; i++)
            if ((string)GlobalManager.softwarelist[i]["Option"] == "IsSatellite")
            {
                if ((string)GlobalManager.softwarelist[i][software] == "TRUE")
                    isSatellite = true;
                break;
            }

        if (isSatellite)
        {
            Debug.Log(software + "###");
            baseSoft = Resources.Load("System/Software/Satellites/" + software + "/" + software) as GameObject;
            Debug.Log(SoftwareManager.satellites);
            Instantiate(baseSoft, SoftwareManager.satellites.transform);
            Debug.Log(baseSoft.name);
            SoftwareManager.satellites.transform.Find(baseSoft.name + "(Clone)").gameObject.GetComponent<Software>().level++;
        }
        else
        {
            baseSoft = Resources.Load("System/Software/" + software + "/" + software) as GameObject;
            Debug.Log(SoftwareManager.softwares);
            Instantiate(baseSoft, SoftwareManager.softwares.transform);
            SoftwareManager.softwares.transform.Find(baseSoft.name + "(Clone)").gameObject.GetComponent<Software>().level++;
        }
    }

    private GameObject RandomSoft(int Rareness, int buttonnum)//몇레벨 장비 뽑을건지
    {
        //Debug.Log("@@");
        //레벨 범위 검사하는 함수 만들기 리스트에 없는 범위면 무한루프 뜸
        List<string> listkeys = new List<string>();//나중에 하드웨어 매니저로 옮기기?

        int i;
        int maxkey = 0;
        string softname;//장비이름

        for (i = 0; i < GlobalManager.softwarelist.Count; i++)
            if ((string)GlobalManager.softwarelist[i]["Option"] == "Rareness")
                break;

        foreach (string key in GlobalManager.softwarelist[0].Keys)
        {
            listkeys.Add(key);//잘되나 체크하기
            maxkey++;
        }

        //확인용
        
        //foreach (string key in listkeys)
        //    Debug.Log(key);
          

        do
        {
            softname = listkeys[Random.Range(1, maxkey)];
        }
        while ((int)GlobalManager.softwarelist[i][softname] != Rareness);

        for(int a = 0; a < 3; a++)
        {
            //Debug.Log(isSatellite[a]);
        }

        for (i = 0; i < GlobalManager.softwarelist.Count; i++)
            if ((string)GlobalManager.softwarelist[i]["Option"] == "IsSatellite")
            {
                if ((string)GlobalManager.softwarelist[i][softname] == "TRUE")
                    isSatellite[buttonnum] = true;
                break;
            }
        //Debug.Log("S : " + softname);
        if(isSatellite[buttonnum])
        {
            Debug.Log("1 : " + Resources.Load("System/Software/Satellites/" + softname + "/" + softname));
            return Resources.Load("System/Software/Satellites/" +  softname + "/" + softname) as GameObject;
        }
        else
        {
            Debug.Log("2 : " + Resources.Load("System/Software/" + softname + "/" + softname));
            return Resources.Load("System/Software/" + softname + "/" + softname) as GameObject;
        }
    }
}
