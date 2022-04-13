using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform saw;
    private SpriteRenderer spriteRenderer;

    private float halfHeight;
    private float progress;
    private int direction = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        halfHeight = spriteRenderer.size.y / 2f;
        progress = Random.Range(0f,1f);
    }

    private void Update()
    {
        DirectionCheck();
        progress = Mathf.Clamp(progress + direction * (speed / spriteRenderer.size.y) * Time.deltaTime, 0, 1);
        saw.position = new Vector3(this.transform.position.x, this.transform.position.y + Mathf.Lerp(-halfHeight, halfHeight, progress),0);
    }

    private void DirectionCheck()
    {
        if (progress >= 1) direction = -1;
        else if (progress <= 0) direction = 1;
    }
}
