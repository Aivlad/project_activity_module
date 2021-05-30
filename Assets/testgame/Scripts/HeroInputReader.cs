using UnityEngine;
using UnityEngine.InputSystem;

namespace TestGame
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        public void OnMovement(InputAction.CallbackContext context) => 
            _hero.SetDirection(context.ReadValue<Vector2>());
        
        public void OnDash(InputAction.CallbackContext contex) 
        {
            if (contex.started)
                _hero.SetDashDirection(contex.ReadValue<float>());
        }
    }
}