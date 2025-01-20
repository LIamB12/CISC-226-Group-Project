using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CharacterClass;
    

    public void SpawnPlayer(Player.PlayerID id)
    {
        Instantiate(CharacterClass, transform.position, Quaternion.identity).GetComponent<Player>().playerID = id;
    }
    
}
