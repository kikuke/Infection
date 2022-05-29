using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBombController : Part
{
    enum State { Idle, Tracking, Return };
    State state = State.Idle;

    private float radius = 5;
    private float runningTime = 0;
    private float speed = 2;
    private float movespeed = 4;
    private float time = 0;
    private GameObject target;
    private Transform parent;
    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
            transform.root.Find("Hardwares/Parts/DronePart").localScale = new Vector3(transform.root.Find("Hardwares").localScale.x, 1, 1);

        if (state == State.Idle)
        {
            runningTime += Time.deltaTime * speed;
            Vector2 cycleVector = new Vector2(radius * Mathf.Cos(runningTime), radius * Mathf.Sin(runningTime));
            transform.localPosition = (Vector3)cycleVector;

            if (time >= cooltime)
            {
                Collider2D other = Physics2D.OverlapCircle(transform.position, atkRange);
                if (other != null && other.CompareTag("Enemy"))
                {
                    target = other.gameObject;
                    state = State.Tracking;
                }
            }
            time += Time.deltaTime;
        }

        if (state == State.Tracking)
        {
            if (target != null && target.transform.parent.name != "Enemy Keep Holder")
                transform.position += (target.transform.position - transform.position).normalized * movespeed * 3 * Time.deltaTime;
            else
                state = State.Return;
        }

        if (state == State.Return)
        {
            transform.position += (GameManager.player.transform.position - transform.position).normalized * movespeed * 5 * Time.deltaTime;

            Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, atkRange);
            foreach (Collider2D other in others)
            {
                if (other != null && other.CompareTag("Player"))
                {
                    time = 0;
                    state = State.Idle;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.Tracking && collision.CompareTag("Enemy"))
        {
            GameObject _bomb = SpawnManager.Instance.FindKeepObject(bomb);
            if (_bomb == null)
                _bomb = Instantiate(bomb, GameObject.Find("Bullet Holder").transform) as GameObject;
            _bomb.transform.position = transform.position;
            _bomb.GetComponent<MissileController>().damage = damage;
            _bomb.GetComponent<MissileController>().Start();
            _bomb.transform.localScale = new Vector3(3, 3, 1);
            state = State.Return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "폭격 드론" + "</color>";
        string description =
            showName + "\n" +
            "적에게 다가가 폭탄을 터뜨린다\n" +
            "범위 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AtkRange")][partName].ToString());

        return description;
    }
}
