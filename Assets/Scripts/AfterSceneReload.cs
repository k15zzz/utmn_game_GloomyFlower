using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterSceneReload : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { 
        PlayerControll player = FindObjectOfType<PlayerControll>();
        player.transform.position = SpawnPoint.spawnPoint.position;
    }
}
