using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForece;

    private bool isGrounded;
    private Rigidbody2D rigitbody2d;

    private void Start()
    {
        // заносим данные с объекта вв переменную
        rigitbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // сохраняем координаты объекта
        Vector3 position = transform.position;

        // добавляем к сохраненным координатам ввод с клавиатуры
        position.x += Input.GetAxis("Horizontal") * speed;

        // переносим новую позицию на объект
        transform.position = position;

        // если нажали клавишу Space
        if (Input.GetKey(KeyCode.Space)) 
        {
            Jump();
        }
    }

    private void Jump()
    {
        // проверка: объект стоит на земле?
        if (isGrounded)
        {
            // отмечаем, что объект покинул землю
            isGrounded = false;
            // делаем прыжок 
            rigitbody2d.AddForce(transform.up * jumpForece, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // если есть пересечение с землей
        if (collision.gameObject.tag == "Ground") 
        {
            // ставим флаг: объект на земле
            isGrounded = true;
        }
    }


}
