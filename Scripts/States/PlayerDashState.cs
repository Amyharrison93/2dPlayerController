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
        stateMachine.soundHander.PlayDashSound();
        stateMachine.ClearJumpCounter();
        currentSpeed = stateMachine.PlayerSpeed/stateMachine.playerSprintMult;
        stateMachine.health.SetInvaunrable(true);
        stateMachine.playerRigidbody.linearVelocityX = 0;
        stateMachine.playerRigidbody.linearVelocityY = 0;
        stateMachine.IncreaseDashCounter();

        dashForce = stateMachine.PlayerDashDistance;

        if(stateMachine.InputReader.MovementValue.x > 0) isDashingRight = true;
        if(!isDashingRight) dashForce = stateMachine.PlayerDashDistance*-1;
        stateMachine.playerRigidbody.gravityScale = 0;
        stateMachine.playerRigidbody.AddForce(new Vector2(dashForce,0),ForceMode2D.Impulse);
        Debug.Log("Entering Dash state");
        stateMachine.InputReader.JumpEvent += OnJump;
    }
    public override void Tick(float DeltaTime)
    {
        if(stateMachine.forceReceiver.velocity == 0) stateMachine.SwitchState(new PlayerFallState(stateMachine));
        dashTimer += Time.deltaTime;
        if(dashTimer > stateMachine.PlayerDashTimer) stateMachine.SwitchState(new PlayerFallState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.playerRigidbody.gravityScale = 3;
        stateMachine.health.SetInvaunrable(false);
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}

