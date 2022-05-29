using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearPunchAddForceController : MonoBehaviour
{
    public GameObject boss;
    public GameObject nuclear;
    public Vector3 targetVector;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    private void FixedUpdate()
    {
        if(boss != null)
        {
            rigidbody = boss.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(targetVector * 10f, ForceMode2D.Impulse);
            if (Vector2.Distance(GameManager.player.transform.position, nuclear.transform.position) < 7f)
            {
                rigidbody = GameManager.player.GetComponent<Rigidbody2D>();
                rigidbody.AddForce(targetVector * 100f, ForceMode2D.Impulse);
            }
        }
        else
        {
            rigidbody = GameManager.player.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(targetVector * 100f, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        rigidbody.velocity = Vector2.zero;
    }
}
