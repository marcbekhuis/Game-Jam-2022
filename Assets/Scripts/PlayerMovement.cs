using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runSpeed = 6;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private int jumps = 2;
    [SerializeField] private float jumpCooldown = 1;
    private float jumpTimer = 0;
    private int jumpCounter;

    [Header("Ground Check")]
    [SerializeField] private Vector2 halfSize;
    [SerializeField] private Vector2 offset;
    [SerializeField] private LayerMask checkLayers;
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private Rigidbody2D rigidbody;
    bool run;

    [HideInInspector] public bool onGround = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpCounter = jumps;
        jumpTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthSystem.death)
        {
            rigidbody.velocity = Vector2.zero;
            return;
        }
        GroundCheck();

        run = Input.GetKey(KeyCode.LeftShift);
        int x = System.Convert.ToInt32(Input.GetKey(KeyCode.D)) - System.Convert.ToInt32(Input.GetKey(KeyCode.A));
        float speed = walkSpeed;
        if (run) speed = runSpeed;
        
        rigidbody.velocity = new Vector2(x * speed, rigidbody.velocity.y);
        Jump();
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        spriteRenderer.flipX = rigidbody.velocity.x < 0;
        if(Mathf.RoundToInt(rigidbody.velocity.x) != 0)
        {
            if (run)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }

        if (!onGround) animator.SetBool("Fall", true);
        else animator.SetBool("Fall", false);
    }

    private void Jump()
    {
        if (onGround) jumpCounter = jumps;

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (jumpCounter > 1 && Time.time > jumpTimer)
            {
                if (onGround) animator.SetTrigger("Jump");
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpTimer = Time.time + jumpCooldown;
                jumpCounter--;
            }
        }
    }

    private void GroundCheck()
    {
        onGround = Physics2D.OverlapBox((Vector2)this.transform.position + offset, halfSize, 0, checkLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube((Vector2)this.transform.position + offset, halfSize);
    }
}
