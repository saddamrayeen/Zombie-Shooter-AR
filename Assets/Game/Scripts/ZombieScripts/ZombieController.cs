using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    enum ZOMBIE_STATE
    {
        IDLE,
        WALKING,
        ATTACKING,
        DEAD
    }

    ZOMBIE_STATE state;
    Transform player;

    [Header("Properties")]
    [SerializeField]
    float moveSpeed;
    Animator anim;

    [SerializeField]
    private int health = 250;

    private void Awake()
    {
        state = ZOMBIE_STATE.IDLE;
    }

    private void Start()
    {
        player = FindObjectOfType<HumanController>().gameObject.transform;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            case ZOMBIE_STATE.IDLE:
                state = ZOMBIE_STATE.WALKING;
                break;
            case ZOMBIE_STATE.WALKING:
                MovePlayer();
                break;
            case ZOMBIE_STATE.ATTACKING:

                Attack();
                break;
            case ZOMBIE_STATE.DEAD:
                Dead();
                break;
        }
    }

    private void MovePlayer()
    {
        var direction = player.transform.position - transform.position;
        direction = direction.normalized;

        transform.Translate(direction * moveSpeed * Time.deltaTime);
        transform.LookAt(new Vector3(player.position.x, 0, player.position.z));
    }

    private void Attack()
    {
        //TODO: Decrease player health
        anim.SetBool("ATTACK", true);
    }

    private void Dead()
    {
        anim.SetTrigger("DEAD");
        Destroy(gameObject,7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            state = ZOMBIE_STATE.ATTACKING;
        }
        if (other.gameObject.tag == "Bullet")
        {
            if (health >= 0)
            {
                var bulletDamage = other.GetComponent<Bullet>().bulletDamage;
                health -= bulletDamage;
            }
           else
            {
                state = ZOMBIE_STATE.DEAD;
            }
        }
    }
}
