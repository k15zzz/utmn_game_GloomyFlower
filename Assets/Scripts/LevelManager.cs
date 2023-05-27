using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;
    public PlayerControll player;

    void Start()
    {
        player = FindObjectOfType<PlayerControll>();
    }

    public void Respawn()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
}
