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
        RaycastHit2D hit;
        Debug.DrawRay(stateMachine.playerGameobject.transform.position, 
                    stateMachine.playerGameobject.transform.TransformDirection(Vector3.down), 
                    Color.green);

        hit = Physics2D.Raycast(
            stateMachine.playerGameobject.transform.position, 
            stateMachine.playerGameobject.transform.TransformDirection(Vector3.down),1,3);
        // Check if there was a hit and the distance is less than 0.51
        if (hit) 
        {
            float distance = Mathf.Abs(hit.point.y - stateMachine.playerGameobject.transform.position.y);
            if(distance < 0.52f) return true;
        }
        return false;
    }
    protected void SetConstraints()
    {
        stateMachine.playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        if(stateMachine.PlayerDashDelay > stateMachine.PlayerDashTimer)return false;
        if(stateMachine.PlayerDashCounter>=stateMachine.PlayerDashCount) return false;
        return true;
    }
    protected void OnDash()
    {
        if(!CheckCanDash()) return;
        stateMachine.SwitchState(new PlayerDashState(stateMachine));
    }
}