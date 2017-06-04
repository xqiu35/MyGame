using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler
{
    public void handle(GameObject obj, Vector3 moveDirection, float moveSpeed)
    {
        CharacterController controller = obj.GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(InputListener.HorizontalInput, 0, InputListener.VertialInput);
            moveDirection = obj.transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
        }
        else
        {
            moveDirection += Physics.gravity * Time.fixedDeltaTime;
        }

        controller.Move(moveDirection * Time.fixedDeltaTime);
    }
}
