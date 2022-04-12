using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainRecoveryItem : Item
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            UIManager.instance.ChangeToCurrentItem(new Color(0, 1, 0), "���� ȸ��");
            GameManager.instance.PainRecovery();
            Destroy(gameObject);
        }
    }
}
