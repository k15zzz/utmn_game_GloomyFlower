using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static Transform spawnPoint;

    void Start()
    {
        // Проверяем, есть ли уже сохраненная позиция спавна
        if (spawnPoint == null)
        {
            // Если нет, то создаем новую позицию спавна
            spawnPoint = new GameObject("SpawnPoint").transform;
            spawnPoint.position = new Vector3(-13.47f, -5.35f, 0);
        }
        else
        {
            // Если есть, то используем сохраненную позицию спавна
            transform.position = spawnPoint.position;
        }
    }
}
