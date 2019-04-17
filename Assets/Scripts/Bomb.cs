﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    public GameObject bomb;

    //public float explosionDelay = 1f;
    public float bombDamage = 100;
    public float explosionRate = 1f;
    public float explosionMaxSize = 1f;
    public float explosionSpeed = 10f;
    public float explosionForce = 10f;
    public float currentRadius = 0.1f;

    public Animator animator;

    bool exploded = false;
    BoxCollider2D explosionRadius;
    private OfflineGameManager offlineGM;

    void Start()
    {
        explosionRadius = gameObject.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        offlineGM = OfflineGameManager.instance;

    }

    /*
    void OnTriggerEnter2D(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    */

    /*
    void Update()
    {
        explosionDelay -= Time.deltaTime;
        if (explosionDelay < 0)
        {
            exploded = true;
        }
    }
    */

    void FixedUpdate()
    {
        if (exploded == true)
        {
            if (currentRadius < explosionMaxSize)
            {
                currentRadius += explosionRate;
                //offlineGM.loadScene();
            }
            else
            {
                Destroy(gameObject, 0.2f);
                //SceneManager.GetActiveScene();
            }
            explosionRadius.edgeRadius = currentRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            exploded = true;
            animator.Play("bomb_explosion", -1);
        }

        
        if (collision.CompareTag("Enemy"))
        {
            //exploded = false;
            //Debug.Log("HIT ENEMY");
            //animator.Play("bomb_explosion", -1);
        }
        
        if (collision.gameObject.GetComponent<PlayerAvatar>() != null)
        {
            collision.gameObject.GetComponent<PlayerAvatar>().getHit(bombDamage);

        }

        if (collision.gameObject.GetComponent<EnemyAvatar>() != null)
        {
            collision.gameObject.GetComponent<EnemyAvatar>().getHit(25);
            animator.Play("bomb_explosion", -1);
            //offlineGM.destroyBomb(gameObject);
            Destroy(gameObject, 0.2f);

        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && exploded == true)
        {
            Vector2 target = collision.gameObject.transform.position;
            Vector2 bomb = gameObject.transform.position;
            Vector2 direction1 =  explosionForce * (target - bomb);
            //Vector2 direction2 = - 0.5f * explosionForce * (target - bomb);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction1);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction2);
            //new WaitForSecondsRealtime(1);
            //Vector2 direction2 = -0.5f * explosionForce * (collision.gameObject.transform.position - gameObject.transform.position);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction2);
        }

    }


}
