using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    private float hangCounter;
    private float jumpBufferCount;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    

    public float speed = 8f;
    public float hangTime = .2f;
    public float jumpBuggerLenght = .1f;
    [Header("Camera")]
    public Transform camTarget;
    public float aheadAmount, aheadSpeed;

    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //Big jump
        if (jumpBufferCount >= 0 && hangCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCount = 0;
        }

        //Small jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            jumpBufferCount = 0;
        }

        //Hantime
        if (IsGrounded() && rb.velocity.y == 0)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        
        //JumpBuffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = jumpBuggerLenght;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        Flip();

        //Move camera point
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x,  aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime) ,camTarget.localPosition.y, camTarget.localPosition.z);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
            aheadAmount = -aheadAmount;
        }
    }
}
