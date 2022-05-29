using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : Enemy // 이거만들고 대사 스크립트 짜기타임스케일 0으로 해서 끝나면 되돌리는 것으로
{
    GameObject player;
    private SpriteRenderer spriteRenderer;
    public float atkRange;
    public float cooltime;//
    private float dcooltime;

    Vector3 target;
    float targetTime;
    float targetDTime;

    //무기전용 변수
    public GameObject missile;
    private int missileNum = 3;

    //보스 전용 함수들
    private void Awake()
    {
        targetDTime = targetTime = 1.2f;
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        //무기
        //cooltime = 5f;
        //atkRange = 20f;//
        transform.localPosition = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()//대사
    {
        hp = maxHp;
        dcooltime = cooltime;
    }

    // Update is called once per frame
    new void Update()
    {
        HpUI();

        if (targetDTime >= targetTime)
            target = (player.transform.position - transform.position).normalized;
        else
            targetDTime += Time.deltaTime;

        transform.position += target * moveSpeed * Time.deltaTime;

        if (target.x > 0)
            spriteRenderer.flipX = false;
        if (target.x < 0)
            spriteRenderer.flipX = true;

        //무기
        if (dcooltime >= cooltime)
        {
            dcooltime = 0;
            Attack();
        }
        if (dcooltime < cooltime)
            dcooltime += Time.deltaTime;

        HeatUI();
    }
    protected override void KillScore()//실상 죽을때 실행시키는 함수로 사용하기
    {
        OperationLogManager.KillScore(100000);
    }

    //밑에서부터 무기함수들
    private void Attack()
    {
        StartCoroutine(CoAttack());
    }

    IEnumerator CoAttack()
    {
        for (int count = 0; count < missileNum; count++)
        {
            GameObject player = null;
            float distanceCompare = 100;

            Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, atkRange);
            for (int i = 0; i < others.Length; i++)
            {
                if (others[i].CompareTag("Player") && (others[i].gameObject.transform.position - transform.position).magnitude < distanceCompare)
                {
                    distanceCompare = (others[i].gameObject.transform.position - transform.position).magnitude;
                    player = others[i].gameObject;
                }
            }

            if (player != null)
            {
                GameObject _missile = SpawnManager.Instance.FindKeepObject(missile);
                if (_missile == null)
                    _missile = Instantiate(missile, GameObject.Find("Bullet Holder").transform) as GameObject;
                _missile.transform.position = transform.position;
                _missile.GetComponent<RocketController>().boss = gameObject;
                _missile.GetComponent<RocketController>().target = player;
                _missile.GetComponent<RocketController>().damage = damage;
                _missile.GetComponent<RocketController>().transform.localScale += Vector3.zero;
                _missile.GetComponent<RocketController>().Start();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    private void HeatUI()
    {
        transform.Find("Heat/HeatBar").transform.localScale = new Vector3(1, (float)dcooltime / (float)cooltime, 1);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }
}