using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottom : MonoBehaviour
{
    public bool isGrounded;

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
}
