using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int health;
    public float speed;
    public int damage;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    public Transform playerTransform;
    private PlayerController player;
    public Animator anim;
    private bool isAttacking = false; // Флаг для предотвращения повторных атак
    private bool isDead = false; // Флаг для проверки смерти

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerController>();
        normalSpeed = speed;
    }

    void Update()
    {
        if (isDead) return; // Остановка всех действий, если враг уже мертв

        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0 && !isDead)
        {
            Die();
        }

        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        float directionToPlayer = playerTransform.position.x - transform.position.x;
        Vector3 scale = transform.localScale;

        if (directionToPlayer > 0)
        {
            // Поворачиваем влево
            scale.x = Mathf.Abs(scale.x) * -1; // Делаем x отрицательным
        }
        else if (directionToPlayer < 0)
        {
            scale.x = Mathf.Abs(scale.x); // Делаем x положительным
        }
        transform.localScale = scale;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Не реагировать на урон, если враг уже мертв

        stopTime = startStopTime;
        health -= damage;
        anim.SetTrigger("TakeDamage");

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true; // Установить флаг смерти
        anim.SetTrigger("Sdeath");
        Destroy(gameObject, 1f); // Уничтожить объект после завершения анимации
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (isDead) return; // Не атаковать, если враг уже мертв

        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0 && !isAttacking)
            {
                Debug.Log("ATTTTTTTAAACK" + timeBtwAttack);
                anim.SetTrigger("SAttack");
                isAttacking = true; // Установка флага атаки
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    public void OnEnemyAttack()
    {
        player.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
        isAttacking = false; // Сброс флага после атаки
    }
}
