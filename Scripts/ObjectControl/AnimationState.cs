using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationState{

    public static IdelState idelState = new IdelState();
    public static ForwardState forwardState = new ForwardState();
    public static RunState runState = new RunState();
    public static LeftWalkState leftWalkState = new LeftWalkState();
    public static LeftRunState leftRunState = new LeftRunState();
    public static RightWalkState rightWalkState = new RightWalkState();
    public static RightRunState rightRunState = new RightRunState();
    public static BackWalkState backWalkState = new BackWalkState();

    public abstract void handle(AnimationHandler handler, Animator animator, MovementControl control);

    public bool canGoNextState(ref float lastInStateTime)
    {
        if (lastInStateTime == 0)
        {
            lastInStateTime = Time.time;
            return false;
        }
        else if (Time.time - lastInStateTime > 1f)
        {
            lastInStateTime = 0;
            return true;
        }

        return false;
    }

    protected void SetIdle(Animator animator)
    {
        animator.SetBool("Forward", false);
        animator.SetBool("Run", false);
        animator.SetBool("Idel", true);
        animator.SetBool("LeftWalk", false);
        animator.SetBool("LeftRun", false);
        animator.SetBool("RightWalk", false);
        animator.SetBool("RightRun", false);
    }

    protected void SetRun(Animator animator)
    {
        animator.SetBool("Forward", false);
        animator.SetBool("Run", true);
        animator.SetBool("Idel", false);
        animator.SetBool("LeftWalk", false);
        animator.SetBool("LeftRun", false);
        animator.SetBool("RightWalk", false);
        animator.SetBool("RightRun", false);
    }

    protected void SetWalk(Animator animator)
    {
        animator.SetBool("Forward", true);
        animator.SetBool("Run", false);
        animator.SetBool("Idel", false);
        animator.SetBool("LeftWalk", false);
        animator.SetBool("LeftRun", false);
        animator.SetBool("RightWalk", false);
        animator.SetBool("RightRun", false);
    }

    protected void SetLeftWalk(Animator animator)
    {
        animator.SetBool("Forward", false);
        animator.SetBool("Run", false);
        animator.SetBool("Idel", false);
        animator.SetBool("LeftWalk", true);
        animator.SetBool("LeftRun", false);
        animator.SetBool("RightWalk", false);
        animator.SetBool("RightRun", false);
    }

    protected void SetLeftRun(Animator animator)
    {
        animator.SetBool("Forward", false);
        animator.SetBool("Run", false);
        animator.SetBool("Idel", false);
        animator.SetBool("LeftWalk", false);
        animator.SetBool("LeftRun", true);
        animator.SetBool("RightWalk", false);
        animator.SetBool("RightRun", false);
    }

    protected void SetRightWalk(Animator animator)
    {
        animator.SetBool("Forward", false);
        animator.SetBool("Run", false);
        animator.SetBool("Idel", false);
        animator.SetBool("LeftWalk", false);
        animator.SetBool("LeftRun", false);
        animator.SetBool("RightWalk", true);
        animator.SetBool("RightRun", false);
    }

    protected void SetRightRun(Animator animator)
    {
        animator.SetBool("Forward", false);
        animator.SetBool("Run", false);
        animator.SetBool("Idel", false);
        animator.SetBool("LeftWalk", false);
        animator.SetBool("LeftRun", false);
        animator.SetBool("RightWalk", false);
        animator.SetBool("RightRun", true);
    }
}

public class IdelState : AnimationState
{
    public override void handle(AnimationHandler handler, Animator animator, MovementControl control)
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idel"))
        {
            Logger.log("idel");

            if (InputListener.up && InputListener.left)
            {
                SetLeftWalk(animator);
                handler.setState(AnimationState.leftWalkState);
            }
            else if (InputListener.up && InputListener.right)
            {
                SetRightWalk(animator);
                handler.setState(AnimationState.rightWalkState);
            }
            else if (InputListener.up)
            {
                SetWalk(animator);
                handler.setState(AnimationState.forwardState);
            }
        }
    }
}

public class ForwardState : AnimationState
{
    private float lastInStateTime=0;
    public override void handle(AnimationHandler handler, Animator animator, MovementControl control)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Forward"))
        {
            Logger.log("forward");

            if (InputListener.up && InputListener.left)
            {
                SetLeftWalk(animator);
                handler.setState(AnimationState.leftWalkState);
            }
            else if (InputListener.up && InputListener.right)
            {
                SetRightWalk(animator);
                handler.setState(AnimationState.rightWalkState);
            }
            else if (InputListener.up)
            {
                if (canGoNextState(ref lastInStateTime))
                {
                    SetRun(animator);
                    handler.setState(AnimationState.runState);
                }
            }
            else
            {
                SetIdle(animator);
                lastInStateTime = 0;
                handler.setState(AnimationState.idelState);
            }
        }
    }
}

public class RunState : AnimationState
{
    public override void handle(AnimationHandler handler, Animator animator, MovementControl control)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            Logger.log("Run");

            if (InputListener.up && InputListener.left)
            {
                SetLeftRun(animator);
                handler.setState(AnimationState.leftRunState);
            }
            else if (InputListener.up && InputListener.right)
            {
                SetRightRun(animator);
                handler.setState(AnimationState.rightRunState);
            }
            if (!InputListener.up)
            {
                SetIdle(animator);
                handler.setState(AnimationState.idelState);
            }
        }
    }
}

public class LeftWalkState : AnimationState
{
    private float lastInStateTime=0;

    public override void handle(AnimationHandler handler, Animator animator, MovementControl control)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftWalk"))
        {
            Logger.log("LeftWalkState");

            if (InputListener.up && InputListener.left)
            {
                if (canGoNextState(ref lastInStateTime))
                {
                    SetLeftRun(animator);
                    handler.setState(AnimationState.leftRunState);
                }
            }
            else if (InputListener.up && InputListener.right)
            {
                SetRightWalk(animator);
                handler.setState(AnimationState.rightWalkState);
            }
            else if (InputListener.up)
            {
                SetWalk(animator);
                handler.setState(AnimationState.forwardState);
            }
            else
            {
                SetIdle(animator);
                lastInStateTime = 0;
                handler.setState(AnimationState.idelState);
            }
        }
    }
}

public class LeftRunState : AnimationState
{
    public override void handle(AnimationHandler handler, Animator animator, MovementControl control)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftRun"))
        {
            Logger.log("LeftRunState");

            if (InputListener.up && InputListener.left)
            {
                return;
            }
            else if (InputListener.up && InputListener.right)
            {
                SetRightRun(animator);
                handler.setState(AnimationState.rightRunState);
            }
            else if (InputListener.up && !InputListener.left && !InputListener.right)
            {
                SetRun(animator);
                handler.setState(AnimationState.runState);
            }
            else
            {
                SetIdle(animator);
                handler.setState(AnimationState.idelState);
            }
        }

    }
}

public class RightWalkState : AnimationState
{
    private float lastInStateTime = 0;
    public override void handle(AnimationHandler handler, Animator animator, MovementControl control)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightWalk"))
        {
            Logger.log("RightWalkState");

            if (InputListener.up && InputListener.right)
            {
                if (canGoNextState(ref lastInStateTime))
                {
                    SetRightRun(animator);
                    handler.setState(AnimationState.rightRunState);
                }
            }
            else if (InputListener.up && InputListener.left)
            {
                SetLeftWalk(animator);
                handler.setState(AnimationState.leftWalkState);
            }
            else if (InputListener.up)
            {
                SetWalk(animator);
                handler.setState(AnimationState.forwardState);
            }
            else
            {
                SetIdle(animator);
                lastInStateTime = 0;
                handler.setState(AnimationState.idelState);
            }
        }
    }
}

public class RightRunState : AnimationState
{
    public override void handle(AnimationHandler handler, Animator animator, MovementControl control)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightRun"))
        {
            Logger.log("RightRunState");

            if (InputListener.up && InputListener.right)
            {
                return;
            }
            else if (InputListener.up && InputListener.left)
            {
                SetLeftRun(animator);
                handler.setState(AnimationState.leftRunState);
            }
            else if (InputListener.up && !InputListener.left && !InputListener.right)
            {
                SetRun(animator);
                handler.setState(AnimationState.runState);
            }
            else
            {
                SetIdle(animator);
                handler.setState(AnimationState.idelState);
            }
        }
    }
}

public class BackWalkState : AnimationState
{
    public override void handle(AnimationHandler handler, Animator animator, MovementControl control)
    {
        Logger.log("BackWalkState");

        if (!InputListener.up)
        {
            SetIdle(animator);
            handler.setState(AnimationState.idelState);
        }
    }
}

