using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    [field: SerializeField] private float currentSpeed;
    [field: SerializeField] private bool isSprinting;
    [field: SerializeField] private float jumpForceTimer;
    private float jumpForce;

    public override void Enter()
    {
        stateMachine.InputReader.DodgeEvent += OnDash;
        stateMachine.InputReader.JumpEvent += OnJump;

        jumpForceTimer = 0;

        if(stateMachine.PlayerJumpCounter > 1) stateMachine.ClearDashCounter();

        currentSpeed = stateMachine.PlayerSpeed;

        stateMachine.forceReceiver.ClearVerticleVelocity();
        if(CheckIfTouchingWall()) {
            //jump away from the wall
            stateMachine.forceReceiver.AddImpulse(new Vector2(-Mathf.Sign(stateMachine.forceReceiver.Velocity.x)*stateMachine.PlayerSpeed, stateMachine.PlayerJumpHeight));
        }
        stateMachine.forceReceiver.AddImpulse(new Vector2(0,stateMachine.PlayerJumpHeight));
        stateMachine.IncreaseJumpCounter();
        Debug.Log("Entering jump state");

        stateMachine.soundHander.PlayJumpSound();
        stateMachine.PlayerAnimator.RestartJumpAnimation();
    }
    public override void Tick(float DeltaTime)
    {
        stateMachine.PlayerAnimator.HandleVerticleMoveAnimation(stateMachine.forceReceiver.Velocity.normalized);
        stateMachine.forceReceiver.AddForce(new(stateMachine.InputReader.MovementValue.x*stateMachine.PlayerSpeed,0));
        
        Debug.Log("jump force timer = "+jumpForceTimer + " Jump force time = "+stateMachine.jumpForceTime + " is jump pressed = " + stateMachine.InputReader.IsJumping);

        if(jumpForceTimer < stateMachine.jumpForceTime && stateMachine.InputReader.IsJumping)
        {
            stateMachine.forceReceiver.AddForce(new(0,stateMachine.jumpConstForce));
            jumpForceTimer += Time.deltaTime;
        }
        else
        {
            if(stateMachine.forceReceiver.Velocity.y < -0.5) stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }
    }
    public override void Exit()
    {
        stateMachine.InputReader.DodgeEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}