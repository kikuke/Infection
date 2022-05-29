using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isBoss = false;

    public float maxHp;
    public float hp;
    public float damage;
    public float moveSpeed;

    private float time = 0;
    private float destroyTime = 10;
    private bool isInvisible = false;

    private void Awake()
    {
        string name = gameObject.name.Substring(0, gameObject.name.IndexOf("(Clone)"));
        if (!isBoss)
        {
            maxHp = float.Parse(GlobalManager.enemylist[GlobalManager.FindOption(GlobalManager.enemylist, "MaxHp")][name].ToString());
            damage = float.Parse(GlobalManager.enemylist[GlobalManager.FindOption(GlobalManager.enemylist, "Damage")][name].ToString());
            moveSpeed = float.Parse(GlobalManager.enemylist[GlobalManager.FindOption(GlobalManager.enemylist, "MoveSpeed")][name].ToString());
        }
    }

    // Start is called before the first frame update
    protected void OnEnable()
    {
        hp = maxHp;
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
    }

    public void OnDamage(float damage)
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
                KillScore();
                Destroy(gameObject);
                BossGenerator.isdead = true;
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
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerController>().OnDamage(damage);
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
}
