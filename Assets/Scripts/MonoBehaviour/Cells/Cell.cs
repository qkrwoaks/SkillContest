using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour
{
    public float hp;
    public float speed;

    SpriteRenderer spriteRenderer;
    Color originColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    public void OnDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) OnDead();
        StartCoroutine(ChangeDamageSprite());
    }

    public IEnumerator ChangeDamageSprite()
    {
        spriteRenderer.color = new Color(originColor.r, originColor.g, originColor.b, 0.4f);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = originColor;
    }


    public virtual void OnDead()
    {
        EffectSoundManager.instance.PlayOnDamageToCellClip();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet")) OnDamage(collision.GetComponent<PlayerBullet>().power);
        else if (collision.CompareTag("Border") && collision.name == "BottomBorder") Destroy(gameObject);
        else if (collision.CompareTag("Player")) OnDead();
    }
}
