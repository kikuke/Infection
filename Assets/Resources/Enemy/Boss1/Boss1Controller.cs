using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : Enemy // 이거만들고 대사 스크립트 짜기타임스케일 0으로 해서 끝나면 되돌리는 것으로
{
    GameObject player;
    private SpriteRenderer spriteRenderer;
    public float atkRange;
    public float cooltime;//
    private float dcooltime;

    //무기전용 변수

    public GameObject bullet;
    private int bulletNum = 5;
    private float rotateAngle = 0.1f;

    //보스 전용 함수들
    private void Awake()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        //무기
        //cooltime = 1f;
        //atkRange = 7f;//
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
        Vector3 target = (player.transform.position - transform.position).normalized;
        transform.position += target * moveSpeed * Time.deltaTime;

        if (target.x > 0)
            spriteRenderer.flipX = false;
        if (target.x < 0)
            spriteRenderer.flipX = true;

        //무기
        if (dcooltime >= cooltime)
        {
            Attack();
        }
        if (dcooltime < cooltime)
            dcooltime += Time.deltaTime;
        HeatUI();
    }
    protected override void KillScore()//실상 죽을때 실행시키는 함수로 사용하기
    {
        OperationLogManager.KillScore(150000);
    }

    //밑에서부터 무기함수들
    private void Attack()
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
