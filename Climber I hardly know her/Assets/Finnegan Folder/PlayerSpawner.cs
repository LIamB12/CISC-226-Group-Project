using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CharacterClass;
    

    public GameObject SpawnPlayer(Player.PlayerID id)
    {
        GameObject player = Instantiate(CharacterClass, transform.position, Quaternion.identity);
        player.GetComponent<Player>().playerID = id;
        return player;
    }
    
}
