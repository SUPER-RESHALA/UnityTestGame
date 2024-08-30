using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (timeBtwAttack <= 0)
        //{
            if (Input.GetKeyDown(KeyCode.F))
            {

                anim.SetTrigger("Attack");
                //Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
            }
        //}
        //else { 
        
        //timeBtwAttack-=Time.deltaTime;
        //}
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    public void onAttack() {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        //Collider2D[] enemies = Physics2D.OverlapCapsuleAll(attackPos.position, new Vector2(0.5f, 1), CapsuleDirection2D.Vertical, 0);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().TakeDamage(damage);

        }
    }
    void endAttack() {
     
        anim.SetBool("isAttack", false);
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("isAttack", true);
        }
        }
}
