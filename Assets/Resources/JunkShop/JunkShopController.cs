using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkShopController : MonoBehaviour
{
    private float time = 0;
    private float destroyTime = 10;
    private bool isInvisible = false;

    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
    }

    private void OnEnable()
    {
        isInvisible = false;
        time = 0;
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            GameManager.upgradePartsSystem.SetActive(true);
            UpgradePartsSystem.isset = true;

            SpawnManager.Instance.KeepObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.upgradePartsSystem.SetActive(true);
            UpgradePartsSystem.isset = true;


            SpawnManager.Instance.KeepObject(gameObject);
        }
    }

    public void OnButton()
    {
        GameManager.upgradePartsSystem.SetActive(true);
        UpgradePartsSystem.isset = true;
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
