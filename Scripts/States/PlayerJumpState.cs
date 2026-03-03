using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    [field: SerializeField] private float currentSpeed;
    [field: SerializeField] private bool isSprinting;
    private Vector3 Momentum;
    private Rigidbody rb;

    public override void Enter()
    {
        currentSpeed = stateMachine.PlayerSpeed/stateMachine.playerSprintMult;
        stateMachine.InputReader.DodgeEvent += OnDash;
        stateMachine.InputReader.JumpEvent += OnJump;
        
        stateMachine.playerRigidbody.AddForce(0,stateMachine.PlayerJumpHeight,0,ForceMode.Impulse);
        stateMachine.IncreaseJumpCounter();
        Debug.Log("Entering jump state");
    }
    public override void Tick(float DeltaTime)
    {
        MoveHorizontal(stateMachine.InputReader.MovementValue.x*currentSpeed, Time.deltaTime);
        
        if(stateMachine.playerRigidbody.linearVelocity.y < -0.5) stateMachine.SwitchState(new PlayerFallState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.InputReader.DodgeEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}
