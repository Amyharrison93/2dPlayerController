using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    [field: SerializeField] private float currentSpeed;
    [field: SerializeField] private bool isSprinting;
    private Vector3 Momentum;
    private float maxJumpTimer;
    private float maxJumpTime=.5f;

    public override void Enter()
    {
        if(stateMachine.isSprinting)
            currentSpeed = stateMachine.PlayerSpeed*stateMachine.playerSprintMult;
        if(!stateMachine.isSprinting)
            currentSpeed = stateMachine.PlayerSpeed;
            
        stateMachine.InputReader.DodgeEvent += OnDash;
        stateMachine.InputReader.JumpEvent += OnJump;

        //clear y velocity
        stateMachine.playerRigidbody.linearVelocityY = 0;
        
        stateMachine.playerRigidbody.AddForce(new Vector2(0,stateMachine.PlayerJumpHeight),ForceMode2D.Impulse);
        stateMachine.IncreaseJumpCounter();
        Debug.Log("Entering jump state");
        maxJumpTimer=0;
    }
    public override void Tick(float DeltaTime)
    {
        if (stateMachine.InputReader.IsJumping && maxJumpTime > maxJumpTimer)
        {
            maxJumpTimer+=Time.deltaTime;
            stateMachine.playerRigidbody.AddForce(new Vector2(0,stateMachine.PlayerJumpHeight*1+maxJumpTimer),ForceMode2D.Force);
        }
        MoveHorizontal(stateMachine.InputReader.MovementValue.x*currentSpeed, Time.deltaTime);
        
        if(stateMachine.playerRigidbody.linearVelocityY < -0.5) stateMachine.SwitchState(new PlayerFallState(stateMachine));
    }
    public override void Exit()
    {
        maxJumpTimer=0;
        stateMachine.InputReader.DodgeEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}