using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarController : Part
{
    private float time = 0;
    private bool canAttack;
    private Vector3 targetVector;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        time = cooltime;
        GameManager.player.GetComponent<Rigidbody2D>().mass = 200;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= cooltime)
        {
            time = 0;
            canAttack = true;
            Attack();
        }
        if (canAttack == false)
            time += Time.deltaTime;

        targetVector = GameManager.player.GetComponent<PlayerController>().moveVector.normalized;

        Animation();
    }

    private void Attack()
    {
        float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        Collider2D[] others = Physics2D.OverlapBoxAll(transform.root.transform.position + targetVector * 2, Vector3.one * 2, rotateDegree);
        foreach(Collider2D other in others)
        {
            if(other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().OnDamage(damage);

                canAttack = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.root.transform.position + targetVector * 2, Vector3.one);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canAttack == true && collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().OnDamage(damage);
            canAttack = false;
        }
    }

    private void Animation()
    {
        if (GameManager.player.GetComponent<PlayerController>().moveVector != Vector3.zero)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);
    }


    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "무한궤도" + "</color>";
        string description =
            showName + "\n\n" +
            "이동속도는 다소 느리지만 앞에 있는 적에게 피해를 준다.\n";
            //"이동속도 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "MoveSpeed")][partName].ToString()) +
            //"\n공격력 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString()) +
            //"\n방어력 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Defend")][partName].ToString());

        return description;
    }

    private void OnDestroy()
    {
        GameManager.player.GetComponent<Rigidbody2D>().mass = 1;
    }
}
