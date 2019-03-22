﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform mypos;
    public GameObject target;
    private GameObject player;
    private Vector3 offset;
    private EnemyAvatar myself;
    private PlayerAvatar pAvatar;

    private float cooldownTimeStamp;
    public float cooldown = 0.5f;
    public float hitRadius = 2;
    public float meleeDmg = 10;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, -1.7f, 0);
        player = GameObject.FindWithTag("Player");
        myself = GetComponent<EnemyAvatar>();
        pAvatar = player.GetComponent<PlayerAvatar>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(myself.getDead()){
            return;
        }
        transform.position = mypos.position;
        if(player != null){
            target.transform.position = player.transform.position;
        }

        if(pAvatar != null && (transform.position - player.transform.position).magnitude <= hitRadius){

            if(Time.time > cooldownTimeStamp){
                cooldownTimeStamp = Time.time + cooldown;
                pAvatar.getHit(meleeDmg);
                Debug.Log("ENEMY ATTACK");
                // Play attack animation here
                //
            }
        }
    }
}
