using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCell : Monster
{
    bool isDash = false;
    bool endDash = false;

    Vector3 originPos;
    Vector3 playerPos;

    private void Start()
    {
        Init();
        StartCoroutine(Fire());
        StartCoroutine(Dash());
    }

    private void FixedUpdate()
    {
        if (isDash == true) MoveDash();
        else if (isDash == false && endDash == false) Move();
        else if (isDash == false && endDash == true) MoveReturn();
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }
    IEnumerator Dash()
    {
        yield return new WaitForSeconds(1f);
        originPos = transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        isDash = true;
    }

    void MoveDash()
    {
        transform.position =  Vector2.MoveTowards(transform.position, playerPos, (speed + 1f) * Time.deltaTime);
        if (transform.position == playerPos)
        {
            isDash = false;
            endDash = true;
        }
    }

    void MoveReturn()
    {
        transform.position = Vector2.MoveTowards(transform.position, originPos, (speed + 1f) * Time.deltaTime);
        if (transform.position == originPos)
        {
            isDash = false;
            endDash = false;
            StartCoroutine(Dash());
        }
    }
}
