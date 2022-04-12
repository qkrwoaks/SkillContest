using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreItem : Item
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            UIManager.instance.ChangeToCurrentItem(new Color(0.6f, 0, 1), "더블 스코어");
            GameManager.instance.OnDoubleScore();
            Destroy(gameObject);
        }
    }
}
