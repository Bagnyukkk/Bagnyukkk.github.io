using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAnimationTesting : MonoBehaviour
{
    public Animator animator;

    public Transform player;
    [SerializeField] private float blockingDistance = 10f;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.R))
            animator.SetBool("isRunningForward", true);
        else
            animator.SetBool("isRunningForward", false);

        if (Input.GetKeyDown(KeyCode.P))
            animator.SetTrigger("punchRight");
        if (Input.GetKeyDown(KeyCode.L))
            animator.SetTrigger("punchLeft");

        if (Input.GetKeyDown(KeyCode.Alpha0))
            animator.SetTrigger("fall");
    

        if(Vector3.Distance(transform.position, player.position) < blockingDistance)
            animator.SetBool("isBlocking", true);
        else
            animator.SetBool("isBlocking", false);

    }
}
