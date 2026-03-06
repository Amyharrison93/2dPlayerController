using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private float movementInertia;
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action CrouchEvent;
    public event Action StandEvent;
    public event Action SprintEvent;
    public event Action MeleeAttackEvent;
    public event Action BlockEvent;
    public bool IsAttacking { get; private set; }
    public bool IsTargeting { get; private set; }
    public bool IsBlocking { get; private set; }
    public bool IsJumping {get; private set;}
    public Vector2 MovementValue { get; private set; }
    private InputSystem_Actions inputActions;  // Reference to the generated Input Actions

    private void Awake()
    {
        inputActions = new InputSystem_Actions(); // Create an instance of the input actions
    }

    private void OnEnable()
    {
        inputActions.Enable(); // Enable the input actions
        inputActions.Player.Move.performed += context => OnMove(context);
        inputActions.Player.Move.canceled += context => OnMove(context);
        
        inputActions.Player.Jump.performed += context => OnJump();
        inputActions.Player.Jump.canceled += context => OnJumpCancelled();

        inputActions.Player.Dodge.performed += context => OnDodge();
        inputActions.Player.Sprint.performed += context => OnSprint();

        inputActions.Player.Crouch.performed += context => OnCrouch();
        inputActions.Player.Crouch.canceled += context => OnStand();

    }
    public void OnJump()
    {
        JumpEvent?.Invoke();
        IsJumping=true;
        Debug.Log(IsJumping);
    }
    public void OnJumpCancelled()
    {
        IsJumping=false;
        Debug.Log(IsJumping);
    }
    public void OnDodge()
    {
        DodgeEvent?.Invoke();
    }
    public void OnSprint()
    {
        SprintEvent?.Invoke();
    }
    public void OnCrouch()
    {
        CrouchEvent?.Invoke();
    }
    public void OnStand()
    {
        StandEvent?.Invoke();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }
    public void OnTarget(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            IsTargeting = false;
        }

        else if (context.performed)
        {
            IsTargeting = true;
        }
    }
    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            IsBlocking = false;
        }
        else if (context.performed)
        {
            IsBlocking = true;
        }
    }
}
