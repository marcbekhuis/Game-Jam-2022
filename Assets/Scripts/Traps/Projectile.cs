using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    private SpriteRenderer spriteRenderer;
    private Vector2 direction;

    public void Setup(Vector2 direction)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.direction = direction;
        spriteRenderer.flipX = direction.x > 0;
    }

    private void Update()
    {
        this.transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
