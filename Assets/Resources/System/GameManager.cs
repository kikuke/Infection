using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject player;
    public static GameObject upgradePartsSystem;
    public static GameObject supplySoftwareSystem;

    public static int supplylevel;//플레이어의 게임속 레벨 개념. 게임밖의 플레이어레벨 즉 스테이지 레벨은 계급임

    public float supplygageper;
    public float basesupplygage;
    private float supplygage;//경험치

    public static float gameTime;
    public static float clearTime;

    public static bool isGameOver;

    //해당 인공위성 또는 스킬을 보유하고있는지 확인해서 각각의 고유 인공위성 쿨타임 접근하기 함수작동은 인공위성 관련 함수에서 하기
    // Start is called before the first frame update
    private void Awake()
    {
        OperationLogManager.zone = DontDestLogManager.zone;//존 돈디스트로이 만들기

        player = GameObject.Find("Player");

        upgradePartsSystem = GameObject.Find("System/UpgradePartsSystem");
        upgradePartsSystem.SetActive(false);
        supplySoftwareSystem = GameObject.Find("System/SupplySoftwareSystem");
        supplySoftwareSystem.SetActive(false);

        supplylevel = 1;
        basesupplygage = 20f;//임시조치
        supplygage = 0;
        gameTime = 0;

        clearTime = 60*99.99f;//기본수치. 다른데서 초기화 시키기

        isGameOver = false;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SupplyGage();
        gameTime += Time.deltaTime;
        GameManage();
    }

    private void GameManage()//킬로 점수 내는거 어떤 개체를 사냥했냐에 따라 개별 점수화 즉각적으로 더하기
    {
        if (!isGameOver)
        {
            if (player.GetComponent<PlayerController>().playerHp <= 0)
            {
                isGameOver = true;
                GameOverManager.GameOver(false);
            }
        }
        else if(isGameOver && player.GetComponent<PlayerController>().playerHp <= 0)
            GameOverManager.GameOver(true);
    }

    public void Supply(float supply)
    {
        if (supply < 0)
            supply = 0;

        supplygage += supply * (player.GetComponent<PlayerController>().addDataPer + 1);
    }

    //위성 관련 함수는 따로 스크립트 만들기

    private void SupplyGage()
    {
        supplygageper = supplygage / basesupplygage;
        while(supplygage >= basesupplygage)
        {
            supplygage -= basesupplygage;
            supplylevel++;
            basesupplygage += 2.5f * Mathf.Pow(1.3f, supplylevel);//임시조치
            //베이스 게이지 요구량 레벨당 한개씩 늘리기?
            //소프트웨어 업그레이드 함수 실행
            supplySoftwareSystem.SetActive(true);//소프트웨어 선택
            SupplySoftwareSystem.isset = true;

        }//레벨이 높은 소프트웨어가 뜰 확률은 스테이지가 높아짐에 따라 증가함. 스테이지 레벨 = 플레이어의 계급/권한 개념?//스테이지 레벨을 계급으로 바꾸기. 플레이어 레벨은 요구할 수 있는 
    }
}
