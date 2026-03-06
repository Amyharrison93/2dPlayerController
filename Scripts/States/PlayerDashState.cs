using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    [field: SerializeField] private float currentSpeed;
    [field: SerializeField] private bool isSprinting;
    private Vector3 Momentum;
    private float dashTimer;
    private bool isDashingRight;
    private float dashForce;
    public override void Enter()
    {
        currentSpeed = stateMachine.PlayerSpeed/stateMachine.playerSprintMult;
        stateMachine.health.SetInvaunrable(true);
        stateMachine.playerRigidbody.linearVelocityX = 0;
        stateMachine.IncreaseDashCounter();

        dashForce = stateMachine.PlayerDashDistance;
        if(stateMachine.InputReader.MovementValue.x > 0) isDashingRight = true;
        if(!isDashingRight) dashForce = stateMachine.PlayerDashDistance*-1;
        
        stateMachine.playerRigidbody.AddForce(new Vector2(dashForce,1),ForceMode2D.Impulse);
        Debug.Log("Entering Dash state");
    }
    public override void Tick(float DeltaTime)
    {
        dashTimer += Time.deltaTime;
        if(dashTimer > stateMachine.PlayerDashTimer) stateMachine.SwitchState(new PlayerFallState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.health.SetInvaunrable(false);
        SetConstraints();
    }
}

