using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerSlideState : PlayerBaseState
{
    public PlayerSlideState(PlayerStateMachine stateMachine) : base(stateMachine) { }
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

        //squish the player
        stateMachine.CrouchDownScale();
        stateMachine.playerRigidbody.linearVelocityX = 0;

        dashForce = stateMachine.PlayerDashDistance;
        if(stateMachine.InputReader.MovementValue.x > 0) isDashingRight = true;
        if(!isDashingRight) dashForce = stateMachine.PlayerDashDistance*-1;
        
        stateMachine.playerRigidbody.AddForce(new Vector2(dashForce,0),ForceMode2D.Impulse);
        Debug.Log("Entering slide state");
    }
    public override void Tick(float DeltaTime)
    {
        dashTimer += Time.deltaTime;
        if(dashTimer > stateMachine.PlayerDashTimer) stateMachine.SwitchState(new PlayerMovementState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.StandUpScale();
        stateMachine.health.SetInvaunrable(false);
    }
}

