using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    private PlayerStateMachine playerStateMachine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerStateMachine = collision.GetComponent<PlayerStateMachine>();
        if(playerStateMachine == null) return;
        if(playerStateMachine.PlayerRespawnPoint != transform.position)
        {
            playerStateMachine.SetRespawnPoint(playerStateMachine.transform.position);
            Debug.Log("new spawn point set");
        }
    }
}

