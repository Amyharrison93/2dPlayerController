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

        dashForce = stateMachine.PlayerDashForce;
        if(stateMachine.InputReader.MovementValue.x > 0) isDashingRight = true;
        if(!isDashingRight) dashForce = stateMachine.PlayerDashForce*-1;
        stateMachine.playerRigidbody.useGravity = false;
        stateMachine.playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY;

        stateMachine.playerRigidbody.AddForce(dashForce,0,0,ForceMode.Impulse);
        Debug.Log("Entering Dash state");
    }
    public override void Tick(float DeltaTime)
    {
        dashTimer += Time.deltaTime;
        if(dashTimer > stateMachine.PlayerDashTimer) stateMachine.SwitchState(new PlayerMovementState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.playerRigidbody.useGravity = true;
        stateMachine.health.SetInvaunrable(false);
        stateMachine.playerRigidbody.constraints = RigidbodyConstraints.None;
        SetConstraints();
    }
}

