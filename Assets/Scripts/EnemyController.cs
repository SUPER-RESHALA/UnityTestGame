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

    // Start is called before the first frame update
    private void Start()
    {    
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerController>();
        normalSpeed = speed;
        //Vector3 Scaler =transform.localScale;
        //Scaler.x *= -1;
        //transform.localScale = Scaler;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            anim.SetTrigger("Sdeath");
            //Destroy(gameObject);
            Destroy(gameObject, 1f);
        }
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
        float directionToPlayer = playerTransform.position.x - transform.position.x;

        // Получаем текущий масштаб врага
        Vector3 scale = transform.localScale;

        if (directionToPlayer > 0)
        {
            // Поворачиваем влево
            scale.x = Mathf.Abs(scale.x) * -1; // Делаем x отрицательным
        }
        else if (directionToPlayer < 0)
        {
            // Поворачиваем вправо
            scale.x = Mathf.Abs(scale.x); // Делаем x положительным
        }

        // Применяем новый масштаб
        transform.localScale = scale;


    }

    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
        anim.SetTrigger("TakeDamage");

    }

    public void OnTriggerStay2D(Collider2D other)
    {
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