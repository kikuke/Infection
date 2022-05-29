using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    GameObject nowBoss;

    float genTime;
    public static float dgenTime;

    public static int level;

    public static bool isdead;

    public GameObject[] boss = new GameObject[3];
    // Start is called before the first frame update
    private void Awake()
    {
        level = 0;
        genTime = 60f * 0.5f;//모든 보스는 30초후에 젠됨
        dgenTime = 0f;
        isdead = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GenerateBoss();
    }
    //최종보스는 엄청 조그만한 점임. 피가 1/5까이기전엔 아무것도 안하고 따라만 다니다가 그만큼깎이면 엄청큰 범위 공격 계속함 최종보스는 코로나임
    void GenerateBoss()//좀더생각//isboss참고하기//boss는 에너미 상속해서 따로 동작하게 하기//화면 멈추고 대사 띄우고 플레이어 위쪽에다 생성후 다시 타임스케일 1로
    {
        if(isdead && dgenTime < genTime)
        {
            dgenTime += Time.deltaTime;
        }
        else if(isdead && dgenTime >= genTime)
        {//각 컨트롤러에서 대사들 작동시키기?
            Vector3 pos = new Vector3(GameManager.player.transform.position.x, GameManager.player.transform.position.y + 50, 0);
            MessageController.BossRespwan();
            SpawnManager.isBoss = true;
            if (level == 0)
            {
                nowBoss = Instantiate(boss[level]) as GameObject;
                nowBoss.transform.position = pos;
                isdead = false;
            }
            else
            {
                dgenTime = 0;
                Debug.Log("@@@@");
                nowBoss = Instantiate(boss[level]) as GameObject;//몬스터 사냥후 30초후 마다 젠됨//스폰위치 정하기
                nowBoss.transform.position = pos;
                isdead = false;
            }

            Handheld.Vibrate();
        }
    }
    //보스 출현시 화면 흔들리게 하는 이펙트 추가하기 보스 사망시 레벨 올려주기 및 1분후 새로운 보스 출현시키기 카운터도 보스 사망시 알려줌
}
