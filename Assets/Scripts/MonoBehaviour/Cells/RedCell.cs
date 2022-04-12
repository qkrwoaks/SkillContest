using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCell : Cell
{
    public override void OnDead()
    {
        GameManager.instance.AppendToPain(0.1f);
        base.OnDead();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Monster")) OnDamage(collision.GetComponent<Monster>().power);
        else if (collision.CompareTag("MonsterBullet")) OnDamage(collision.GetComponent<MonsterBullet>().power);
    }
}
