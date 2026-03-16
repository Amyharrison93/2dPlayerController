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
        stateMachine.playerRigidbody.gravityScale = 3;
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
        stateMachine.forceReceiver.AddForce(new Vector2 (stateMachine.InputReader.MovementValue.x*stateMachine.PlayerSpeed,0));
        if(CheckIfGrounded()) {
            stateMachine.SwitchState(new PlayerMovementState(stateMachine));
            stateMachine.soundHander.PlayLandSound();
        }

    }
    public override void Exit()
    {
        stateMachine.playerRigidbody.gravityScale = 1;
        stateMachine.InputReader.DodgeEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}
