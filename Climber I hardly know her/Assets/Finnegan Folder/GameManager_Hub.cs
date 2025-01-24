using System;
using UnityEngine;

public class GameManager_Hub : MonoBehaviour
{
    [SerializeField] private PlayerSpawner Player1Spawn;
    private bool Player1Spawned;
    [SerializeField] private PlayerSpawner Player2Spawn;
    private bool Player2Spawned;
    
    [Header("Class Selector")]
    [SerializeField] private GameObject ClassSelectorObj;
    [SerializeField] private GameObject ClassGrid;
    [SerializeField] private GameObject[] ClassArray;
    [SerializeField] private GameObject ClassWidgetTemplate;


    private void Awake()
    {
        foreach (GameObject i in ClassArray)
        {
            GameObject newWidget = Instantiate(ClassWidgetTemplate, ClassGrid.transform);
            newWidget.GetComponent<ClassWidget>().ClassIcon.sprite = i.GetComponent<Player>().ClassIcon;
            newWidget.GetComponent<ClassWidget>().ClassName.text = i.gameObject.name;
        }
    }

    private void Update()
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
        
        if(Input.GetKeyDown(KeyCode.C))
            ToggleClassSelector();
    }

    private void ToggleClassSelector()
    {
        ClassSelectorObj.SetActive(!ClassSelectorObj.activeSelf);
    }
}
