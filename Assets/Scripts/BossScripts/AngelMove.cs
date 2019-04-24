﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMove : MonoBehaviour
{
    public Transform mypos;
    public GameObject target;
    private GameObject player;
    private EnemyAvatar myself;

    public bool isActivated = false;

    //Used for situational specific triggers.
    public bool levelSpecificTrigger = false;


    public int storyProgActivate;
    public int curStoryProg;

    private Vector3 midPos;
    private Vector3 offsetY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        myself = GetComponent<EnemyAvatar>();
        target.transform.position = transform.position;

        offsetY = new Vector3(0, 1.5f, 0);
        midPos = transform.position + offsetY;
    }

    // Update is called once per frame
    void Update()
    {
        
        curStoryProg = OfflineGameManager.instance.storyProgress;
        if(OfflineGameManager.instance.storyProgress >= storyProgActivate && !isActivated){
            isActivated = true;
            GetComponent<AngelAttack>().isActivated = true;
            levelSpecificTrigger = false;
            //Debug.Log("attack activated.");
        }
        if(!isActivated){
            return;
        }
        if(player){
            target.transform.position = player.transform.position;
        }
    }
    
    void FixedUpdate(){
        if(myself.getDead()){
            //Debug.Log("Angel boss dead?.");

            //deactivates crown and wings
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(false);
            }

            if (isActivated){
                GetComponent<AngelAttack>().isActivated = false;
                isActivated = false;
            }
            return;
        }
        Vector3 updPos = mypos.position - midPos;
        myself.change = midPos;

        midPos = mypos.position;
        transform.position = midPos - offsetY;
    }
}
