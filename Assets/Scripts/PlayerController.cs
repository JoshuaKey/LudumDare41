using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Spawn")]
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] public Color initialColor;

    [Header("Mvmt")]
    public float speed = 5.0f;
    public bool isFacingRight = true;
    public bool canClimb;

    [Header("Attack")]
    public float damage = 5f;
    public float attackLength = 1.0f;
    public float attackTime = .5f;
    public float nextAttackTime;
    public bool canAttack = true;

    private Rigidbody2D m_rb;
    private Collider2D m_collider;
    private SpriteRenderer m_renderer;
    private Animator m_anim;

    private LevelSystem m_levelSystem;
    private Health m_health;

    public Health health { get { return m_health; } }
    public LevelSystem level { get { return m_levelSystem; } }

    public static PlayerController Instance;

	// Use this for initialization
	void Awake () {
        Instance = this;
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_renderer = GetComponent<SpriteRenderer>();
        m_anim = GetComponent<Animator>();
        m_renderer.color = initialColor;

        m_health = GetComponent<Health>();
        m_levelSystem = GetComponent<LevelSystem>();

        m_health.OnDeath += OnDeath;
        m_levelSystem.OnLevel += OnLevel;
        // Add Health and EXP change for UI
	}

    private void Start() {
        OnLevel();
    }

    // Update is called once per frame
    void Update () {
        ChangeColor();
        canClimb = m_rb.IsTouchingLayers(1 << Data.LADDER_LAYER);

        // Movement
        {
            // Get Input
            Vector2 mvmt = Vector2.zero;
            mvmt.x = Input.GetAxisRaw("Horizontal");
            if (canClimb) {
                print("Here");
                mvmt.y = Input.GetAxisRaw("Vertical");
                
                if(mvmt.y > 0) {
                    m_rb.gravityScale = 0f;
                }
                if(mvmt.y < 0) {
                    m_rb.gravityScale = 1f;
                }
            } else {
                m_rb.gravityScale = 1f;
            }

            // Change Facing Direction
            if(mvmt.x > 0) {
                isFacingRight = true;
                Vector3 scale = this.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                this.transform.localScale = scale;
            } else if(mvmt.x < 0) {
                isFacingRight = false;
                Vector3 scale = this.transform.localScale;
                scale.x= -Mathf.Abs(scale.x);
                this.transform.localScale = scale;
            } 

            // Move Player (In Future should account for Rigidbody)
            mvmt = mvmt.normalized;

            //Vector2 pos = transform.position;
            //pos += mvmt * speed * Time.deltaTime;
            //transform.position = pos;
            Vector2 vel = m_rb.velocity;

            //Vector3 end = this.transform.position;
            //end.x += this.transform.localScale.x;
            //var hit = Physics2D.Linecast(this.transform.position, end, 1 << Data.GROUND_LAYER);
            //if (!hit) {
            //    vel.x = mvmt.x * speed;
            //}

            if (m_rb.gravityScale == 0f) {
                vel.y = mvmt.y * speed;
            }

            vel.x = mvmt.x * speed;

            m_rb.velocity = vel;

            //m_rb.AddForce(mvmt - m_rb.velocity, ForceMode2D.Force);
        }

        // Attack 
        {
            if (canAttack) {
                if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextAttackTime) {
                    Attack();
                    m_anim.SetTrigger("Attack");
                }
            }
        }
    }

    public void Attack() {
        Vector2 end = this.transform.position;
        end.x += (isFacingRight ? 1 : -1) * attackLength;

        var hit = Physics2D.LinecastAll(this.transform.position, end, 1 << Data.ENEMY_LAYER);
        foreach (var obj in hit) {
            Health health = obj.collider.GetComponent<Health>();

            if (health != null) {
                health.TakeDamage(damage);
                if (health.IsDead()) {
                    var level = health.LevelSys;
                    if(level != null) {
                        m_levelSystem.GainExp(level.ExpWorth);
                        //print("Gained " + level.ExpWorth);
                    }
                }
            }

            //print("Hit " + obj.collider.name);
        }

        nextAttackTime = Time.time + attackTime;
    }

    private void ChangeColor() {
        Color c = m_renderer.color;

        float h, s, b;
        Color.RGBToHSV(c, out h, out s, out b);
        h += Time.deltaTime * .01f;
        if(h > 1f) {
            h -= 1f;
        }
        c = Color.HSVToRGB(h, s, b);

        m_renderer.color = c;
    }

    private void OnLevel() {
        Data.playerLevel = m_levelSystem.Level;
        m_levelSystem.SetMaxXP(m_levelSystem.Level * 10f);

        // Change Attack and Health
        m_health.Initialize(10 * m_levelSystem.Level);
        damage = 5f + (m_levelSystem.Level - 1) * 4f;
    }

    private void OnDeath() {
        Data.playerDeath++;
        this.gameObject.SetActive(false);

        m_health.Initialize(m_health.MaxHP);
        this.transform.position = spawnPosition;

        m_renderer.color = initialColor;
        this.gameObject.SetActive(true);
    }

}
