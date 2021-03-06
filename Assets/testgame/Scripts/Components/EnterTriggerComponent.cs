using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TestGame.Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        //запускает встроенное событие при заходе в зону триггера объекта с встроенным тегом 
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tag)) 
            {
                _action?.Invoke();
            }
        }
    }
}   