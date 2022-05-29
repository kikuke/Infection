using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Controller : Enemy // 이거만들고 대사 스크립트 짜기타임스케일 0으로 해서 끝나면 되돌리는 것으로
{
    GameObject player;
    private SpriteRenderer spriteRenderer;
    public float shotatkRange;
    public float shotcooltime;//
    private float dshotcooltime;

    //무기전용 변수

    public GameObject bullet;
    private int bulletNum = 5;
    private float rotateAngle = 0.1f;

    //미사일
    public float mdamage;
    public float atkRange;
    public float cooltime;//
    private float dcooltime;
    public GameObject missile;
    private int missileNum = 3;

    //스텔스
    private float stime = 0;
    public float stealthCoolTime;
    private Collider2D collider2D;
    private Rigidbody2D rigidbody2D;
    public GameObject stealth;
    float scooltime = 10f;

    //보스 전용 함수들
    private void Awake()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        //무기
        //shotcooltime = 1f;
        //shotatkRange = 7f;//
        //미사일
        //mdamage = 9f;
        //cooltime = 5f;
        //atkRange = 20f;//
        transform.localPosition = Vector3.zero;
        //스텔스
        //stealthCoolTime = 3;
        collider2D = gameObject.GetComponent<Collider2D>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()//대사
    {
        hp = maxHp;
        dshotcooltime = shotcooltime;
        dcooltime = cooltime;
        stime = scooltime;
    }

    // Update is called once per frame
    new void Update()
    {
        HpUI();
        Vector3 target = (player.transform.position - transform.position).normalized;
        transform.position += target * moveSpeed * Time.deltaTime;

        if (target.x > 0)
            spriteRenderer.flipX = false;
        if (target.x < 0)
            spriteRenderer.flipX = true;

        //무기
        if (dshotcooltime >= shotcooltime)
        {
            ShotAttack();
        }
        if (dshotcooltime < shotcooltime)
            dshotcooltime += Time.deltaTime;
        ShotHeat1UI();
        //미사일
        if (dcooltime >= cooltime)
        {
            Attack();
        }
        if (dcooltime < cooltime)
            dcooltime += Time.deltaTime;

        HeatUI();
        //스텔스
        if (stime >= scooltime)
        {
            stime = 0;
            StartCoroutine(Stealth());
        }
        stime += Time.deltaTime;
    }
    protected override void KillScore()//실상 죽을때 실행시키는 함수로 사용하기
    {
        OperationLogManager.KillScore(1000000);
    }
    //스텔스
    private IEnumerator Stealth()
    {
        collider2D.enabled = false;
        GameObject _stealth = Instantiate(stealth, transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(stealthCoolTime);
        stime = 0;

        collider2D.enabled = true;
        Destroy(_stealth);
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
                dcooltime = 0;
                GameObject _missile = SpawnManager.Instance.FindKeepObject(missile);
                if (_missile == null)
                    _missile = Instantiate(missile, GameObject.Find("Bullet Holder").transform) as GameObject;
                _missile.transform.position = transform.position;
                _missile.GetComponent<RocketController>().boss = gameObject;
                _missile.GetComponent<RocketController>().target = player;
                _missile.GetComponent<RocketController>().damage = mdamage;
                _missile.GetComponent<RocketController>().transform.localScale += Vector3.zero;
                _missile.GetComponent<RocketController>().Start();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void ShotAttack()
    {
        GameObject player = null;
        float distanceCompare = 100;

        Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, shotatkRange);
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
            dshotcooltime = 0;
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("isAttack");
            int startBulletNum = -(bulletNum / 2);
            for (int i = 0; i < bulletNum; i++)
            {
                Vector3 targetVector = (player.transform.position - transform.position).normalized;
                targetVector += new Vector3(rotateAngle * startBulletNum, rotateAngle * startBulletNum, 0);
                float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
                GameObject _bullet = SpawnManager.Instance.FindKeepObject(bullet);
                if (_bullet == null)
                    _bullet = Instantiate(bullet, GameObject.Find("Bullet Holder").transform) as GameObject;
                _bullet.GetComponent<ShotGunBulletController>().isPlayer = false;
                _bullet.transform.position = transform.position;
                _bullet.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
                _bullet.GetComponent<ShotGunBulletController>().damage = damage;
                _bullet.GetComponent<ShotGunBulletController>().targetVector = targetVector;
                startBulletNum++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, shotatkRange);
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    private void HeatUI()
    {
        transform.Find("Heat").localPosition = new Vector3(2, 0, 0);
        transform.Find("Heat/HeatBar").transform.localScale = new Vector3(1, (float)dcooltime / (float)cooltime, 1);
    }

    private void ShotHeat1UI()
    {
        transform.Find("Heat1/HeatBar").transform.localScale = new Vector3(1, (float)dshotcooltime / (float)shotcooltime, 1);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }
}