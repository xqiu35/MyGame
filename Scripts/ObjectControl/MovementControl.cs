using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class MovementControl : MonoBehaviour
{
    private float walkSpeed = 3f;
    private Vector3 moveDirection;

    private Animator animator;
    private CharacterController controller;
    private MovementHandler movementHandler;
    private AnimationHandler animationHandler; 

    // Use this for initialization
    void Start()
    {
        moveDirection = Vector3.zero;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        movementHandler = new MovementHandler();
        animationHandler = new AnimationHandler(animator,transform);
    }

    // Update is called once per frame
    void Update()
    {
        InputListener.listenHorizontalInput();
        InputListener.listenVertialInput();
        InputListener.listenJumpInput();
    }

    private void FixedUpdate()
    {
        animationHandler.handle();
        //movementHandler.handle(gameObject, moveDirection, walkSpeed);
    }

    public void setSpeed(float speed)
    {
        walkSpeed = speed;
    }
}
