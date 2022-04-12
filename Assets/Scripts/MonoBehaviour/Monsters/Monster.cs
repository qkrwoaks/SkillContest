using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public MonsterData monsterData;

    public string monsterName;
    public float hp;
    public float power;
    public float speed;
    public int score;

    public GameObject bulletPrefab;
    public GameObject boomPrefabs;

    public int gameLevel;
    public int stageStep;
    public float gameBalance;
    private SpriteRenderer spriteRenderer;
    private Color originColor;

    public void Init()
    {
        gameLevel = GameManager.instance.gameLevel;
        stageStep = GameManager.instance.stageStep;
        gameBalance = (gameLevel * 0.5f) + (stageStep * 0.5f);
        monsterName = monsterData.MonsterName;
        hp = (monsterData.Hp * gameBalance);
        power = monsterData.Power;
        speed = (monsterData.Speed + gameBalance);
        score = monsterData.Score;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    public void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<MonsterBullet>().power = power * 2;
            bullet.GetComponent<MonsterBullet>().speed = speed + 2f;
        }
    }

    public virtual void OnDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) OnDead();
        StopCoroutine(ChangeDamageSprite());
        StartCoroutine(ChangeDamageSprite());
    }

    public void OnDead()
    {
        GameManager.instance.AppendToScore(score);
        Instantiate(boomPrefabs, transform.position, transform.rotation);
        EffectSoundManager.instance.DestroyMonster();
        Destroy(gameObject);
    }

    public IEnumerator ChangeDamageSprite()
    {
        spriteRenderer.color = new Color(originColor.r, originColor.g, originColor.b, 0.4f);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = originColor;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet")) OnDamage(collision.GetComponent<PlayerBullet>().power);
        else if (collision.CompareTag("Border") && collision.name == "BottomBorder")
        {
            GameManager.instance.AppendToPain(0.05f);
            Destroy(gameObject);
        }
    }
}
