using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    GameObject player;
    public Animator enemyAnimator;
    float damage = 10f;
    public float health = 100f;
    public GameManager gameManager;
    public Slider healthSlider;

    private bool playerInReach;
    private float attackDelayTimer;

    public float attackAnimStartDelay;
    public float delayBetweenAttack;
    AudioSource audioSource;
    public AudioClip[] zombieSounds;

    public float points = 20f;
    


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthSlider.maxValue = health;
        healthSlider.value = health;
        audioSource = GetComponent<AudioSource>();
    }


    public void Hit(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if(health<=0)
        {
           
            enemyAnimator.SetTrigger("isDead");
            gameManager.enimiesAlive--;

            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<EnemyManager>());
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(gameObject,10f);
        }
    }

    
    
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = zombieSounds[Random.Range(0, zombieSounds.Length)];
            audioSource.Play();
        }


        healthSlider.transform.LookAt(player.transform);

        GetComponent<NavMeshAgent>().destination = player.transform.position;

        if(GetComponent<NavMeshAgent>().velocity.magnitude>1f)
        {
            enemyAnimator.SetBool("isRunning", true);
        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInReach = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(playerInReach)
        {
            attackDelayTimer += Time.deltaTime;
        }

        if(attackDelayTimer>=delayBetweenAttack-attackAnimStartDelay && attackDelayTimer<=delayBetweenAttack && playerInReach)
        {
            enemyAnimator.SetTrigger("isAttacking");
        }

        if(attackDelayTimer>=delayBetweenAttack && playerInReach)
        {
            player.GetComponent<PlayerManager>().Hit(damage);
            attackDelayTimer = 0f;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject==player)
        {
            playerInReach = false;
            attackDelayTimer = 0;
        }
    }

}
