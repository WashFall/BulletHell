using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D rigidBody;
    private Vector2 movementVector;
    private float playerSpeed = 5;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Actions.Move.performed += NormalizeMovement;
        playerInput.Actions.Move.canceled += NormalizeMovement;
        if(GameManager.Instance.currentCharacter is not null) 
            playerSpeed = GameManager.Instance.currentCharacter.characterWalkingSpeed;
    }

    private void NormalizeMovement(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();

        if(movementVector.magnitude <= 1) MovePlayer(movementVector);

        else MovePlayer(movementVector.normalized);
    }

    private void MovePlayer(Vector2 playerInput)
    {
        rigidBody.velocity = playerInput * playerSpeed;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MovePlayer(movementVector);
    }

    private void OnDisable()
    {
        playerInput.Actions.Move.performed -= NormalizeMovement;
        playerInput.Actions.Move.canceled -= NormalizeMovement;
        playerInput.Disable();
    }
}
