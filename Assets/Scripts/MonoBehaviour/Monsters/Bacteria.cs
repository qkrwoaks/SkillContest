using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Monster
{
    private void Start()
    {
        Init();
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
