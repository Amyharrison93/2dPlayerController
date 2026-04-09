
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    private float rampedMovement = 0;
    protected void MoveHorizontal(float MoveBy, float deltaTime)
    {
        //stateMachine.playerGameobject.transform.Translate(rampedMovement*deltaTime,0,0);
        stateMachine.playerRigidbody.AddForce(new Vector2(MoveBy*deltaTime, 0),ForceMode2D.Force);
    }
    protected bool CheckIfGroundedOld()
    {
        RaycastHit2D hit;
        Debug.DrawRay(stateMachine.playerGameobject.transform.position, 
                    stateMachine.playerGameobject.transform.TransformDirection(Vector3.down), 
                    Color.green);

        hit = Physics2D.Raycast(
            stateMachine.playerGameobject.transform.position, 
            stateMachine.playerGameobject.transform.TransformDirection(Vector3.down),1,3);
        // Check if there was a hit and the distance is less than 0.51
        if (hit) 
        {
            float distance = Mathf.Abs(hit.point.y - stateMachine.playerGameobject.transform.position.y);
            if(distance < 0.52f) return true;
        }
        return false;
    }
    protected bool CheckIfGrounded()
    {
        var collider = stateMachine.playerGameobject.GetComponent<Collider2D>();
        Vector3 position = stateMachine.playerGameobject.transform.position;

        float width = collider.bounds.extents.x;
        float height = collider.bounds.extents.y;
        Vector2 frontCorner = new Vector2(position.x + width, position.y - height);
        Vector2 backCorner = new Vector2(position.x - width, position.y - height);

        Debug.DrawRay(frontCorner, Vector2.down * 0.5f, Color.green);
        Debug.DrawRay(backCorner, Vector2.down * 0.5f, Color.green);

        bool isFrontGrounded = CheckRaycast(frontCorner);
        bool isBackGrounded = CheckRaycast(backCorner);

        return isFrontGrounded || isBackGrounded;
    }
    protected bool CheckIfTouchingWall()
    {
        var collider = stateMachine.playerGameobject.GetComponent<Collider2D>();
        Vector3 position = stateMachine.playerGameobject.transform.position;

        float width = collider.bounds.extents.x;
        float height = collider.bounds.extents.y;
        Vector2 frontCorner = new Vector2(position.x + width, position.y);
        Vector2 backCorner = new Vector2(position.x - width, position.y);

        Debug.DrawRay(frontCorner, Vector2.right * 0.5f, Color.red);
        Debug.DrawRay(backCorner, Vector2.left * 0.5f, Color.red);

        bool isFrontTouchingWall = CheckRaycast(frontCorner);
        bool isBackTouchingWall = CheckRaycast(backCorner);

        return isFrontTouchingWall || isBackTouchingWall;
    }

    private bool CheckRaycast(Vector2 origin)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.5f,3); // Adjust layer mask as needed

        // Check if there was a hit and the distance is within acceptable limits
        if (hit)
        {
            float distance = Mathf.Abs(hit.point.y - origin.y);
            if (distance < 0.52f)
            {
                return true;
            }
        }

        return false;
    }
    private bool CheckCanJump()
    {
        if(stateMachine.resourceHandler.GetStamina() <= 0) return false;
        if(stateMachine.PlayerJumpCounter < stateMachine.PlayerJumpCount) return true;
        return false;
    }
    protected void OnJump()
    {
        if(!CheckCanJump()) return;
        stateMachine.resourceHandler.PerformAction(stateMachine.PlayerActionCost);
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
    private bool CheckCanDash()
    {
        if(stateMachine.resourceHandler.GetMana() <= 0) return false;
        if(stateMachine.PlayerDashDelay > stateMachine.PlayerDashTimer)return false;
        if(stateMachine.PlayerDashCounter>=stateMachine.PlayerDashCount) return false;
        return true;
    }
    protected void OnDash()
    {
        if(!CheckCanDash()) return;
        stateMachine.resourceHandler.CastSpell(stateMachine.PlayerSpellCost);
        stateMachine.SwitchState(new PlayerDashState(stateMachine));
    }
}