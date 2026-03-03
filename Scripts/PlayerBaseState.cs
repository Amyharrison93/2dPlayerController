using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    private float rampedMovement = 0;
    protected void MoveHorizontal(float MoveBy, float deltaTime)
    {
        //input ramping
        rampedMovement += MoveBy;
        rampedMovement /=2;
        stateMachine.playerGameobject.transform.Translate(rampedMovement*deltaTime,0,0);
        //stateMachine.playerRigidbody.AddForce(MoveBy*deltaTime, 0,0,ForceMode.Force);
    }
    protected bool CheckIfGrounded()
    {
        RaycastHit hit;
        Debug.DrawRay(stateMachine.playerGameobject.transform.position, 
        stateMachine.playerGameobject.transform.TransformDirection(Vector3.down),
        Color.green);

        if (Physics.Raycast(
            stateMachine.playerGameobject.transform.position, 
            stateMachine.playerGameobject.transform.TransformDirection(Vector3.down), 
            out hit, Mathf.Infinity))
        {
            
            if(hit.distance < 0.51) return true;
        }
        return false;
    }
    protected void SetConstraints()
    {
        stateMachine.playerRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        stateMachine.playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
    private bool CheckCanJump()
    {
        if(stateMachine.PlayerJumpCounter < stateMachine.PlayerJumpCount) return true;
        return false;
    }
    protected void OnJump()
    {
        if(!CheckCanJump()) return;
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
    private bool CheckCanDash()
    {
        if(stateMachine.PlayerDashDelay > stateMachine.PlayerDashTimer) return false;
        return true;
    }
    protected void OnDash()
    {
        if(!CheckCanDash()) return;
        stateMachine.SwitchState(new PlayerDashState(stateMachine));
    }
}