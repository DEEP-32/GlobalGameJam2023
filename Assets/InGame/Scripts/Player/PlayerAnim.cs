
using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator anim;
    private PlayerController controller;


    private void Awake()
    {
        controller= GetComponent<PlayerController>();   
    }
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        jumpAnim();
        RunAnim();
        Attack();
    }

    private void RunAnim()
    {
        if (controller.Input.X > 0 || controller.Input.X < 0 && controller.Grounded)
        {
            anim.SetBool("isRunning", true);
        }
        else if (controller.Input.X ==0 ||!controller.Grounded)
        {
            anim.SetBool("isRunning", false);
        }
    }
       

    private void jumpAnim()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            anim.SetTrigger("isJump");
            anim.SetBool("isRunning", false);
        }
  
    }

    private void Attack()
    {
        if (controller.Input.AttackInput)
        {
            
            anim.SetBool("isAttack", true);

        }
        else
        {
            anim.SetBool("isAttack", false);
   
        }

    }
}
