﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPaint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerAvatar>().updateTrail(2);
        }
    }
}
