using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public LevelManager levelManager;
    
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            levelManager.Respawn();
        }
    }
}
