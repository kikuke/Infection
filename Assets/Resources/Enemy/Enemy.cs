using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isBoss = false;

    private float tempMaxHp;
    public float maxHp;
    public float hp;
    public float damage;
    public float moveSpeed;

    private float time = 0;
    private float destroyTime = 5;
    private bool isInvisible = false;
    private float attackTime = 0.5f;
    private float attackCooltime = 0.5f;

    public void SetStat(float _maxHp)
    {
        maxHp += maxHp * _maxHp;
        hp = maxHp;
    }

    private void Awake()
    {
        string name = gameObject.name.Substring(0, gameObject.name.IndexOf("(Clone)"));
        if (!isBoss)
        {
            tempMaxHp = float.Parse(GlobalManager.enemylist[GlobalManager.FindOption(GlobalManager.enemylist, "MaxHp")][name].ToString());
            damage = float.Parse(GlobalManager.enemylist[GlobalManager.FindOption(GlobalManager.enemylist, "Damage")][name].ToString());
            moveSpeed = float.Parse(GlobalManager.enemylist[GlobalManager.FindOption(GlobalManager.enemylist, "MoveSpeed")][name].ToString());
        }
    }

    // Start is called before the first frame update
    protected void OnEnable()
    {
        if (!isBoss)
        {
            isInvisible = false;
            time = 0;
            maxHp = tempMaxHp;
            hp = maxHp;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if (isInvisible == true)
        {
            if (time >= destroyTime)
            {
                SpawnManager.Instance.KeepObject(gameObject);
            }
            time += Time.deltaTime;
        }

        attackTime += Time.deltaTime;
    }

    public virtual void OnDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            if (!isBoss)
            {
                GetComponent<SpriteRenderer>().material.shader = Shader.Find("Sprites/Default");
                KillScore();
                SpawnManager.Instance.KeepObject(gameObject);
            }
            else
            {
                SpawnManager.isBoss = false;
                Debug.Log(GameObject.Find("GameManager").GetComponent<GameManager>().basesupplygage);
                GameObject.Find("GameManager").GetComponent<GameManager>().Supply(GameObject.Find("GameManager").GetComponent<GameManager>().basesupplygage);
                GetComponent<SpriteRenderer>().material.shader = Shader.Find("Sprites/Default");
                GMZone3.level++;
                BossGenerator.level++;
                KillScore();
                Destroy(gameObject);
                BossGenerator.isdead = true;
                BossGenerator.dgenTime = 0f;
                MessageController.BossDest();
            }
        }
        else
            StartCoroutine(ShowHitFlash());
    }

    protected virtual void KillScore()
    {

    }

    IEnumerator ShowHitFlash()
    {
        GetComponent<SpriteRenderer>().material.shader = Shader.Find("PaintWhite");
        yield return new WaitForSeconds(0.02f);
        GetComponent<SpriteRenderer>().material.shader = Shader.Find("Sprites/Default");
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerController>().OnDamage(damage);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackTime >= attackCooltime)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>().OnDamage(damage);
                attackTime = 0;
            }
        }
    }

    protected void OnBecameInvisible()
    {
        isInvisible = true;
    }

    protected void OnBecameVisible()
    {
        isInvisible = false;
        time = 0;
    }

    protected void HpUI()
    {
        transform.Find("Hp").transform.Find("HpBar").transform.localScale = new Vector3((float)hp / (float)maxHp, 1, 1);
    }
}
