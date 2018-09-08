using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    internal static Player instance;

    public PlayerInput input;

    public enum ACTIONSTATE
    {
        LOCOMOTION, //on ground nav (walking, running, crouching)
        HOISTING,
        CLIMBING,   //climbing on rope or ladder
        SWINGNING, //climbing on rope
        INTERACTING, //pushing or pulling something
    }
    public ACTIONSTATE actionState = ACTIONSTATE.LOCOMOTION;
    bool actionStateCallOnce = true;

    public enum LOCOMOTIONSTATE
    {
        WALKING,    //or standing
        RUNNING,
        CROUCHING
    }
    public LOCOMOTIONSTATE locomotionState = LOCOMOTIONSTATE.WALKING;
    bool locomotionStateCallOnce = true;

    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;

    public Animator animator;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        //Initialize class
        instance = this;
    }

    private void Update()
    {
        ActionStateFSM();
    }

    void ActionStateFSM()
    {
        switch(actionState)
        {
            case ACTIONSTATE.LOCOMOTION:
                if(actionStateCallOnce)
                {

                    actionStateCallOnce = false;
                }
                LocomotionStateFSM();   //call locomotion
                break;
            case ACTIONSTATE.HOISTING:
                if (actionStateCallOnce)
                {

                    actionStateCallOnce = false;
                }
                break;
            case ACTIONSTATE.CLIMBING:
                if (actionStateCallOnce)
                {

                    actionStateCallOnce = false;
                }
                break;
            case ACTIONSTATE.SWINGNING:
                if (actionStateCallOnce)
                {

                    actionStateCallOnce = false;
                }
                break;
            case ACTIONSTATE.INTERACTING:
                if (actionStateCallOnce)
                {

                    actionStateCallOnce = false;
                }
                break;
        }
    }

    void LocomotionStateFSM()
    {
        Vector3 direction = Vector3.zero;
        if (input.ControlsActivated.Exists(
            x => x == PlayerInput.CONTROLS.W_HOLD || 
            x == PlayerInput.CONTROLS.S_HOLD || 
            x == PlayerInput.CONTROLS.A_HOLD || 
            x == PlayerInput.CONTROLS.D_HOLD))
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        LOCOMOTIONSTATE prevLocState = locomotionState;

        //prioritize running over crouching

        if (input.ControlsActivated.Exists(x => x == PlayerInput.CONTROLS.SHIFT_HOLD))
        {
            if (locomotionState != LOCOMOTIONSTATE.RUNNING)
            {
                ChangeLocomotionState(LOCOMOTIONSTATE.RUNNING);
            }

        }
        else
        {
            if(locomotionState == LOCOMOTIONSTATE.RUNNING)
                ChangeLocomotionState(LOCOMOTIONSTATE.WALKING);
        }
        
        if(input.ControlsActivated.Exists(x => x == PlayerInput.CONTROLS.CTRL_TAP))
        {
            if (locomotionState == LOCOMOTIONSTATE.WALKING)
                ChangeLocomotionState(LOCOMOTIONSTATE.CROUCHING);
            else if (locomotionState == LOCOMOTIONSTATE.CROUCHING)
                ChangeLocomotionState(LOCOMOTIONSTATE.WALKING);
        }

        switch(locomotionState)
        {
            case LOCOMOTIONSTATE.WALKING:
                if(locomotionStateCallOnce)
                {
                    if (prevLocState == LOCOMOTIONSTATE.CROUCHING)
                        Uncrouch();

                    locomotionStateCallOnce = false;
                }
                MoveEntity(direction, walkSpeed);
                break;
            case LOCOMOTIONSTATE.RUNNING:
                if (locomotionStateCallOnce)
                {
                    if (prevLocState == LOCOMOTIONSTATE.CROUCHING)
                        Uncrouch();

                    locomotionStateCallOnce = false;
                }
                MoveEntity(direction, runSpeed);
                break;
            case LOCOMOTIONSTATE.CROUCHING:
                if (locomotionStateCallOnce)
                {
                    Crouch();
                    locomotionStateCallOnce = false;
                }
                MoveEntity(direction, crouchSpeed);
                break;
        }
    }

    public override void MoveEntity(Vector3 direction, float speed)
    {
        base.MoveEntity(direction, speed);

        //animator set
    }

    public void ChangeActionState(ACTIONSTATE state)
    {
        actionState = state;
        actionStateCallOnce = true;
    }

    public void ChangeLocomotionState(LOCOMOTIONSTATE state)
    {
        locomotionState = state;
        locomotionStateCallOnce = true;
    }
}
