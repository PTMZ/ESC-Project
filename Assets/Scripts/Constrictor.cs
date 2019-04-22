﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrictor : MonoBehaviour
{
    public float speed;//speed of the game object
    public string direction; //type "right", "left", "up", or "down" only

    Vector3 moveRight = new Vector3(100, 0, 0);
    Vector3 moveLeft = new Vector3(-100, 0, 0);
    Vector3 moveUp = new Vector3(0, 100, 0);
    Vector3 moveDown = new Vector3(0, -100, 0);

    private bool isHit = false;
    private float cooldownTimeStamp;
    public float cooldown = 0.1f;

    private float dmg = 9999;
    private PlayerAvatar pAvatar;

    // Start is called before the first frame update
    void Start(){
        pAvatar = GameObject.FindWithTag("Player").GetComponent<PlayerAvatar>();
        cooldownTimeStamp = Time.time;
        AudioManager.instance.PlayLoopButMustStop("Constrictor");
    }

    void FixedUpdate()
    {
        if (direction == "right")
        {
            ConstrictorAction(moveRight);
        }
        else if(direction == "left")
        {
            ConstrictorAction(moveLeft);
        }
        else if(direction == "up")
        {
            ConstrictorAction(moveUp);
        }
        else if(direction == "down")
        {
            ConstrictorAction(moveDown);
        }

    }

    void ConstrictorAction(Vector3 dir)
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + dir, Time.deltaTime * speed);
        if (isHit && Time.time > cooldownTimeStamp)
        {
            cooldownTimeStamp = Time.time + cooldown;
            pAvatar.getHit(dmg);
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
            isHit = true;
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player"))
            isHit = false;
    }
}


