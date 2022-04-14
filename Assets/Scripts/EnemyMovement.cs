using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runSpeed = 3;
    [SerializeField] private float range = 4;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask playerLayer;
    [Header("Ground check")]
    [SerializeField] private Vector2 offsetLeft;
    [SerializeField] private Vector2 offsetRight;
    [SerializeField] private Vector2 halfSizeLeft;
    [SerializeField] private Vector2 halfSizeRight;
    [SerializeField] private LayerMask groundLayer;
    [Header("Wall check")]
    [SerializeField] private Vector2 offsetLeftWall;
    [SerializeField] private Vector2 offsetRightWall;
    [SerializeField] private Vector2 halfSizeLeftWall;
    [SerializeField] private Vector2 halfSizeRightWall;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rigidbody;
    private Vector2 direction;

    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (Random.Range(0, 10) < 5) direction = Vector2.right;
        else direction = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthSystem.death)
        {
            rigidbody.velocity = Vector2.zero;
            return;
        }
        WallCheck();
        float speed = walkSpeed;
        if(!GroundCheck())
            if (PlayerInRange()) 
                speed = runSpeed;

        rigidbody.velocity = new Vector2(direction.x * speed, rigidbody.velocity.y);
    }

    private bool PlayerInRange()
    {
        Collider2D player = Physics2D.OverlapCircle(this.transform.position, range, playerLayer);
        if (player)
        {
            if (player.transform.position.x < this.transform.position.x)
            {
                direction = Vector2.left;
                spriteRenderer.flipX = false;
            }
            else
            {
                direction = Vector2.right;
                spriteRenderer.flipX = true;
            }
            return true;
        }
        return false;
    }

    private bool GroundCheck()
    {
        if (!Physics2D.OverlapBox((Vector2)this.transform.position + offsetLeft, halfSizeLeft, 0, groundLayer))
        {
            direction = Vector2.right;
            spriteRenderer.flipX = true;
            return true;
        }
        else if (!Physics2D.OverlapBox((Vector2)this.transform.position + offsetRight, halfSizeRight, 0, groundLayer))
        {
            direction = Vector2.left;
            spriteRenderer.flipX = false;
            return true;
        }
        return false;
    }

    private bool WallCheck()
    {
        if (Physics2D.OverlapBox((Vector2)this.transform.position + offsetLeftWall, halfSizeLeftWall, 0, wallLayer))
        {
            direction = Vector2.right;
            spriteRenderer.flipX = true;
            return true;
        }
        else if (Physics2D.OverlapBox((Vector2)this.transform.position + offsetRightWall, halfSizeRightWall, 0, wallLayer))
        {
            direction = Vector2.left;
            spriteRenderer.flipX = false;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube((Vector2)this.transform.position + offsetLeft, halfSizeLeft);
        Gizmos.DrawCube((Vector2)this.transform.position + offsetRight, halfSizeRight);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube((Vector2)this.transform.position + offsetLeftWall, halfSizeLeftWall);
        Gizmos.DrawCube((Vector2)this.transform.position + offsetRightWall, halfSizeRightWall);
    }
}
