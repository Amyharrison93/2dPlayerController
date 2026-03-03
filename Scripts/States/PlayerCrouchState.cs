using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    [field: SerializeField] private float currentSpeed;
    [field: SerializeField] private bool isSprinting;
    private Vector3 Momentum;

    public override void Enter()
    {
        currentSpeed = stateMachine.PlayerSpeed/stateMachine.playerSprintMult;
        stateMachine.InputReader.DodgeEvent += OnDash;
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.SprintEvent +=OnSprint;
        stateMachine.InputReader.StandEvent += OnStand;

        //squish the player
        stateMachine.CrouchDownScale();

        Debug.Log("Entering crouch state");
    }
    public override void Tick(float DeltaTime)
    {
        stateMachine.CountDashDelay();
        MoveHorizontal(stateMachine.InputReader.MovementValue.x*currentSpeed, Time.deltaTime);
        if(!CheckIfGrounded()) stateMachine.SwitchState(new PlayerFallState(stateMachine));
    }
    public override void Exit()
    {
        Debug.Log("Exiting crouch state");
        stateMachine.InputReader.DodgeEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.SprintEvent -=OnSprint;
        stateMachine.InputReader.StandEvent -= OnStand;
        stateMachine.StandUpScale();
    }
    private void OnSprint()
    {
        if(Mathf.Abs(stateMachine.InputReader.MovementValue.x) < 0.75) return;
        stateMachine.SwitchState(new PlayerMovementState(stateMachine));
    }
    private void OnStand()
    {
        stateMachine.SwitchState(new PlayerMovementState(stateMachine));
    }
}
