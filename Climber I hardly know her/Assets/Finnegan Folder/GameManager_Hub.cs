using System;
using UnityEngine;

public class GameManager_Hub : MonoBehaviour
{
    [SerializeField] private GameObject baseCharacter;
    [SerializeField] private GameObject Player1Spawn;
    [SerializeField] private GameObject Player2Spawn;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            Instantiate(baseCharacter, Player1Spawn.transform.position, Quaternion.identity).GetComponent<Player>().playerID = Player.PlayerID.Player_1;

        if (Input.GetKey(KeyCode.I))
            Instantiate(baseCharacter, Player2Spawn.transform.position, Quaternion.identity).GetComponent<Player>().playerID = Player.PlayerID.Player_2;
    }
}
