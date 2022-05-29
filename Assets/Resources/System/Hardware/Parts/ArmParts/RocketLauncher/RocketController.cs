using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public GameObject boss;//보스를 대입해야함#####

    public GameObject target;
    public GameObject bomb;
    private Vector3 targetVector;
    public float damage;
    public float speed;

    // Start is called before the first frame update
    public void Start()
    {
        speed = 1f;
        targetVector = target.transform.position - transform.position;

        if(target.CompareTag("Enemy"))
            transform.localPosition = GameManager.player.transform.position + targetVector.normalized * 2;
        else
            transform.localPosition = boss.transform.position + targetVector.normalized * 2;//보스를 대입해야함

        float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
    }

    private void OnEnable()
    {
        StartCoroutine(SetSmoke());
    }

    // Update is called once per frame
    void Update()
    {
        speed += Time.deltaTime;
        transform.position += targetVector.normalized * Mathf.Pow(speed, 10) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target.CompareTag("Enemy"))
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().OnDamage(damage);
                GameObject _bomb = SpawnManager.Instance.FindKeepObject(bomb);
                if (_bomb == null)
                    _bomb = Instantiate(bomb, GameObject.Find("Bullet Holder").transform) as GameObject;
                _bomb.transform.position = transform.position;
                _bomb.transform.localScale = FindObjectOfType<RocketLauncherUpgradeController>() != null && FindObjectOfType<RocketLauncherUpgradeController>().level >= 2 ? Vector3.one * 4 * (FindObjectOfType<RocketLauncherUpgradeController>().explosionRange + 1) : Vector3.one * 4;
                _bomb.GetComponent<MissileController>().damage = 3;
                _bomb.GetComponent<MissileController>().Start();
                SpawnManager.Instance.KeepObject(gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>().OnDamage(damage);
                GameObject _bomb = SpawnManager.Instance.FindKeepObject(bomb);
                if (_bomb == null)
                    _bomb = Instantiate(bomb, GameObject.Find("Bullet Holder").transform) as GameObject;
                bomb.GetComponent<MissileController>().isPlayer = false;
                _bomb.transform.position = transform.position;
                _bomb.transform.localScale = Vector3.one * 4;
                _bomb.GetComponent<MissileController>().damage = 1;
                _bomb.GetComponent<MissileController>().Start();
                SpawnManager.Instance.KeepObject(gameObject);
            }
        }
    }

    private IEnumerator SetSmoke()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnBecameInvisible()
    {
        SpawnManager.Instance.KeepObject(gameObject);
    }
}
