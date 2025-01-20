using System;
using UnityEngine;

public class GameManager_Hub : MonoBehaviour
{
    [SerializeField] private PlayerSpawner Player1Spawn;
    private bool Player1Spawned;
    [SerializeField] private PlayerSpawner Player2Spawn;
    private bool Player2Spawned;


    private void FixedUpdate()
    {
        if (Player1Spawned == false && Input.GetKey(KeyCode.W))
        {
            Player1Spawn.SpawnPlayer(Player.PlayerID.Player_1);
            Player1Spawned = true;
        }

        if (Player2Spawned == false && Input.GetKey(KeyCode.I))
        {
            Player2Spawn.SpawnPlayer(Player.PlayerID.Player_2);
            Player2Spawned = true;
        }
    }
}
