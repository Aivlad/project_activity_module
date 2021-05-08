using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForece;

    public bool isGrounded;
    private Rigidbody2D rigitbody2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private GameObject bottom = null;

    private int score = 0;
    public Text scoreText;

    public float timerSpeed;
    public float timerSpeedMax;
    public float speedBonus;
    public float startSpeed;

    public float timerScale;
    public float timerScaleMax;

    private void Start()
    {
        // заносим данные с объекта в переменные
        rigitbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        // получаем дочерний объект 
        bottom = gameObject.transform.Find("Bottom").gameObject;

        startSpeed = speed;
    }

    private void FixedUpdate()
    {
        isGrounded = bottom.GetComponent<PlayerBottom>().isGrounded;

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

        CallBonus();
    }

    private void Update() 
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void CallBonus()
    {
        if (timerSpeed > 0)
        {
            speed = speedBonus;
            timerSpeed--;
        }
        else
        {
            speed = startSpeed;
        }

        if (timerScale > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
            timerScale--;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Jump()
    {
        // делаем прыжок 
        rigitbody2d.AddForce(transform.up * jumpForece, ForceMode2D.Impulse);
    }

    public void AddCoin(int count = 1)
    {
        score += count;
    }

    public void SpeedBonus()
    {
        timerSpeed = timerSpeedMax;
    }

    public void AddScaleBonus()
    {
        timerScale = timerScaleMax;
    }
}
