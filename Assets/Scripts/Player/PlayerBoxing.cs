using UnityEngine;

public class PlayerBoxing : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Player player;


    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            animator.SetBool("IsBlocking", true);
            player.isInBlock = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("LeftPunch");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("RightPunch");
        }else
        {
            animator.SetBool("IsBlocking", false);
            player.isInBlock = false;
        }
    }
}
