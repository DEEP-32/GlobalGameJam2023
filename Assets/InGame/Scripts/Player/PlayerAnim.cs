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
        //Attack();
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
            anim.SetBool("falling", false);
            anim.SetBool("isRunning", false);
        }
        if (controller.Velocity.y < 0 && !controller.Grounded)
        {

            anim.SetBool("falling", true);
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("falling", false);
        }
       // Debug.Log(controller.Velocity.y);
 
    }

    
}
