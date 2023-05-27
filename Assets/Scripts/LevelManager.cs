using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //SceneManager.LoadScene("MainScene");
        player.transform.position = SpawnPoint.spawnPoint.position;
    }
}
