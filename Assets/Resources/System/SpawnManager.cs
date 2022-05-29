using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpawnManager>();
            }

            return instance;
        }
    }

    public static bool isBoss;
    private float enemySpawn;

    private static SpawnManager instance;

    private float height;
    private float width;
    private float radius;

    int rndJunk;

    public GameObject[] enemies;
    public GameObject[] objects;
    public GameObject junkShop;
    public GameObject restore;
    private int spawnDistance = 24;
    private float enemySpawnTime = 30f;
    private float maxobjectSpawnTime = 3f;
    private float minobjectSpawnTime = 1f;
    int rndIndex;
    private int startSpawnNum = 5;
    private int genCoTime = 0;
    private float time = 0;
    private int enemyPow = 0;

    private GameObject player;

    private Transform enemyHolder;
    private Transform bulletHolder;
    private Transform objectHolder;
    private Transform enemyKeepHolder;
    private Transform bulletKeepHolder;
    private Transform objectKeepHolder;

    private float clearTime;

    // Start is called before the first frame update
    void Start()
    {
        isBoss = false;
        time = 0;
        enemyPow = 0;
        height = (2 * Camera.main.orthographicSize);
        width = (height * Camera.main.aspect);
        radius = height;

        player = GameObject.Find("Player");

        enemyHolder = GameObject.Find("Enemy Holder").transform;
        bulletHolder = GameObject.Find("Bullet Holder").transform;
        objectHolder = GameObject.Find("Object Holder").transform;
        enemyKeepHolder = GameObject.Find("Enemy Keep Holder").transform;
        bulletKeepHolder = GameObject.Find("Bullet Keep Holder").transform;
        objectKeepHolder = GameObject.Find("Object Keep Holder").transform;

        enemyKeepHolder.gameObject.SetActive(false);
        bulletKeepHolder.gameObject.SetActive(false);
        objectKeepHolder.gameObject.SetActive(false);

        clearTime = GameManager.clearTime;

        StartSpawn();
        StartCoroutine(EnemySpawn());
        if(junkShop != null)
            StartCoroutine(JunkShopSpawn());
        if (junkShop != null)
            StartCoroutine(JunkShopSpawn());
        if (junkShop != null)
            StartCoroutine(JunkShopSpawn());
        StartCoroutine(ObjectSpawn());
        StartCoroutine(ObjectSpawn());
        StartCoroutine(ObjectSpawn());
        StartCoroutine(RestoreSpawn());
        StartCoroutine(RestoreSpawn());
    }

    private void StartSpawn()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            for(int j = 0; j < startSpawnNum; j++)
            {
                Instantiate(enemies[i], Vector3.zero, Quaternion.identity, enemyKeepHolder);
            }
        }
        for (int i = 0; i < objects.Length; i++)
        {
            for (int j = 0; j < startSpawnNum; j++)
            {
                Instantiate(objects[i], Vector3.zero, Quaternion.identity, objectKeepHolder);
            }
        }
        for (int i = 0; i < startSpawnNum; i++)
        {
            if(junkShop != null)
                Instantiate(junkShop, Vector3.zero, Quaternion.identity, objectKeepHolder);
        }
        for (int i = 0; i < startSpawnNum; i++)
        {
            if (restore != null)
                Instantiate(restore, Vector3.zero, Quaternion.identity, objectKeepHolder);
        }
    }

    public void KeepObject(GameObject keepObject)
    {
        if(keepObject.CompareTag("Enemy"))
        {
            keepObject.transform.SetParent(enemyKeepHolder);
        }
        else if (keepObject.CompareTag("Data") || keepObject.CompareTag("JunkShop"))
        {
            keepObject.transform.SetParent(objectKeepHolder);
        }
        else
        {
            keepObject.transform.SetParent(bulletKeepHolder);
        }
    }

    public GameObject FindKeepObject(GameObject keepObject)
    {
        GameObject bullet = null;

        if (bulletKeepHolder.Find(keepObject.name + "(Clone)") != null)
        {
            bullet = bulletKeepHolder.Find(keepObject.name + "(Clone)").gameObject;
            bullet.transform.SetParent(bulletHolder);
        }

        return bullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 10)
        {
            time = 0;
            if (genCoTime < 30 && GameManager.gameTime > 60 * 4.5f)
            {
                genCoTime++;
                enemySpawnTime -= 1.3f * Mathf.Pow(0.87f, genCoTime);
            }
            if(GameManager.gameTime > 60 * 5.5f)
            {
                enemyPow++;
            }
        }
        time += Time.deltaTime;
    }

    IEnumerator EnemySpawn()
    {
        GameObject enemy = null;
        float rndPercentage;

        while (true)
        {
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                if (GameManager.gameTime > 60 * 5f)
                {
                    rndPercentage = Random.Range(0, 1f);
                    if (rndPercentage > 0.9f)
                        rndIndex = 3;
                    else if (rndPercentage > 0.7f)
                        rndIndex = 2;
                    else if (rndPercentage > 0.5f)
                        rndIndex = 1;
                    else
                        rndIndex = 0;
                }
                else if (GameManager.gameTime > 60 * 3f)
                {
                    rndPercentage = Random.Range(0, 1f);
                    if (rndPercentage > 0.9f)
                        rndIndex = 3;
                    else if (rndPercentage > 0.7f)
                        rndIndex = 2;
                    else if (rndPercentage > 0.5f)
                        rndIndex = 1;
                    else
                        rndIndex = 0;
                }
                else if (GameManager.gameTime > 60 * 2f)
                {
                    rndPercentage = Random.Range(0, 1f);
                    if (rndPercentage > 0.7f / 0.9f)
                        rndIndex = 2;
                    else if (rndPercentage > 0.5f / 0.9f)
                        rndIndex = 1;
                    else
                        rndIndex = 0;
                }
                else if (GameManager.gameTime > 60 * 1f)
                {
                    rndPercentage = Random.Range(0, 1f);
                    if (rndPercentage > 0.5f / 0.7f)
                        rndIndex = 1;
                    else
                        rndIndex = 0;
                }
                else
                {
                    rndIndex = 0;
                }

                if (enemyKeepHolder.Find(enemies[rndIndex].name + "(Clone)") != null)
                {
                    enemy = enemyKeepHolder.Find(enemies[rndIndex].name + "(Clone)").gameObject;
                    enemy.transform.position = RandomPos() + player.transform.position;
                    enemy.transform.SetParent(enemyHolder);
                }
                else
                {
                    enemy = Instantiate(enemies[rndIndex], RandomPos() + player.transform.position, Quaternion.identity, enemyHolder) as GameObject;
                }
                if (enemyPow != 0)
                {
                    enemy.GetComponent<Enemy>().SetStat(0.3f * Mathf.Pow(1.2f, enemyPow));
                }
            }

            enemySpawn = enemySpawnTime / 90;
            if (isBoss)
                enemySpawn *= 3f;
            yield return new WaitForSeconds(enemySpawn);
        }
    }


    IEnumerator ObjectSpawn()
    {
        while (true)
        {
            float rndPer = Random.Range(0, 1f);
            if (rndPer > 0.9f)
                rndIndex = 2;
            else if (rndPer > 0.7f)
            {
                rndIndex = 1;
            }
            else
                rndIndex = 0;

            if (objectKeepHolder.Find(objects[rndIndex].name + "(Clone)") != null)
            {
                GameObject data = objectKeepHolder.Find(objects[rndIndex].name + "(Clone)").gameObject;
                data.transform.position = RandomPos() + player.transform.position;
                data.transform.SetParent(objectHolder);
            }
            else
                Instantiate(objects[rndIndex], RandomPos() + player.transform.position, Quaternion.identity, objectHolder);

            yield return new WaitForSeconds(Random.Range(4, 7));
        }
    }

    IEnumerator RestoreSpawn()
    {
        while (true)
        {
            if (objectKeepHolder.Find(restore.name + "(Clone)") != null)
            {
                GameObject _restore = objectKeepHolder.Find(restore.name + "(Clone)").gameObject;
                _restore.transform.position = RandomPos() + player.transform.position;
                _restore.transform.SetParent(objectHolder);
            }
            else
                Instantiate(restore, RandomPos() + player.transform.position, Quaternion.identity, objectHolder);
            yield return new WaitForSeconds(Random.Range(4, 10));
        }
    }

    IEnumerator JunkShopSpawn()
    {
        while (true)
        {
            if (objectKeepHolder.Find(junkShop.name + "(Clone)") != null)
            {
                GameObject junkshop = objectKeepHolder.Find(junkShop.name + "(Clone)").gameObject;
                junkshop.transform.position = RandomPos() + player.transform.position;
                junkshop.transform.SetParent(objectHolder);
            }
            else
                Instantiate(junkShop, RandomPos() + player.transform.position, Quaternion.identity, objectHolder);
            yield return new WaitForSeconds(Random.Range(15, 30));
        }
    }

    //private Vector3 RandomPos()
    //{
    //    Vector3 dir = player.GetComponent<PlayerController>().moveVector;
    //    Vector3 rndPos = Vector3.zero;
    //
    //    Debug.Log(height);
    //    Debug.Log(width);
    //
    //    if (dir != Vector3.zero)
    //    {
    //        rndPos = new Vector3(dir.x * width, dir.y * height, 0);
    //        //if (dir.x == 0)
    //        //    dir = new Vector3(dir.x + Random.Range(-(spawnDistance / 2), spawnDistance / 2), dir.y, 0);
    //        //if (dir.y == 0)
    //        //    dir = new Vector3(dir.x, dir.y + Random.Range(-(spawnDistance), spawnDistance), 0);
    //        //if (dir.x != 0 && dir.y != 0)
    //        //{
    //        //    rndIndex = Random.Range(0, 2);
    //        //    if(rndIndex == 0)
    //        //        rndPos = new Vector3(rndPos.x, rndPos.y + Random.Range(-height, height), 0);
    //        //    if(rndIndex == 1)
    //        //        rndPos = new Vector3(rndPos.x + Random.Range(-width, width), rndPos.y, 0);
    //        //}
    //        if (dir.x != 0)
    //            rndPos += new Vector3(0, Random.Range(-height, height) / 3, 0);
    //        if (dir.y != 0)
    //            rndPos += new Vector3(Random.Range(-width, width) / 3, 0, 0);
    //    }
    //
    //    else if (dir == Vector3.zero)
    //    {
    //        int rnd = Random.Range(0, 4);
    //        if (rnd == 0) //위
    //        {
    //            rndPos = new Vector3(Random.Range(0, width), height, 0);
    //        }
    //        if (rnd == 1) //아래
    //        {
    //            rndPos = new Vector3(Random.Range(0, width), -height, 0);
    //        }
    //        if (rnd == 2) //왼쪽
    //        {
    //            rndPos = new Vector3(-width, Random.Range(0, height), 0);
    //        }
    //        if (rnd == 3) //오른쪽
    //        {
    //            rndPos = new Vector3(width, Random.Range(0, height), 0);
    //        }
    //    }
    //
    //    return rndPos;
    //}

    private Vector3 RandomPos()
    {
        Vector3 dir = player.GetComponent<PlayerController>().moveVector;
        Vector3 rndPos = Vector3.zero;
        
        if (dir != Vector3.zero)
        {
            float rotateDegree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            float angle = Random.Range(rotateDegree - 60, rotateDegree + 60) * Mathf.Deg2Rad;
            float xPos = Mathf.Cos(angle) * radius;
            float yPos = Mathf.Sin(angle) * radius;

            rndPos = new Vector2(xPos, yPos);
        }
    
        else if (dir == Vector3.zero)
        {
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            float xPos = Mathf.Cos(angle) * radius;
            float yPos = Mathf.Sin(angle) * radius;

            rndPos = new Vector2(xPos, yPos);
        }
    
        return rndPos * 0.7f;
    }
}
