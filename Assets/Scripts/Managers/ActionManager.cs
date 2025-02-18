
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public UnityEvent jump;
    public UnityEvent jump2;
    public UnityEvent jumpHold;
    public UnityEvent jumpHold2;
    public UnityEvent<int> moveCheck;
    public UnityEvent<int> moveCheck2;
    public CharacterSwap characterSwap;

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("JumpHold was started");
        else if (context.performed)
        {
            Debug.Log("JumpHold was performed");
            Debug.Log(context.duration);
            if (characterSwap.activeCharacter.name == "Mario")
                jumpHold.Invoke();
            else
                jumpHold2.Invoke();
        }
        else if (context.canceled)
            Debug.Log("JumpHold was cancelled");
    }

    // called twice, when pressed and unpressed
    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("Jump was started");
        else if (context.performed)
        {
            if (characterSwap.activeCharacter.name == "Mario")
                jump.Invoke();
            else
                jump2.Invoke();
            Debug.Log("Jump was performed");
        }
        else if (context.canceled)
            Debug.Log("Jump was cancelled");

    }

    // called twice, when pressed and unpressed
    public void OnMoveAction(InputAction.CallbackContext context)
    {
        // Debug.Log("OnMoveAction callback invoked");
        if (context.started)
        {
            Debug.Log("move started");
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;

            if (characterSwap.activeCharacter.name == "Mario")
                moveCheck.Invoke(faceRight);
            else
                moveCheck2.Invoke(faceRight);
        }
        if (context.canceled)
        {
            Debug.Log("move stopped");
            if (characterSwap.activeCharacter.name == "Mario")
                moveCheck.Invoke(0);
            else
                moveCheck2.Invoke(0);
        }

    }
}
