﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletScript : MonoBehaviourPunCallbacks
{

    public float impactRadius = 2;
    public float impactPower = 5;
    private float DeathTime;
    private float bulletDmg;
    public bool isOnline = true;
    public GameObject explosion;

    private OfflineGameManager offlineGM;

    // Start is called before the first frame update
    void Start()
    {
        offlineGM = FindObjectOfType<OfflineGameManager>();
        DeathTime = GetComponent<AttackStats>().deathTime;
        bulletDmg = GetComponent<AttackStats>().damage;
        /* 
        if(PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient){
            StartCoroutine(NetworkDestroyEnum(DeathTime));
        }
        else{
            Destroy(gameObject, DeathTime);
        }
        */
        Destroy(gameObject, DeathTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        /*
        if(col.gameObject.GetComponent<PlayerMovement>() != null){
            return;
        }
        */
        if(col.gameObject.GetComponent<PlayerAvatar>() != null){
            //Debug.Log("I am hit, my name is " + col.gameObject.name);
            if(PhotonNetwork.IsConnected){
                if(GetComponent<PhotonView>().IsMine){
                    col.gameObject.GetComponent<PlayerAvatar>().reduceHealthRPC(1);
                }
            }
            else{
                col.gameObject.GetComponent<PlayerAvatar>().getHit(bulletDmg);
            }
        }
        if(col.gameObject.GetComponent<EnemyAvatar>() != null){
            //Debug.Log("Enemy hit is " + col.gameObject.name);
            col.gameObject.GetComponent<EnemyAvatar>().getHit(bulletDmg);
        }
        if (col.gameObject.GetComponent<Destroyable>() != null)
        {
            //Debug.Log("Enemy hit is " + col.gameObject.name);
            col.gameObject.GetComponent<Destroyable>().getHit(bulletDmg);
        }
        if (col.gameObject.GetComponent<OnlineDestroyable>() != null)
        {
            //Debug.Log("Enemy hit is " + col.gameObject.name);
            if(GetComponent<PhotonView>().IsMine){
                col.gameObject.GetComponent<OnlineDestroyable>().reduceHealthRPC(1);
            }
        }
        if (col.gameObject.GetComponent<DestroyableWall>() != null)
        {
            //Debug.Log("Enemy hit is " + col.gameObject.name);
            col.gameObject.GetComponent<DestroyableWall>().getHit(bulletDmg);
        }
        if (col.gameObject.GetComponent<ShootBomb>() != null)
        {
            //Debug.Log("Enemy hit is " + col.gameObject.name);
            col.gameObject.GetComponent<ShootBomb>().getHit(bulletDmg);
        }
        //Debug.Log("OnCollisionEnter2D");
        //Put Sound here
        Vector2 hitPoint = col.GetContact(0).point;
        Rigidbody2D other = col.otherRigidbody;
        if(!isOnline){
            AddExplosionForce(other, impactPower, new Vector3(hitPoint.x, hitPoint.y, 0), impactRadius);
        }
        //Destroy(gameObject);
        GameObject exp = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(exp, 0.3f);


        if(isOnline){
            if(GetComponent<PhotonView>().IsMine){
                PhotonNetwork.Destroy(gameObject);
            }
        }
        else{
            Destroy(gameObject);
        }

    }

    public static void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius){
            var dir = (body.transform.position - expPosition);
            float calc = 1 - (dir.magnitude / expRadius);
            if (calc <= 0) {
                    calc = 0;		
            }

            body.AddForce (dir.normalized * expForce * calc);
    }


    private IEnumerator NetworkDestroyEnum(float DeathTime)
    {
        Debug.Log("Destroy on network");
        yield return new WaitForSeconds(DeathTime);
        PhotonNetwork.Destroy(gameObject);
       
    }

}
