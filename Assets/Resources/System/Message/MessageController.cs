using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageController : MonoBehaviour//타임스케일 모든 시작 구간에 1로 맞춰놓기
{
    bool isSet;
    static bool isZone = false;
    static bool isMoveScene;
    bool isnext;
    public static int nowPage;
    public static int pageNum;
    static int viewline;

    float coroutineTime;
    float tempCorTime;

    public static string[] strwho;
    public static string[] message;

    private GameObject who;
    private GameObject text;

    private void Awake()
    {
        isSet = false;
        isMoveScene = false;
        text = transform.Find("MessageBox/Message").gameObject;
        who = transform.Find("MessageBox/Who").gameObject;
        nowPage = 0;
        viewline = 18;
        coroutineTime = 0.04f;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isZone)
        {
            Time.timeScale = 0f;
        }
        if(!isSet)
        {
            isSet = true;
            if (isZone)
            {
                Time.timeScale = 0f;
                isnext = false;
                who.GetComponent<Text>().text = null;
                who.GetComponent<Text>().text = strwho[nowPage];
                text.GetComponent<Text>().text = null;
                if (message[nowPage].Length > viewline)
                {
                    string temp = message[nowPage];
                    message[nowPage] = null;
                    for (int i = 0; i < temp.Length / viewline + 1; i++)
                    {
                        if (temp.Length - viewline * i > viewline)
                            message[nowPage] += temp.Substring(i * viewline, viewline);
                        else if (temp.Length != viewline * i)
                            message[nowPage] += temp.Substring(i * viewline, temp.Length - i * viewline);
                        if (temp.Length != viewline * i)
                            message[nowPage] += "\n";
                    }
                }
                text.GetComponent<Text>().text += message[nowPage];
                isnext = true;
            }
            else
                StartCoroutine(ShowPage());
        }
        if(Input.GetMouseButtonDown(0))
        {
            tempCorTime = 0f;
        }
        if(nowPage >=pageNum)
        {
            isZone = false;
            Time.timeScale = 1f;
            Destroy(gameObject);
            if(isMoveScene)
                SceneManager.LoadScene("Main");
        }
    }

    public static bool NonRepeatMessage(string messageName)
    {
        if ((string)DontDestLogManager.messageEvent[0][messageName] == "FALSE")
        {
            DontDestLogManager.messageEvent[0][messageName] = "TRUE";
            JsonWriter.Write(DontDestLogManager.messageEvent, PathManager.FindPath("MessageEvent"));
            return true;
        }
        else
            return false;
    }

    public static void SetMessage(string[] who, string[] message)//
    {
        if (GameObject.Find("Message(Clone)") == null)
        {
            Instantiate(Resources.Load("System/Message/Message"), Vector3.zero, Quaternion.identity);
            strwho = who;
            MessageController.message = message;
            pageNum = message.Length;
        }
    }

    IEnumerator ShowPage()
    {
        tempCorTime = coroutineTime;
        isnext = false;
        who.GetComponent<Text>().text = null;
        who.GetComponent<Text>().text = strwho[nowPage];
        text.GetComponent<Text>().text = null;
        if (message[nowPage].Length> viewline)
        {
            string temp = message[nowPage];
            message[nowPage] = null;
            for (int i = 0; i < temp.Length/ viewline + 1; i++)
            {
                if (temp.Length - viewline * i > viewline)
                    message[nowPage] += temp.Substring(i * viewline, viewline);
                else if(temp.Length != viewline * i)
                    message[nowPage] += temp.Substring(i * viewline, temp.Length - i * viewline);
                if(temp.Length != viewline * i)
                    message[nowPage] += "\n";
            }
        }
        for (int i = 0; i < message[nowPage].Length; i++)
        {
            text.GetComponent<Text>().text += message[nowPage].Substring(i, 1);
            yield return new WaitForSeconds(tempCorTime);
        }
        isnext = true;
    }

    public void NextPage()
    {
        if (isnext)
        {
            if (isZone)
            {
                nowPage++;
                if (nowPage < pageNum)
                {
                    isnext = false;
                    who.GetComponent<Text>().text = null;
                    who.GetComponent<Text>().text = strwho[nowPage];
                    text.GetComponent<Text>().text = null;
                    if (message[nowPage].Length > viewline)
                    {
                        string temp = message[nowPage];
                        message[nowPage] = null;
                        for (int i = 0; i < temp.Length / viewline + 1; i++)
                        {
                            if (temp.Length - viewline * i > viewline)
                                message[nowPage] += temp.Substring(i * viewline, viewline);
                            else if (temp.Length != viewline * i)
                                message[nowPage] += temp.Substring(i * viewline, temp.Length - i * viewline);
                            if (temp.Length != viewline * i)
                                message[nowPage] += "\n";
                        }
                    }
                    text.GetComponent<Text>().text += message[nowPage];
                    isnext = true;
                }
            }
            else
            {
                nowPage++;
                if (nowPage < pageNum)
                    StartCoroutine(ShowPage());
            }
        }
    }

    public static void BossDest()
    {
        int i = 0;
        string[] who = new string[2];
        string[] message = new string[2];
        who[i] = "System";
        message[i] = "메카닉 숙주가 제거됐습니다.";
        i++;
        who[i] = "System";
        message[i] = "다시 주변 숙주들의 힘이 강화됩니다.";
        isZone = true;
        MessageController.SetMessage(who, message);
    }

    public static void BossRespwan()
    {
        int i = 0;
        string[] who = new string[2];
        string[] message = new string[2];
        who[i] = "System";
        message[i] = "메카닉 숙주가 출현했습니다.";
        i++;
        who[i] = "System";
        message[i] = "메카닉 숙주가 주변 숙주들의 힘을 잠시 흡수합니다.";
        isZone = true;
        MessageController.SetMessage(who, message);
    }

    public static void LogHelp()
    {
        int i = 0;
        string[] who = new string[5];
        string[] message = new string[5];
        who[i] = "System";
        message[i] = "작전 기록 메뉴들을 설명해 드리겠습니다.";
        i++;
        who[i] = "System";
        message[i] = "작전 기록 메뉴는 이전에 수행하신 임무 결과들을 열람하거나,";
        i++;
        who[i] = "System";
        message[i] = "해당 임무에 사용된 메카닉을 불러오기 기능을 사용하여 임무 1, 2시작시 사용하실 수 있습니다.";
        i++;
        who[i] = "System";
        message[i] = "또한 보관, 삭제기능을 통해 해당 기록을 보관하실수도, 삭제하실수도 있습니다.";
        i++;
        who[i] = "System";
        message[i] = "기록의 최대 개수는 10개 입니다.";
        MessageController.SetMessage(who, message);
    }

    public static void MechanicHelp()
    {
        int i = 0;
        string[] who = new string[6];
        string[] message = new string[6];
        who[i] = "System";
        message[i] = "메카닉 메뉴들을 설명해 드리겠습니다.";
        i++;
        who[i] = "System";
        message[i] = "메카닉 메뉴는 보스전인 임무3에서만 사용되는 메카닉을 개조하는 공간입니다.";
        i++;
        who[i] = "System";
        message[i] = "메카닉 메뉴의 메카닉을 개조하게 되면 메인화면의 메카닉 또한 변경됩니다.";
        i++;
        who[i] = "System";
        message[i] = "파츠 상점에서 골드를 소모하여 각 파츠 부품들을 구매하실 수 있습니다.";
        i++;
        who[i] = "System";
        message[i] = "구매하신 파츠는 파츠 정비 메뉴에서 교체를 진행하거나 골드를 소모하여 강화를 진행 하실 수 있습니다.";
        i++;
        who[i] = "System";
        message[i] = "강화시 파츠의 체력, 공격력, 쿨타임, 방어력, 공격범위가 각 10%씩 상승합니다.";
        MessageController.SetMessage(who, message);
    }

    public static void MainHelp()
    {
        int i = 0;
        string[] who = new string[11];
        string[] message = new string[11];
        who[i] = "System";
        message[i] = "메인 메뉴들을 설명해 드리겠습니다.";
        i++;
        who[i] = "System";
        message[i] = "메카닉 메뉴는 보스전인 임무3에서만 사용되는 메카닉을 개조하는 공간입니다.";
        i++;
        who[i] = "System";
        message[i] = "메카닉 메뉴의 메카닉을 개조하게 되면 메인화면의 메카닉 또한 변경됩니다.";
        i++;
        who[i] = "System";
        message[i] = "하지만 임무1, 2 에서는 작전기록메뉴의 불러오기 기능을 통해서만 시작 메카닉을 변경하실 수 있습니다.";
        i++;
        who[i] = "System";
        message[i] = "작전 선택 메뉴는 게임을 시작 하는 메뉴입니다.";
        i++;
        who[i] = "System";
        message[i] = "임무1은 골드 소모 없이 진행 가능하지만 임무2부터는 시작 전 골드를 지불해야 합니다.";
        i++;
        who[i] = "System";
        message[i] = "보급상점 메뉴는 골드를 소모하여 랜덤으로 소프트웨어를 가지고 임무를 시작하실 수 있는 소프트 뽑기와";
        i++;
        who[i] = "System";
        message[i] = "광고 시청 후 레벨당 1000골드를 주는 골드 보급메뉴를 이용하실 수 있습니다.";
        i++;
        who[i] = "System";
        message[i] = "작전 기록 메뉴에서는 이전 임무에서 사용된 메카닉을 다음 임무 시작시 사용할 수 있는 불러오기 기능과";
        i++;
        who[i] = "System";
        message[i] = "이전 임무 수행 결과를 보거나 삭제, 보관을 하실 수 있습니다.";
        i++;
        who[i] = "System";
        message[i] = "임무1과 2 클리어 즉시 Rank가 3,5로 올라갑니다.";
        MessageController.SetMessage(who, message);
    }

    public static void LowGold()
    {
        int i = 0;
        string[] who = new string[1];
        string[] message = new string[1];
        who[i] = "System";
        message[i] = "골드가 부족합니다.";
        MessageController.SetMessage(who, message);
    }
    public static void LowRank5()
    {
        int i = 0;
        string[] who = new string[1];
        string[] message = new string[1];
        who[i] = "System";
        message[i] = "해당 메뉴는 Rank 5에 개방됩니다.";
        MessageController.SetMessage(who, message);
    }

    public static void LowRank3()
    {
        int i = 0;
        string[] who = new string[1];
        string[] message = new string[1];
        who[i] = "System";
        message[i] = "해당 메뉴는 Rank 3에 개방됩니다.";
        MessageController.SetMessage(who, message);
    }

    public static void Rank5()
    {
        int i = 0;
        string[] who = new string[4];
        string[] message = new string[4];
        who[i] = "Commander";
        message[i] = "전투실적이 쌓여 당신의 Rank가 5가 되었습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "최고 난이도 임무 숙주 제거를 수행하실 수 있습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "감염된지 오랜시간이 지난 숙주들은 강력하여 메카닉 파츠들을 가루로 만들어 버리기 때문에 더이상 거리에서 작은 크기인 드론을 제외한 파츠를 획득하실 수 없습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "하지만 골드를 소모해 메카닉 메뉴의 해당 임무에서 사용할 수 있는 영구적 파츠 구매, 교체 및 강화 권한을 획득했습니다.";
        MessageController.SetMessage(who, message);
    }

    public static void Rank3()
    {
        int i = 0;
        string[] who = new string[7];
        string[] message = new string[7];
        who[i] = "Commander";
        message[i] = "전투실적이 쌓여 당신의 Rank가 3이 되었습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "개방된 권한은 다음과 같습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "보급 상점의 전투 전 소프트웨어 보급(골드 소모)";
        i++;
        who[i] = "Commander";
        message[i] = "작전 기록메뉴의 이전 임무 열람, 보관 및 사용된 메카닉 파츠 불러오기(골드 소모)";
        i++;
        who[i] = "Commander";
        message[i] = "또한 당신이 파괴될 경우에 대비한 데이터 서버 보관 비용을 지불하면 감염체 사냥임무를 수행하실 수 있습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "두 번째 임무 클리어시 즉시 Rank5가 됩니다.";
        i++;
        who[i] = "Commander";
        message[i] = "이상입니다.";
        MessageController.SetMessage(who, message);
    }

    public static void Event1()
    {
        int i = 0;
        string[] who = new string[17];
        string[] message = new string[17];
        who[i] = "r¦jw^";
        message[i] = "æ«)à능 클rW어";
        i++;
        who[i] = "r¦jder";
        message[i] = "jY_ºw-능의 정상 작¢«jØ¨을 확인.";
        i++;
        who[i] = "rmmander";
        message[i] = "ß}번 복원 완료.";
        i++;
        who[i] = "Commander";
        message[i] = "331번.";
        i++;
        who[i] = "Commander";
        message[i] = "숙주 제거 임무중 치명적 파괴로 대량의 데이터들을 손실한 당신을 위해 다시금 보고합니다.";
        i++;
        who[i] = "Commander";
        message[i] = "갑작스레 생겨난 좀비들로부터 인간들을 보호하기 위해 만들어진 우리, 메카닉들은";
        i++;
        who[i] = "Commander";
        message[i] = "최종적으로 인류 수호에 실패했습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "인류가 좀비화 바이러스는 체액이 아닌 공기중으로도 전파된다는 사실을 알지 못했기 때문이죠.";
        i++;
        who[i] = "Commander";
        message[i] = "그 결과, 인류가 그 사실을 깨달았을 때는 모든 인류가 감염된 상태였고,";
        i++;
        who[i] = "Commander";
        message[i] = "결국 유기물로 구성되지 않은 우리들만이 남겨졌습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "현재, 모든인류가 좀비로 변했습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "그중에는 당신이 보호하던 인간도 포함되어 있겠죠.";
        i++;
        who[i] = "Commander";
        message[i] = "331번. 이미 실패한 우리가 할 수 있는 일은 하루빨리 그들이 안식을 취할 수 있도록 하는 것 뿐입니다.";
        i++;
        who[i] = "Commander";
        message[i] = "당신의 효율적인 데이터 복원을 위해 시스템을 순차적으로 개방하겠습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "현재 당신의 Rank는 1이며  수행가능 임무는 생존, 아직 특별한 권한은 없습니다.";
        i++;
        who[i] = "Commander";
        message[i] = "다음 시스템 개방은 Rank3입니다. 첫 번째 임무 클리어시 즉시 Rank3이 됩니다.";
        i++;
        who[i] = "Commander";
        message[i] = "임무 클리어와 많은 전투를 통해 Rank를 올리고 많은 감염자들에게 안식을 주십시오.";
        MessageController.SetMessage(who, message);
        isMoveScene = true;
    }
}
