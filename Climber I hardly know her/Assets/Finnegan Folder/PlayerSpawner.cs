using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CharacterClass;
    

    public GameObject SpawnPlayer(Player.PlayerID id, GameObject classType)
    {
        GameObject player = Instantiate(classType, transform.position, Quaternion.identity);
        player.GetComponent<Player>().playerID = id;
        return player;
    }
    
}
