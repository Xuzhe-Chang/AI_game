using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputController inputController;

    public static InputController InputController
    {
        get
        {
            if (inputController == null)
                inputController = new InputController();

            return inputController;
        }
    }
    private void OnEnable()
    {
        InputController.Player.Move.Enable();
        InputController.Player.Attack.Enable();
        InputController.Player.Jump.Enable();
    }
    private void OnDisable()
    {
        InputController.Player.Move.Disable();
        InputController.Player.Attack.Disable();
        InputController.Player.Jump.Disable();
    }

}
