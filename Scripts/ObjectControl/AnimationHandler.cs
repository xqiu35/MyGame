using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler{

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;

    private Animator m_animator;
    private Transform m_character;
    private Quaternion m_characterTargetRot;
    private MovementControl m_movementControl;
    private AnimationState currentState;

    public AnimationHandler(Animator animator, Transform character)
    {
        m_animator = animator;
        m_character = character;
        m_movementControl = character.GetComponentInChildren<MovementControl>();
        currentState = new IdelState();
    }

    public void handle()
    {
        m_characterTargetRot = m_character.localRotation;
        currentState.handle(this, m_animator, m_movementControl);
        Rotate();
    }

    public void Rotate()
    {
        if(!InputListener.up)
        {
            float yRot = InputListener.HorizontalInput;
            m_characterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_character.localRotation = m_characterTargetRot;
        }
    }

    public void setState(AnimationState state)
    {
        currentState = state;
    }
}

