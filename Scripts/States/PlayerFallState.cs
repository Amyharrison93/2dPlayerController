using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    [field: SerializeField] private float currentSpeed;
    [field: SerializeField] private bool isSprinting;
    private Vector3 Momentum;

    public override void Enter()
    {
        if(stateMachine.isSprinting)
            currentSpeed = stateMachine.PlayerSpeed*stateMachine.playerSprintMult;
        if(!stateMachine.isSprinting)
            currentSpeed = stateMachine.PlayerSpeed;
        
        stateMachine.InputReader.DodgeEvent += OnDash;
        stateMachine.InputReader.JumpEvent += OnJump;

        Debug.Log("Entering falling state");
    }
    public override void Tick(float DeltaTime)
    {
        MoveHorizontal(stateMachine.InputReader.MovementValue.x*currentSpeed, Time.deltaTime);
        if(CheckIfGrounded()) stateMachine.SwitchState(new PlayerMovementState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.InputReader.DodgeEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}
