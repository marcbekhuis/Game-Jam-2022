using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runSpeed = 6;
    [SerializeField] private float jumpForce = 5;

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
        if (Input.GetKeyDown(KeyCode.Space)) rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
