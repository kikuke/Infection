using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectOperationManager : MonoBehaviour
{
    GameObject rankScreen;
    GameObject goldScreen;

    GameObject content;
    GameObject operation;

    public int maxOperation = 3;//현재 최대 오퍼레이션 개수
    //고물상 보급 없는 무제한전 만들기
    float coroutineTime;

    static string[] selectOperationName;
    static int[] selectRank;
    static int[] selectGold;
    private void Awake()
    {
        Time.timeScale = 1f;
        rankScreen = transform.Find("Canvas/Rank").gameObject;
        goldScreen = transform.Find("Canvas/Gold").gameObject;
        SetScreen();

        content = transform.Find("Canvas/SelectOperation/Viewport/Content").gameObject;
        operation = Resources.Load("SceneResources/SelectOperation/Operation") as GameObject;

        selectOperationName = new string[maxOperation];
        selectRank = new int[maxOperation];
        selectGold = new int[maxOperation];

        //미션 시작할때 대사 넣기?
        SetOperationCost(1,"적응 미션 : 생존", 1, 0);
        SetOperationCost(2, "성장 미션 : 감염체 사냥", 3, 300);//단순히 사냥만 함.
        SetOperationCost(3, "최종 미션 : 숙주 사냥", 5, 700);//각 단계마다 플레이어 무기 사용하는 보스들 만들기 10단계까지 있음. 스펙게임임. 장비강화 및 구매 컨텐츠가 추가됨
        //최종미션은 각 레벨 갈때마다 스토리 넣기//성장미션의 클리어스코어는 각 단계마다 더해짐
        SetOperation();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            coroutineTime = 0f;
    }

    void SetOperation()//아직 미사용중
    {
        GameObject opTemp;
        for (int i = 0; i < maxOperation; i++)
        {
            opTemp = Instantiate(operation, content.transform) as GameObject;
            opTemp.GetComponent<SelectOperationController>().OperationName(selectOperationName[i]);//임무이름 나중에 하나로 통일해서 받아오기
            opTemp.GetComponent<SelectOperationController>().operateNum = (i + 1);
            opTemp.GetComponent<SelectOperationController>().Rank(selectRank[i]);
            opTemp.GetComponent<SelectOperationController>().Gold(selectGold[i]);
        }
    }

    void SetScreen()
    {
        coroutineTime = 0.01f;
        StartCoroutine(ShowString(rankScreen, "Rank\n" + MainInformationManager.Rank(false).ToString("N0")));
        StartCoroutine(ShowString(goldScreen, "Gold\n" + MainInformationManager.Gold(0).ToString("N0")));
        coroutineTime = 0f;
    }

    IEnumerator ShowString(GameObject screen, string text)
    {
        screen.GetComponent<Text>().text = null;

        for (int i = 0; i < text.Length; i++)
        {
            screen.GetComponent<Text>().text += text.Substring(i, 1);
            yield return new WaitForSeconds(coroutineTime);
        }
    }

    void SetOperationCost(int operationNum, string operationName, int rankCost, int goldCost)//여기서 콘텐츠 표시 설정하기//1부터 시작해야함//반드시 OperationStting에 해당 이름이 있어야함utf8로저장
    {
        operationNum--;
        selectOperationName[operationNum] = operationName;
        selectRank[operationNum] = rankCost;
        selectGold[operationNum] = -goldCost;
    }

    public static void OperateZoneButton(int operationNum)//씬이름은 반드시 OperateZone+숫자로 이루어져야함
    {
        operationNum--;
        if (MainInformationManager.Rank(false) >= selectRank[operationNum] && MainInformationManager.Gold(selectGold[operationNum]) != -1)//소모 돈 체크 여기선 0원임.
            SceneManager.LoadScene("OperateZone" + (operationNum + 1));
        else if (MainInformationManager.Rank(false) < selectRank[operationNum])
            Debug.Log("랭크가 낮습니다.");
        else
            Debug.Log("골드가 부족합니다.");
    }

    public void CancelButton()
    {
        SceneManager.LoadScene("Main");
    }
}
