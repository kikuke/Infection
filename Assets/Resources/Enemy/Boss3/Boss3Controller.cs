using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Controller : Enemy // 이거만들고 대사 스크립트 짜기타임스케일 0으로 해서 끝나면 되돌리는 것으로
{
    GameObject player;
    private SpriteRenderer spriteRenderer;
    public float atkRange;
    public float atkcooltime;//
    private float datkcooltime;

    //무기전용 변수
    public GameObject nuclear;
    public GameObject nuclearPunchAddForce;
    private Vector3 targetVector;
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
        //atkcooltime = 10f;
        //atkRange = 10f;//
        transform.localPosition = Vector3.zero;
        transform.Find("Heat").transform.localPosition = new Vector3(1, 0, 0);
        //스텔스
        //stealthCoolTime = 3;
        collider2D = gameObject.GetComponent<Collider2D>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()//대사
    {
        hp = maxHp;
        datkcooltime = atkcooltime;
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
        if (datkcooltime >= atkcooltime)
        {
            Attack();
        }
        if (datkcooltime < atkcooltime)
            datkcooltime += Time.deltaTime;

        targetVector = target;

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
        OperationLogManager.KillScore(300000);
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
            datkcooltime = 0;
            float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
            GameObject _nuclear = Instantiate(nuclear, transform.root.transform.position, Quaternion.Euler(0, 0, rotateDegree), GameObject.Find("Bullet Holder").transform) as GameObject;
            _nuclear.GetComponent<NuclearController>().isPlayer = false;
            _nuclear.GetComponent<NuclearController>().damage = damage;
            _nuclear.GetComponent<NuclearController>().targetVector = targetVector;
            GameObject _nuclearPunchAddForce = Instantiate(nuclearPunchAddForce, transform.root) as GameObject;
            _nuclearPunchAddForce.GetComponent<NuclearPunchAddForceController>().boss = gameObject;
            _nuclearPunchAddForce.GetComponent<NuclearPunchAddForceController>().nuclear = _nuclear;
            _nuclearPunchAddForce.GetComponent<NuclearPunchAddForceController>().targetVector = targetVector;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    private void HeatUI()
    {
        transform.Find("Heat/HeatBar").transform.localScale = new Vector3(1, (float)datkcooltime / (float)atkcooltime, 1);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }
}
