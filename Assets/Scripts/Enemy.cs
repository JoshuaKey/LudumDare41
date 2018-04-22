using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum SlimeType {
        Green,
        Red,
        Blue,
        Purple,
        Black,
        White,
        Gold,
    }
    [SerializeField] private AudioClip attackSound;

    private Health m_health;
    private LevelSystem m_levelSystem;
    private SpriteRenderer m_renderer;
    private Rigidbody2D m_rb;
    private Animator m_anim;

    public SlimeType m_slimeType;
    public float speed = 5.0f;
    public float damage;
    public float attackLength;
    public float attackTime;
    public float nextAttackTime;
    public bool isChasing;
    public bool isFacingRight;

    public Health health { get { return m_health; } }
    public LevelSystem levelSystem { get { return m_levelSystem; } }

	// Use this for initialization
	protected virtual void Awake () {
        m_health = GetComponent<Health>();
        m_levelSystem = GetComponent<LevelSystem>();
        m_renderer = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
	}

    public void Initialize(SlimeType type, int level) {
        m_slimeType = type;
        this.name = GetName(type) + "Slime";

        m_renderer.color = GetColor(type);

        float expWorth = 2 * level;
        float expMod = GetExpMod(type);
        m_levelSystem.Initialize(level, 0, expWorth * expMod);

        // Change health based on level
        m_health.Initialize(10 * level * expMod);

        // m_health.Initialize(10 * m_levelSystem.Level);
        //  damage = 5f + (m_levelSystem.Level - 1) * 4f;
        switch (type) {
            case SlimeType.Green:
                damage = 1 * level;
                attackLength = .75f;
                attackTime = 1.5f;
                speed = 3.0f;
                //damage
                //attackLength
                //attackTime
                //speed
                break;
            case SlimeType.Red:
                damage = 1 * level;
                attackLength = .6f;
                attackTime = .75f;
                speed = 5f;
                break;
            case SlimeType.Blue:
                damage = 3 * level;
                attackLength = 1.0f;
                attackTime = 1.25f;
                speed = 4.0f;
                break;
            case SlimeType.Purple:
                damage = 5 * level;
                attackLength = 3.0f;
                attackTime = 3.0f;
                speed = 1.0f;
                break;
            case SlimeType.Black:
                damage = 6 * level;
                attackLength = .8f;
                attackTime = .8f;
                speed = 4.0f;
                break;
            case SlimeType.White:
                damage = 4 * level;
                attackLength = 1.2f;
                attackTime = 1.2f;
                speed = 4.0f;
                break;
            case SlimeType.Gold:
                damage = 2 * level;
                attackLength = 1.0f;
                attackTime = 1.0f;
                speed = 4.0f;
                break;
        }
    }

    protected void Update() {
        Vector2 mvmt = Vector2.zero;
        if (Time.time > nextAttackTime) {
            Vector3 dist = PlayerController.Instance.transform.position - this.transform.position;
            if (dist.sqrMagnitude > 100) { isChasing = false; return; }
            isChasing = true;


            switch (m_slimeType) {
                case SlimeType.Green:
                case SlimeType.Red:
                case SlimeType.Blue:
                case SlimeType.Purple:
                case SlimeType.Black:
                case SlimeType.White:
                    mvmt.x = (dist.x > 0 ?  1 : -1);
                    break;
                case SlimeType.Gold:
                    mvmt.x = (dist.x > 0 ? -1 : 1);
                    break;
            }

            if (mvmt.x > 0) {
                isFacingRight = true;
                Vector3 scale = this.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                this.transform.localScale = scale;
            } else if (mvmt.x < 0) {
                isFacingRight = false;
                Vector3 scale = this.transform.localScale;
                scale.x = -Mathf.Abs(scale.x);
                this.transform.localScale = scale;
            }


            if (Mathf.Abs(dist.x) < attackLength && Mathf.Abs(dist.y) < 1f) {
                m_anim.SetTrigger("Attack");
                //AudioManager.Instance.PlaySound(attackSound);
                nextAttackTime = Time.time + attackTime;
            }
        }
        Vector2 vel = m_rb.velocity;
        vel.x = mvmt.x * speed;
        m_rb.velocity = vel;
    }

    public void Attack() {
        Vector2 end = this.transform.position;
        end.x += attackLength * (isFacingRight ? 1 : -1);
        // Delay Attack?
        var hit = Physics2D.Linecast(this.transform.position, end, 1 << Data.PLAYER_LAYER);
        if (hit) {
            PlayerController.Instance.health.TakeDamage(damage);
        }

        AudioManager.Instance.PlaySound(attackSound);
    }

    public static Color GetColor(SlimeType type) {
        switch (type) {
            case SlimeType.Green:
                return new Color(0, 1, 76 / 255f);
            case SlimeType.Red:
                return new Color(1, 0, 0);
            case SlimeType.Blue:
                return new Color(0, 160/255f, 1f);
            case SlimeType.Purple:
                return new Color(.5f, 0, 1.0f);
            case SlimeType.Black:
                float t = 86 / 255f;
                return new Color(t, t, t);
            case SlimeType.White:
                return Color.white;
            case SlimeType.Gold:
                return new Color(1, 215 / 255f, 0f);
        }
        return Color.grey;
    }

    public static string GetName(SlimeType type) {
        switch (type) {
            case SlimeType.Green:
                return "Green";
            case SlimeType.Red:
                return "Red";
            case SlimeType.Blue:
                return "Blue";
            case SlimeType.Purple:
                return "Purple";
            case SlimeType.Black:
                return "Black";
            case SlimeType.White:
                return "White";
            case SlimeType.Gold:
                return "Gold";
        }
        return "";
    }

    public static float GetExpMod(SlimeType type) {
        switch (type) {
            case SlimeType.Green:
                return 1.0f;
            case SlimeType.Red:
                return 1.3f;
            case SlimeType.Blue:
                return 1.25f;
            case SlimeType.Purple:
                return 1.5f;
            case SlimeType.Black:
                return 3f;
            case SlimeType.White:
                return 3f;
            case SlimeType.Gold:
                return 10f;
        }
        return 1.0f;
    }
}
