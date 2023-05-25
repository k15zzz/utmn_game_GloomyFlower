using UnityEngine;

public class ExitGamePlate : MonoBehaviour
{
    private bool playerInPlate;
    
    void Update()
    {
        if (playerInPlate && Input.GetKeyDown(KeyCode.E))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //При срабатывание тригера на объекте
    {
        playerInPlate = other.CompareTag("Player");
    }

    private void OnTriggerExit2D(Collider2D other) //При срабатывание тригера на объекте
    {
        playerInPlate = other.CompareTag("Player");
    }
}