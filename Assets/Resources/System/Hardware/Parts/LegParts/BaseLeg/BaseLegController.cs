using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLegController : Part
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
    }

    private void Animation()
    {
        if(GameManager.player.GetComponent<PlayerController>().moveVector != Vector3.zero)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "기계 다리" + "</color>";
        string description =
            showName + "\n\n" +
            "기본적인 다리다.\n";
            //"이동속도 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "MoveSpeed")][partName].ToString());

        return description;
    }
}
