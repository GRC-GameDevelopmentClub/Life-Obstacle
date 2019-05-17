using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float playerXValue;

    public float playerHealth;

    //public Animator animator;
    
    

    // Start is called before the first frame update
    void Start()
    {
        playerXValue = GetComponent<Transform>().position.x;
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.A) && playerXValue > -6)
        {
            GetComponent<Transform>().position = new Vector2(playerXValue -= 3f, 0);
            
        }

        if (Input.GetKeyDown(KeyCode.D) && playerXValue < 6)
        {
            GetComponent<Transform>().position = new Vector2(playerXValue += 3f, 0);
            
        }

        

        if(playerHealth <= 0)
        {
            Debug.Log("Game Over");
        }

    }
}
