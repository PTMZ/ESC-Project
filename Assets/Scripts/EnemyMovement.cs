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
    public WeaponAnim weaponAnim;

    private float cooldownTimeStamp;
    public float cooldown = 0.5f;
    public float hitRadius = 3;
    public float meleeDmg = 10;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, -1.7f, 0);
        player = GameObject.FindWithTag("Player");
        myself = GetComponent<EnemyAvatar>();
        pAvatar = player.GetComponent<PlayerAvatar>();
        weaponAnim = GetComponent<WeaponAnim>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(myself.getDead()){
            return;
        }
        Vector3 updPos = mypos.position - transform.position;
        myself.change = updPos;
        

        transform.position = mypos.position;
        if(player != null){
            target.transform.position = player.transform.position;
            //target.transform.position = transform.position;  // Comment above line and uncomment this line to make enemy stationary
        }

        Vector3 midPos = transform.position + new Vector3(0, 1, 0);
        if (pAvatar != null && (midPos - player.transform.position).magnitude <= hitRadius){

            if(Time.time > cooldownTimeStamp){
                cooldownTimeStamp = Time.time + cooldown;
                pAvatar.getHit(meleeDmg);   // Comment this line out to stop enemy from attacking
                Debug.Log("ENEMY ATTACK");
                // Play attack animation here
                weaponAnim.weaponAnimator.SetBool("attack", true);
                //
            }
        }
    }
}
