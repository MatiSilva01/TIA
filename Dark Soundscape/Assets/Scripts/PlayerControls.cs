using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
//Handle inputs
[RequireComponent(typeof(PlayerController))]
public class PlayerControls : MonoBehaviour
{
    #region References
        PlayerController controller;
    #endregion

    void Awake()
{
   controller = GetComponent<PlayerController>();
}
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(CallbackContext ctx)
    {
        controller.movInput = ctx.ReadValue<Vector2>();
    }

    public void OnAim(CallbackContext ctx)
    {
        controller.aimInput = ctx.ReadValue<Vector2>();
    }

    public void OnInteract(CallbackContext ctx){
        if(ctx.performed){
            controller.Interact();
        }
    }
    
}
