using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    private float horizontal;
    private bool isJumping = true;
    private bool facingRight = true;
    private bool runAndShoot = false;
    private bool isShooting = false;
    private bool isRuning = false;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Fixed update is called once per physics change
    void FixedUpdate()
    {
        Vector2 playerMovement = new Vector2(horizontal * speed, rb.velocity.y);
        rb.velocity = playerMovement;
        // If the input is moving the player right and the player is facing left...
        if (horizontal > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontal < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }
    }
    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.C) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        if(isRuning && isShooting)
        {
            runAndShoot = true;
        }
        else
        {
            runAndShoot=false;
        }


        //Animations
        if(runAndShoot & !isJumping) {
            animator.SetBool("Run-Shoot", true);
        }
        else
        {
            animator.SetBool("Run-Shoot", false);
        }
        if (isShooting)
        {
            animator.SetBool("Shoot", true);
        }
        if (!isShooting)
        {
            animator.SetBool("Shoot", false);
        }

        if (isJumping)
        {
            animator.SetBool("Jump", true);
        }
        if (!isJumping)
        {
            animator.SetBool("Jump", false);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Run", true);
            isRuning = true;
        }
        else
        {
            animator.SetBool("Run", false);
            isRuning=false;
        }

        /* if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && !isJumping)
         {
             animator.SetBool("Run", true);
         }
         if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
         {
             animator.SetBool("Run", false);
         }*/
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        animator.SetBool("Run", false);
        Debug.Log("Not jumping");
    }
}
