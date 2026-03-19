using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    [field: SerializeField] private float currentSpeed;
    [field: SerializeField] private bool isSprinting;
    [field: SerializeField] private float jumpForceTimer;
    [field: SerializeField] private float jumpForceConst = 5;
    private float jumpForce;

    public override void Enter()
    {
        stateMachine.soundHander.PlayJumpSound();
        stateMachine.PlayerAnimator.RestartJumpAnimation();
        jumpForceTimer = 0;
        if(stateMachine.PlayerJumpCounter > 1) stateMachine.ClearDashCounter();
        if(stateMachine.isSprinting)
            currentSpeed = stateMachine.PlayerSpeed*stateMachine.playerSprintMult;
        if(!stateMachine.isSprinting)
            currentSpeed = stateMachine.PlayerSpeed;
            
        stateMachine.InputReader.DodgeEvent += OnDash;
        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.forceReceiver.ClearVerticleVelocity();

        stateMachine.forceReceiver.AddImpulse(new Vector2(0,stateMachine.PlayerJumpHeight));
        stateMachine.IncreaseJumpCounter();
        Debug.Log("Entering jump state");
    }
    public override void Tick(float DeltaTime)
    {
        stateMachine.PlayerAnimator.HandleVerticleMoveAnimation(stateMachine.forceReceiver.Velocity.normalized);
        if(jumpForceTimer < stateMachine.jumpForceTime && stateMachine.InputReader.IsJumping)
        {
            stateMachine.forceReceiver.AddForce(new(0,jumpForceConst));
            jumpForceTimer += Time.deltaTime;
        }
        stateMachine.forceReceiver.AddForce(new(stateMachine.InputReader.MovementValue.x*stateMachine.PlayerSpeed,0));
        if(stateMachine.forceReceiver.Velocity.y < -0.5) stateMachine.SwitchState(new PlayerFallState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.InputReader.DodgeEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}