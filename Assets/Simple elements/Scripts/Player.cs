using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForece;

    public bool isGrounded;
    private Rigidbody2D rigitbody2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private int score = 0;
    public Text scoreText;

    private void Start()
    {
        // заносим данные с объекта в переменные
        rigitbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // если нажали клавишу Space
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // сохраняем координаты объекта
        Vector3 position = transform.position;

        // добавляем к сохраненным координатам ввод с клавиатуры
        position.x += Input.GetAxis("Horizontal") * speed;

        // переносим новую позицию на объект
        transform.position = position;

        // проверка на движение
        if (Input.GetAxis("Horizontal") != 0)
        {
            // проверка в какую сторону движемся
            if (Input.GetAxis("Horizontal") < 0)
            {
                spriteRenderer.flipX = true; // горизонтальное отражение спрайтов
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                spriteRenderer.flipX = false; // горизонтальное отражение спрайтов
            }
            animator.SetInteger("State", 1);
        }
        else 
        {
            animator.SetInteger("State", 0);
        }

        if (!isGrounded) 
        {
            animator.SetInteger("State", 2);
        }

        scoreText.text = string.Format("Score: {0}", score);
    }

    private void Jump()
    {
        // делаем прыжок 
        rigitbody2d.AddForce(transform.up * jumpForece, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // если входим в область земли
        if (collision.gameObject.tag == "Ground")
        {
            // ставим флаг: объект на земле
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // если выходим из области земли
        if (collision.gameObject.tag == "Ground")
        {
            // ставим флаг: объект покинул земле
            isGrounded = false;
        }

    }

    public void AddCoin(int count = 1)
    {
        score += count;
    }


}
