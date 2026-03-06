using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field:SerializeField] public InputController InputReader { get; private set; }
    [field:SerializeField] public ForceReceiver forceReceiver {get; private set;}
    [field:SerializeField] public GameObject playerGameobject { get; private set;}
    [field:SerializeField] public Rigidbody2D playerRigidbody {get; private set;} 
    [field: SerializeField] public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    [field: SerializeField] public float MovementDeadZone { get; private set; }
    [field: SerializeField] public float PlayerSpeed { get; private set; }
    [field:SerializeField] public float playerSprintMult {get;private set;}
    [field: SerializeField] public float PlayerSprintSpeed { get; private set; }
    [field: SerializeField] public float PlayerJumpHeight { get; private set; }
    [field: SerializeField] public float PlayerJumpCount { get; private set; }
    [field: SerializeField] public float PlayerJumpCounter { get; private set; }
    [field: SerializeField] public float PlayerDashDistance {get;private set;}
    [field: SerializeField] public float PlayerDashCount {get;private set;}
    [field: SerializeField] public float PlayerDashCounter {get;private set;}
    [field: SerializeField] public float PlayerDashTimer {get;private set;}
    [field: SerializeField] public float PlayerDashDelay {get;private set;}
    [field: SerializeField] public int PlayerId { get; private set; } = 1;
    [field: SerializeField] public HealthHandler health { get; private set; }
    [field:SerializeField] public bool isSprinting;
    private void Start()
    {
        if(PlayerId == 0)
        {
            PlayerId = Random.Range(1, 10000);
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SwitchState(new PlayerMovementState(this));
        PlayerSprintSpeed = PlayerSpeed*playerSprintMult;
    }
    private void InitializePlayer()
    {

    }
    private void OnEnable()
    {
        health.OnTakeDamage += HandleTakeDamage;
        health.OnDeath += HandleDeath;
    }
    private void OnDisable()
    {
        health.OnTakeDamage -= HandleTakeDamage;
        health.OnDeath -= HandleDeath;
    }
    private void HandleTakeDamage()
    {
        //SwitchState(new PlayerImpactState(this));
    }
    private void HandleDeath()
    {
        health.OnDeath -= HandleDeath;
        //SwitchState(new PlayerDeathState(this));
    }
    public void CountDashDelay()
    {
        if(PlayerDashTimer < PlayerDashDelay) PlayerDashTimer+=Time.deltaTime;
    }
    public void ClearDashTimer()
    {
        PlayerDashTimer+=0;
    }
    public void IncreaseDashCounter()
    {
        PlayerDashCounter+=1;
    }
    public void ClearDashCounter()
    {
        PlayerDashCounter=0;
    }
    public void IncreaseJumpCounter()
    {
        PlayerJumpCounter += 1;
    }
    public void ClearJumpCounter()
    {
        PlayerJumpCounter = 0;
    }
    public void CrouchDownScale()
    {
        playerGameobject.transform.localScale -= new Vector3(0,0.25f,0);
        //playerGameobject.transform.Translate(new Vector3(0,-0.2f,0));
    }
    public void StandUpScale()
    {
        playerGameobject.transform.localScale = new Vector3(1,1,1);
        //playerGameobject.transform.Translate(new Vector3(0,0.25f,0));
    }
}