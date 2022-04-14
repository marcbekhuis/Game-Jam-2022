using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float shootInterval = 1;
    [SerializeField] private Vector2 offset;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;

    private float timer;
    private Vector2 direction;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = Time.deltaTime;
        if (spriteRenderer.flipX) direction = Vector2.right;
        else direction = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer)
        {
            animator.SetTrigger("Attack");
            GameObject bullet = Instantiate(bulletPrefab, (Vector2)this.transform.position + offset, new Quaternion(0,0,0,0));
            bullet.GetComponent<Projectile>().Setup(direction);
            timer = Time.time + shootInterval;
        }
    }
}
