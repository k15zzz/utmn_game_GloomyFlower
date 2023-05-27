using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LevelManager levelManager;
    
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            levelManager.currentCheckpoint = gameObject;
            SpawnPoint.spawnPoint.position = levelManager.currentCheckpoint.transform.position;
        }
    }
}
