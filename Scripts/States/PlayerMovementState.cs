using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerMovementState : PlayerBaseState
{
    public PlayerMovementState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    [field: SerializeField] private float currentSpeed;
    [field: SerializeField] private bool isSprinting;
    private Vector3 Momentum;
    private float CyoteeTime = 0.11666666666f;
    private float CyoteeTimer;

    public override void Enter()
    {
        currentSpeed = stateMachine.PlayerSpeed;
        stateMachine.InputReader.DodgeEvent += OnDash;
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.SprintEvent +=OnSprint;
        stateMachine.InputReader.CrouchEvent += OnCrouch;
        CyoteeTime=0;
        stateMachine.isSprinting = false;

        stateMachine.ClearJumpCounter();
        stateMachine.ClearDashCounter();

        Debug.Log("Entering movement state");
    }
    public override void Tick(float DeltaTime)
    {
        stateMachine.CountDashDelay();
        MoveHorizontal(stateMachine.InputReader.MovementValue.x*currentSpeed, Time.deltaTime);
        
        if(Mathf.Abs(stateMachine.InputReader.MovementValue.x)< 0.9) 
        {
            isSprinting = false;
            currentSpeed = stateMachine.PlayerSpeed;
        }
        if(CheckIfGrounded()) {
            CyoteeTimer=0;
            return;
        }

        CyoteeTimer+=Time.deltaTime;
        if (CyoteeTimer >= CyoteeTime)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }
    }
    public override void Exit()
    {
        Debug.Log("Exiting movement state");
        stateMachine.InputReader.DodgeEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.SprintEvent -=OnSprint;
        stateMachine.InputReader.CrouchEvent -= OnCrouch;
    }
    private void OnSprint()
    {
        if(Mathf.Abs(stateMachine.InputReader.MovementValue.x) < 0.75) return;

        if(isSprinting) 
        {
            currentSpeed = stateMachine.PlayerSpeed;
            stateMachine.isSprinting = false;
        }
        else
        {
            currentSpeed = stateMachine.PlayerSprintSpeed;
            stateMachine.isSprinting = true;
        }
    }
    private void OnCrouch()
    {
        if(isSprinting)stateMachine.SwitchState(new PlayerSlideState(stateMachine));
        if(!isSprinting)stateMachine.SwitchState(new PlayerCrouchState(stateMachine));
    }
}
