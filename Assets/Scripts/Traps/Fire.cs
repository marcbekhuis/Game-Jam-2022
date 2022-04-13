using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float minOffTime = 1;
    [SerializeField] private float maxOffTime = 2;
    [SerializeField] private float minOnTime = 1;
    [SerializeField] private float maxOnTime = 2;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject damageBox;

    private bool on;
    private float timer;

    private void Start()
    {
        on = Random.Range(0, 10f) < 5;
        if (on) SetOff();
        else SetOn();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer)
        {
            if (on) SetOff();
            else SetOn();
        }
    }

    private void SetOn()
    {
        on = true;
        damageBox.SetActive(true);
        animator.SetBool("On", true);
        timer = Time.time + Random.Range(minOnTime, maxOnTime);
    }

    private void SetOff()
    {
        on = false;
        damageBox.SetActive(false);
        animator.SetBool("On", false);
        timer = Time.time + Random.Range(minOffTime, maxOffTime);
    }
}
