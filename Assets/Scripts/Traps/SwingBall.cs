using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBall : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float angle = 45;

    [SerializeField] private SpriteRenderer chain;
    [SerializeField] private Transform ball;

    private float progress = 0;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        chain.transform.localPosition = new Vector3(0, -(chain.size.y / 2f), 0);
        ball.transform.localPosition = new Vector3(0, -chain.size.y, 0);
        progress = Random.Range(0,1);
    }

    private void Update()
    {
        if (HealthSystem.death) return;
        DirectionCheck();
        progress = Mathf.Clamp(progress + direction * (speed / angle) * Time.deltaTime, 0, 1);
        this.transform.eulerAngles = new Vector3(0,0, this.transform.position.y + Mathf.Lerp(-angle, angle, progress));
    }

    private void DirectionCheck()
    {
        if (progress >= 1) direction = -1;
        else if (progress <= 0) direction = 1;
    }
}
