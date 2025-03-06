using UnityEngine;

public class GameManager_Core : MonoBehaviour
{

    [SerializeField] private PlayerSpawner Player1Spawn;
    [SerializeField] private PlayerSpawner Player2Spawn;
    private GameObject Player1;
    private GameObject Player2;

    private void Start() {

    }             

    private void Update() {

        if(!Player1) {
            Debug.Log("SPAWNING 1");
            Player1 = Player1Spawn.SpawnPlayer(Player.PlayerID.Player_1, GameInstance.ClassType_Player1);        
            Debug.Log("SPAWN 1");
        }

        if(!Player2) {
            Debug.Log("SPAWNING 1");
            Player2 = Player2Spawn.SpawnPlayer(Player.PlayerID.Player_2, GameInstance.ClassType_Player2);
            Debug.Log("SPAWN 1");
        }

    }
}