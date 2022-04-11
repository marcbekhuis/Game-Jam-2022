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
    [SerializeField] private float jumpCooldown = 1;
    private float jumpTimer = 0;

    [Header("Ground Check")]
    [SerializeField] private Vector2 halfSize;
    [SerializeField] private Vector2 offset;
    [SerializeField] private LayerMask checkLayers;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int x = System.Convert.ToInt32(Input.GetKey(KeyCode.D)) - System.Convert.ToInt32(Input.GetKey(KeyCode.A));
        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) speed = runSpeed;
        
        rigidbody.velocity = new Vector2(x * speed, rigidbody.velocity.y);
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (GroundCheck() && Time.time > jumpTimer)
            {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpTimer = Time.time + jumpCooldown;
            }
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapBox((Vector2)this.transform.position + offset, halfSize, 0, checkLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube((Vector2)this.transform.position + offset, halfSize);
    }
}
