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

    [SerializeField]
    float moveSpeed;

    private void Awake()
    {
        state = ZOMBIE_STATE.IDLE;
    }

    private void Start()
    {
        player = FindObjectOfType<HumanController>().gameObject.transform;
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
    }

    private void Attack()
    {
        Debug.Log("Attack");
        // TODO play attack animation and low down the player health
    }

    private void Dead()
    {
        //TODO: When Health reaches to zero then dead
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            state = ZOMBIE_STATE.ATTACKING;
        }
        //TODO: Colliding with bullet and lowing down the health, when reach zero then switch to dead state
    }
}
