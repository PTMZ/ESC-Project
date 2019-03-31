﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMelee : MonoBehaviour
{

    public bool active = false;
    private float duration = 0.3f;

    void OnTriggerEnter2D(Collider2D other){
        
        if (other.CompareTag("Enemy") && active){
            active = false;
            other.gameObject.GetComponent<EnemyAvatar>().getHit(5);
        }
    }

    public void startMelee(){
        active = true;
        StartCoroutine(endMelee());
    }

    private IEnumerator endMelee(){
        yield return new WaitForSeconds(duration);
        active = false;
    }
}
