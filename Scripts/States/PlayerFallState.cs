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
        currentSpeed = stateMachine.PlayerSpeed;
        
        stateMachine.InputReader.DodgeEvent += OnDash;
        stateMachine.InputReader.JumpEvent += OnJump;

        Debug.Log("Entering falling state");
    }
    public override void Tick(float DeltaTime)
    {
        stateMachine.PlayerAnimator.HandleVerticleMoveAnimation(stateMachine.forceReceiver.Velocity.normalized);
        
        stateMachine.forceReceiver.AddForce(new Vector2 (stateMachine.InputReader.MovementValue.x*stateMachine.PlayerSpeed,Physics.gravity.z*3));
        if(CheckIfGrounded()) {
            stateMachine.SwitchState(new PlayerMovementState(stateMachine));
            stateMachine.soundHander.PlayLandSound();
        }

    }
    public override void Exit()
    {
        stateMachine.InputReader.DodgeEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}
