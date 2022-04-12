using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp;
    public float speed;
    public int bulletStep;
    public float additionalSpeed;
    public float maxHp;

    public GameObject[] bulletPrefabs;

    public bool isGodMode = false;
    public bool isItemGodMode = false;
    public bool isCheatGodMode = false;

    private bool[] onBorder = new bool[4] { false, false, false, false };

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Color originColor;

    public static Player instance;

    private void Awake()
    {
        if (instance == true) Destroy(gameObject);
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Fire());
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Init()
    {
        if (GameManager.instance.gameLevel == 2) hp = 3;
        else hp = 10;
        maxHp = hp;
        originColor = spriteRenderer.color;
        UIManager.instance.ChangeToHpSlider(1f);
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h == -1 && onBorder[2] || h == 1 && onBorder[3]) h = 0;
        animator.SetInteger("Input", (int)h);
        float v = Input.GetAxisRaw("Vertical");
        if (v == -1 && onBorder[1] || v == 1 && onBorder[0]) v = 0;
        Vector3 nextPos = new Vector3(h, v, 0);
        Vector3 curPos = transform.position;
        transform.position = curPos + nextPos * speed * Time.deltaTime;
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                EffectSoundManager.instance.FireBulletClip();
                Instantiate(bulletPrefabs[bulletStep], new Vector2(transform.position.x, transform.position.y + 0.5f), transform.rotation);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) GameManager.instance.StartCoroutine(GameManager.instance.GameOver());
        UIManager.instance.ChangeToHpSlider(hp / maxHp);
        EffectSoundManager.instance.PlayOnDamageToPlayerClip();
        StartCoroutine(ChangeDamageSprite());
        StartCoroutine(GodMode());
    }

    public void ItemGodMode()
    {
        StopCoroutine(ChangeDamageSprite());
        StopCoroutine(GodMode());
        isGodMode = false;
        StopCoroutine(OnItemGodMode());
        StartCoroutine(OnItemGodMode());
    }

    public IEnumerator GodMode()
    {
        isGodMode = true;
        yield return new WaitForSeconds(1.5f);
        isGodMode = false;
    }

    public IEnumerator OnItemGodMode()
    {
        isItemGodMode = true;
        StopCoroutine(ChangeItemGodSprite());
        StartCoroutine(ChangeItemGodSprite());
        yield return new WaitForSeconds(3f);
        isItemGodMode = false;
    }

    public IEnumerator ChangeDamageSprite()
    {
        spriteRenderer.color = new Color(originColor.r, originColor.g, originColor.b, 0.4f);
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.color = originColor;
    }

    public IEnumerator ChangeItemGodSprite()
    {
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(2.5f);
        spriteRenderer.color = originColor;
    }

    public void UpgradeBullet()
    {
        if (bulletStep == 4) return;
        bulletStep++;
    }

    public void UpgradeSpeed()
    {
        if (additionalSpeed == 2) return;
        additionalSpeed++;
    }

    public void Recovery()
    {
        hp += 1;
        if (hp >= maxHp) hp = maxHp;
        UIManager.instance.ChangeToHpSlider(hp / maxHp);
    }

    public void ChangeHP(float value)
    {
        Time.timeScale = 1;
        hp = value;
        if (hp >= maxHp) hp = maxHp;
        UIManager.instance.ChangeToHpSlider(hp / maxHp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border") && collision.name == "TopBorder") onBorder[0] = true;
        else if (collision.CompareTag("Border") && collision.name == "BottomBorder") onBorder[1] = true;
        else if (collision.CompareTag("Border") && collision.name == "LeftBorder") onBorder[2] = true;
        else if (collision.CompareTag("Border") && collision.name == "RightBorder") onBorder[3] = true;
        else if (collision.CompareTag("Monster") && isGodMode == false && isItemGodMode == false && isCheatGodMode == false) OnDamage(collision.GetComponent<Monster>().power);
        else if (collision.CompareTag("MonsterBullet") && isGodMode == false && isItemGodMode == false && isCheatGodMode == false) OnDamage(collision.GetComponent<MonsterBullet>().power);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Border") && collision.name == "TopBorder") onBorder[0] = false;
        else if (collision.CompareTag("Border") && collision.name == "BottomBorder") onBorder[1] = false;
        else if (collision.CompareTag("Border") && collision.name == "LeftBorder") onBorder[2] = false;
        else if (collision.CompareTag("Border") && collision.name == "RightBorder") onBorder[3] = false;
    }
}
