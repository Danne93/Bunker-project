using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHealth = 2;
    public int health;
    public float speed = 3;
    public int value = 1;
    protected Rigidbody rb;
    public GameObject target;
    //[SerializeField] private float attackRange = 0.7f;
    private float nextAttack = 0f;
    private int attackDamage = 1;
    private float attackCooldown = 1.5f;
    protected GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Bunker");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        Vector3 towardsTarget = (target.transform.position - transform.position).normalized;
        towardsTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(towardsTarget);
        rb.velocity = towardsTarget * speed;
    }

    /*public void Attack()
    {
        float distanceToPlayer = (target.transform.position - transform.position).magnitude;
        if (distanceToPlayer < attackRange && Time.time > nextAttack)
        {
            Bunker bunker = target.GetComponent<Bunker>();
            bunker.Damage(attackDamage);
            nextAttack = Time.time + attackCooldown;
        }
    }*/

    public void Damage(int d)
    {
        health -= d;
        if (health <= 0)
        {
            Destroy(gameObject);
            gameManager.AddMoney(value);
        }
    }
    public void Heal(int h)
    {
        health += h;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Time.time > nextAttack)
        {
            Bunker bunker = collision.gameObject.GetComponent<Bunker>();
            bunker.Damage(attackDamage);
            nextAttack = Time.time + attackCooldown;
        }
    }
}
