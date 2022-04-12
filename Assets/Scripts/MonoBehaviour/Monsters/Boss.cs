using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    public string[] bossAttackPattenName = { "NormalBulletPatten", "MonsterSpawnPatten" };

    public float maxHp;
    public int miniCount;
    public bool isMini;
    public bool isSecondBoss;

    public GameObject[] monsterPrefabs;
    public GameObject miniBossPrefab;
    public GameObject udoBulletPrefab;

    private GameObject bullet;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Init();
        if (isMini) InitMini();
        else
        {
            animator.SetTrigger("Move");
            UIManager.instance.BossSpawn(monsterName);
        }
        if (isSecondBoss) monsterName = "º¯Á¾ " + monsterName;
        maxHp = hp;
        StartCoroutine(BossAttackPatten());
    }

    public void InitMini()
    {
        hp *= 0.5f;
        speed *= 0.5f;
        score /= 2;
        animator.SetInteger("Boonyear", miniCount);
        UIManager.instance.MiniBossSpawn(miniCount, monsterName);
    }

    public override void OnDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) Destroy(gameObject);
        if (isMini) UIManager.instance.ChangeToMiniBossSlider(maxHp, hp, miniCount);
        else UIManager.instance.ChangeToBossSlider(maxHp, hp);
        StopCoroutine(ChangeDamageSprite());
        StartCoroutine(ChangeDamageSprite());
    }

    private void OnDestroy()
    {
        GameManager.instance.AppendToScore(score);
        if (isMini)
        {
            UIManager.instance.MiniBossDead(miniCount);
            GameManager.instance.BossClear(miniCount);
            Destroy(gameObject);
        }
        else
        {
            for (int i = 0; i <= 1; i++)
            {
                GameObject miniBoss = Instantiate(miniBossPrefab, transform.position, transform.rotation);
                miniBoss.GetComponent<Boss>().isMini = true;
                miniBoss.GetComponent<Boss>().miniCount = i;
            }
            Destroy(gameObject);
        }
    }

    public IEnumerator BossAttackPatten()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            StartCoroutine(bossAttackPattenName[Random.Range(0, bossAttackPattenName.Length)]);
        }
    }


    public IEnumerator NormalBulletPatten()
    {
        for (int j = 0; j <= gameLevel; j++)
        {
            for (int i = 0; i < 360; i += 18)
            {
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<MonsterBullet>().power = power;
                bullet.GetComponent<MonsterBullet>().speed = speed + 2f;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 9; i < 369; i += 18)
            {
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<MonsterBullet>().power = power;
                bullet.GetComponent<MonsterBullet>().speed = speed + 2f;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator MonsterSpawnPatten()
    {
        if (isMini) Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], transform.position, transform.rotation);
        else for (int i = -2; i <= 2; i += 2) Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], new Vector2(i, transform.position.y - 1f), transform.rotation);
        yield return new WaitForSeconds(0.1f);
    }
}
