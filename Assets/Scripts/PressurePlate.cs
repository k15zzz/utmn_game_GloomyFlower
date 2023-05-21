using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door; //Ссылка на объект

    private void OnTriggerEnter2D(Collider2D other) //При срабатывание тригера на объекте
    {
        if (other.CompareTag("Box") || other.CompareTag("Player")) //Проверка тип нажимающего объекта
        {
            VisibleDor(false); //Убираем объект
        }
    }

    private void OnTriggerExit2D(Collider2D other) //При срабатывание тригера на объекте
    {
        if (other.CompareTag("Box") || other.CompareTag("Player")) //Проверка тип нажимающего объекта
        {
            VisibleDor(true); //Показываем объект
        }
    }

    private void VisibleDor(bool set)
    {
        door.SetActive(set);
    }
}