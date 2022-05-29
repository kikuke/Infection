using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Controller : Enemy
{
    GameObject player;
    private SpriteRenderer spriteRenderer;
    public GameObject bomb;

    Vector3 target;

    float targetTime;
    float targetDTime;
    // Start is called before the first frame update
    new void OnEnable()
    {
        targetDTime = targetTime = 1.2f;
        base.OnEnable();
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (targetDTime >= targetTime)
            target = (player.transform.position - transform.position).normalized;
        else
            targetDTime += Time.deltaTime;

        transform.position += target * moveSpeed * Time.deltaTime;

        if (target.x > 0)
            spriteRenderer.flipX = false;
        if (target.x < 0)
            spriteRenderer.flipX = true;
    }

    protected override void KillScore()
    {
        OperationLogManager.KillScore(50);
    }

    new private void OnBecameInvisible()
    {
        base.OnBecameInvisible();
    }
    new private void OnBecameVisible()
    {
        base.OnBecameVisible();
    }

    new private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject _bomb = SpawnManager.Instance.FindKeepObject(bomb);
            if (_bomb == null)
                _bomb = Instantiate(bomb, GameObject.Find("Bullet Holder").transform) as GameObject;
            _bomb.transform.position = transform.position;
            _bomb.GetComponent<Enemy4BombController>().damage = damage;
            _bomb.transform.localScale = new Vector3(5, 5, 1);
            GetComponent<SpriteRenderer>().material.shader = Shader.Find("Sprites/Default");
            SpawnManager.Instance.KeepObject(gameObject);
        }
    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject _bomb = SpawnManager.Instance.FindKeepObject(bomb);
            if (_bomb == null)
                _bomb = Instantiate(bomb, GameObject.Find("Bullet Holder").transform) as GameObject;
            _bomb.transform.position = transform.position;
            _bomb.GetComponent<Enemy4BombController>().damage = damage;
            _bomb.transform.localScale = new Vector3(5, 5, 1);
            GetComponent<SpriteRenderer>().material.shader = Shader.Find("Sprites/Default");
            SpawnManager.Instance.KeepObject(gameObject);
        }
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        if (hp <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Supply(1.2f);
        }
    }
}
