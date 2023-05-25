using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Объявляем публичные переменные
    public float speed = 5f; // Скорость врага
    public LayerMask groundLayer; // Слой земли
    public Transform groundCheck; // Точка проверки земли
    public float playerCheckRadius = 5f; // Радиус проверки персонажа
    public float groundCheckRadius = 1f; // Радиус проверки земли

    // Объявляем приватные переменные
    private Rigidbody2D rb; // Компонент Rigidbody2D
    private bool facingRight = true; // Направление взгляда врага
    private bool isGrounded = false; // Находится ли враг на земле
    private bool isChasing = false; // Преследует ли враг персонажа
    private bool isBlocked = false; // Заблокирован ли враг препятствием
    private Transform player; // Трансформ персонажа
    
    private Animator _animator;

    // Вызывается при старте скрипта
    private void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
        // animator = GetComponent<Animator>(); // Получаем компонент Animator
        player = GameObject.FindGameObjectWithTag("Player").transform; // Получаем трансформ персонажа
    }

    // Вызывается на каждом кадре
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // Проверяем, находится ли враг на земле

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
                rb.velocity = new Vector2((player.position.x - transform.position.x) * speed, rb.velocity.y); // Двигаем врага в сторону персонажа
            }
        }
    }

    // Вызывается на каждом кадре

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Получаем трансформ персонажа
        
        if (Vector3.Distance(transform.position, player.position) < playerCheckRadius) // Если персонаж находится в зоне действия триггера врага
        {
            isChasing = true; // Враг начинает преследование
            _animator.SetBool("isRun", true);
        }
        else
        {
            isChasing = false; // Враг прекращает преследование
            _animator.SetBool("isRun", false);
        }
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