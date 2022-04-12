
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : Monster
{
    private void Start()
    {
        Init();
        StartCoroutine(Fire());
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }
}