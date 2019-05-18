using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public PlayerBehavior playerBehavior;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && playerBehavior.playerXValue > -6)
        {
            animator.SetTrigger("MoveLeft");

        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.D) && playerBehavior.playerXValue < 6)
        {
            animator.SetTrigger("MoveRight");

        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetTrigger("Idle");
        }
    }
}
