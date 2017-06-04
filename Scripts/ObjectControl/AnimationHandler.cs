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

    public AnimationHandler(Animator animator, Transform character)
    {
        m_animator = animator;
        m_character = character;
        m_characterTargetRot = character.localRotation;
    }

    public void handle()
    {
        if(InputListener.left)
        {
            float yRot = InputListener.HorizontalInput;
            m_characterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_character.localRotation = m_characterTargetRot;
            Logger.log(m_character.localRotation.ToString());
        }
    }



    public void Walk()
    {
        m_animator.SetFloat("Speed", 0.5f);
    }

    public void Stop()
    {
        m_animator.SetFloat("Speed", 0.0f);
    }
}

