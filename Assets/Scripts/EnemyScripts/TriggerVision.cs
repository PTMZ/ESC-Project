﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVision : MonoBehaviour
{

    public EnemyMovement enemyMovement;
    private bool inRange = false;
    void FixedUpdate(){
        if(inRange){
            if(!enemyMovement.checkBlocked()){
                Debug.Log("IN VISION");
                enemyMovement.changePatrol(false);
                enemyMovement.TriggerExclamationMarkMethod();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            //enemyMovement.changePatrol(false);
            enemyMovement.inVisionRange = true;
            if(!enemyMovement.checkBlocked()){
                Debug.Log("IN VISION");
                enemyMovement.changePatrol(false);
                enemyMovement.TriggerExclamationMarkMethod();
            }

            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            inRange = false;
        }
    }
}
