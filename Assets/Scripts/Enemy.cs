using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Объявляем публичные переменные
    public float speed = 5f; // Скорость врага
    public float jumpForce = 10f; // Сила прыжка врага
    public float jumpDistance = 1f; // Расстояние, на которое враг может прыгнуть
    public float jumpHeight = 1f; // Высота прыжка врага
    public LayerMask groundLayer; // Слой земли
    public Transform groundCheck; // Точка проверки земли
    public float playerCheckRadius = 5f; // Радиус проверки персонажа
    public float groundCheckRadius = 0.1f; // Радиус проверки земли

    // Объявляем приватные переменные
    private Rigidbody2D rb; // Компонент Rigidbody2D
    // private Animator animator; // Компонент Animator
    private bool facingRight = true; // Направление взгляда врага
    private bool isGrounded = false; // Находится ли враг на земле
    private bool isChasing = false; // Преследует ли враг персонажа
    private bool isJumping = false; // Производит ли враг прыжок
    private bool isBlocked = false; // Заблокирован ли враг препятствием
    private Transform player; // Трансформ персонажа
    private Vector2 jumpTarget; // Точка, куда должен прыгнуть враг

    // Вызывается при старте скрипта
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
        // animator = GetComponent<Animator>(); // Получаем компонент Animator
        player = GameObject.FindGameObjectWithTag("Player").transform; // Получаем трансформ персонажа
    }

    // Вызывается на каждом кадре
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // Проверяем, находится ли враг на земле
        // animator.SetBool("isGrounded", isGrounded); // Устанавливаем значение параметра isGrounded в компоненте Animator

        if (isChasing) // Если враг преследует персонажа
        {
            if (player.position.x < transform.position.x && facingRight) // Если персонаж находится слева от врага и враг смотрит вправо
            {
                Flip(); // Поворачиваем врага
            }
            else if (player.position.x > transform.position.x && !facingRight) // Если персонаж находится справа от врага и враг смотрит влево
            {
                Flip(); // Поворачиваем врага
            }

            if (!isBlocked) // Если враг не заблокирован препятствием
            {
                float distance = Vector2.Distance(transform.position, player.position); // Вычисляем расстояние между врагом и персонажем
                if (distance > jumpDistance) // Если расстояние больше, чем враг может прыгнуть
                {
                    rb.velocity = new Vector2((player.position.x - transform.position.x) * speed, rb.velocity.y); // Двигаем врага в сторону персонажа
                }
                else if (!isJumping) // Если расстояние меньше, чем враг может прыгнуть, и враг не производит прыжок
                {
                    Jump(); // Производим прыжок
                }
            }
        }
    }

    // Вызывается на каждом кадре

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Получаем трансформ персонажа
        
        if (Physics2D.OverlapCircle(player.position, playerCheckRadius, groundLayer)) // Если персонаж находится в зоне действия триггера врага
        {
            isChasing = true; // Враг начинает преследование
        }
        else
        {
            isChasing = false; // Враг прекращает преследование
        }
        
        if (isJumping) // Если враг производит прыжок
        {
            float distance = Vector2.Distance(transform.position, jumpTarget); // Вычисляем расстояние между врагом и точкой, куда он должен прыгнуть
            if (distance < 0.1f) // Если расстояние меньше 0.1f
            {
                isJumping = false; // Враг заканчивает прыжок
            }
        }
    }

    // Производит прыжок в заданную точку
    private void Jump()
    {
        Vector2 target = player.position; // Получаем позицию персонажа
        target.y += jumpHeight; // Увеличиваем высоту точки на jumpHeight
        jumpTarget = target; // Сохраняем точку, куда должен прыгнуть враг

        Vector2 direction = (target - (Vector2)transform.position).normalized; // Вычисляем направление прыжка
        float distance = Vector2.Distance(transform.position, target); // Вычисляем расстояние до точки прыжка

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, groundLayer); // Проверяем, есть ли на пути препятствие
        if (hit.collider != false) // Если препятствие есть
        {
            if (hit.distance < jumpDistance) // Если расстояние до препятствия меньше, чем враг может прыгнуть
            {
                isJumping = true; // Враг начинает прыжок
                rb.velocity = new Vector2(direction.x * speed, jumpForce); // Применяем силу прыжка
            }
            else // Если расстояние до препятствия больше, чем враг может прыгнуть
            {
                isBlocked = true; // Враг заблокирован препятствием
                StartCoroutine(Unblock()); // Запускаем корутину Unblock
            }
        }
        else // Если препятствия нет
        {
            isJumping = true; // Враг начинает прыжок
            rb.velocity = new Vector2(direction.x * speed, jumpForce); // Применяем силу прыжка
        }
    }

    // Корутина, которая разблокирует врага после некоторого времени
    private IEnumerator Unblock()
    {
        yield return new WaitForSeconds(0.5f); // Ждем 0.5 секунды
        isBlocked = false; // Разблокируем врага
    }

    // Поворачивает врага в противоположную сторону
    private void Flip()
    {
        facingRight = !facingRight; // Изменяем направление взгляда врага
        Vector3 scale = transform.localScale; // Получаем масштаб врага
        scale.x *= -1; // Изменяем масштаб по оси X
        transform.localScale = scale; // Применяем новый масштаб
    }
}