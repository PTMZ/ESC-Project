﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PvPButton : MonoBehaviour
{
    public void nextScene()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        SceneManager.LoadScene("PvPLobby");
    }
}
