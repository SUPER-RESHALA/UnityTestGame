using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private float moveInput;
    public float jumpForce;
    public Transform feetPos;
    public LayerMask whatIsGround;
    public float checkRadius;
    private bool isGrounded;
    private Animator animator;
    public float health;
    public int numOfHearts;
    public Image[] hearts;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded==true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    private void FixedUpdate()
    {
moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
       



        if (moveInput == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else {
            animator.SetBool("isWalking", true);
        }
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
          


        }
        else if (moveInput > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
      
        

    }

   
}
