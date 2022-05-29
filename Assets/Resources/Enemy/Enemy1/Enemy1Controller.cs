using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : Enemy
{
    GameObject player;
    private SpriteRenderer spriteRenderer;

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
        OperationLogManager.KillScore(5);//50점짜리 몬스터
    }

    new private void OnBecameInvisible()
    {
        base.OnBecameInvisible();
    }
    new private void OnBecameVisible()
    {
        base.OnBecameVisible();
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        if (hp <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Supply(0.3f);
        }
    }
}
