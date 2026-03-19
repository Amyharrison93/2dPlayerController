using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    private PlayerStateMachine playerStateMachine;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerStateMachine = collision.collider.GetComponent<PlayerStateMachine>();
        if(playerStateMachine == null) return;
        if(playerStateMachine.PlayerRespawnPoint != transform.position)
        {
            playerStateMachine.SetRespawnPoint(transform.position);
            Debug.Log("new spawn point set");
        }
    }
}

