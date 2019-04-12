﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{
    static public OnlineGameManager Instance;
    public float curCooldown;

    [SerializeField]
    private PlayerAvatar playerPrefab;

    [SerializeField]
    private GameObject[] bulletPrefabs;

    [SerializeField]
    private int curAttack;

    void Awake(){
        if(!PhotonNetwork.IsConnected){
            SceneManager.LoadScene("TitleScreen");
            return;
        }
    }

    void Start(){
        Instance = this;

        if (!PhotonNetwork.IsConnected) {
            SceneManager.LoadScene("PvPLobby");
            return;
        }

        if (playerPrefab == null) { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.
            Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else {
            if (PlayerManager.LocalPlayerInstance==null) {
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
            }else {
                // Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }
        this.curAttack = 0;
        this.curCooldown = bulletPrefabs[curAttack].GetComponent<AttackStats>().cooldown;
    }

    public override void OnPlayerEnteredRoom( Player other  ) {
        Debug.Log( "OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting
        if ( PhotonNetwork.IsMasterClient )
        {
            Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom( Player other ) {
        Debug.Log( "OnPlayerLeftRoom() " + other.NickName ); // seen when other disconnects

        if ( PhotonNetwork.IsMasterClient )
        {
            Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
            LoadArena(); 
        }
    }

    public override void OnLeftRoom() {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }
    public void QuitApplication() {
        Application.Quit();
    }

    void LoadArena() {
        if ( !PhotonNetwork.IsMasterClient ) {
            Debug.LogError( "PhotonNetwork : Trying to Load a level but we are not the master Client" );
        }
        Debug.LogFormat( "PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount );
        //PhotonNetwork.LoadLevel("MultiPlayer");
    }
    

    public static void SpawnBullet(Vector3 spawnPos, Quaternion rotation, Vector3 bulletVector){
        GameObject bulletInstance = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "OnlineBullet"), spawnPos, rotation);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVector * bulletInstance.GetComponent<AttackStats>().bulletSpeed;
        //Destroy(bulletInstance,DeathTime);
        //StartCoroutine(bulletInstance.GetComponent<BulletScript>().NetworkDestroyEnum(DeathTime));
    }
    /*
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }
    */

    
}
