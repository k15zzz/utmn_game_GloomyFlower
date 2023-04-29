using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool hasEntered;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !hasEntered)
        {
            Invoke("Die", 1f);
        }
    }

    void Die()
    {
        Destroy(gameObject);
        LevelManager.instance.Respawn();
    }
}
