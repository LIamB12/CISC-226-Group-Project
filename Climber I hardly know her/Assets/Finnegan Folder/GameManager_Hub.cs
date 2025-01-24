using System;
using System.Diagnostics;
using UnityEngine;

public class GameManager_Hub : MonoBehaviour
{
    [SerializeField] private PlayerSpawner Player1Spawn;
    private GameObject Player1;
    private bool Player1Spawned;
    [SerializeField] private PlayerSpawner Player2Spawn;
    private GameObject Player2;
    private bool Player2Spawned;
    
    [Header("Class Selector")]
    [SerializeField] private GameObject ClassListCanvas;
    [SerializeField] private GameObject ClassGrid;
    [SerializeField] private GameObject[] ClassArray;
    [SerializeField] private GameObject ClassWidgetTemplate;
    private int ClassSelectionIndex_Player1;
    private int ClassSelectionIndex_Player2;
    [SerializeField]private GameObject ClassSelectionWidget_Player1;
    [SerializeField]private GameObject ClassSelectionWidget_Player2;
    
    
    
    
    private void Awake()
    {
        foreach (GameObject i in ClassArray)
        {
            GameObject newWidget = Instantiate(ClassWidgetTemplate, ClassGrid.transform);
            newWidget.GetComponent<ClassWidget>().ClassIcon.sprite = i.GetComponent<Player>().ClassIcon;
            newWidget.GetComponent<ClassWidget>().ClassName.text = i.gameObject.name;
        }
        
        ShiftClassSelector(0, 0);

    }

    private void Update()
    {
        if (Player1Spawned == false && Input.GetKey(KeyCode.W))
        {
            Player1 = Player1Spawn.SpawnPlayer(Player.PlayerID.Player_1);
            Player1Spawned = true;
        }

        if (Player2Spawned == false && Input.GetKey(KeyCode.I))
        {
            Player2 = Player2Spawn.SpawnPlayer(Player.PlayerID.Player_1);
            Player2Spawned = true;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
            ToggleClassSelector();

        if (ClassListCanvas.activeSelf && Player1Spawned)
        {
            if (Input.GetKeyDown(Player1.GetComponent<Player>().key_Up))
                ShiftClassSelector(0, -10);
            if (Input.GetKeyDown(Player1.GetComponent<Player>().key_Down))
                ShiftClassSelector(0, 10);
            if (Input.GetKeyDown(Player1.GetComponent<Player>().key_Left))
                ShiftClassSelector(0, -1);
            if (Input.GetKeyDown(Player1.GetComponent<Player>().key_Right))
                ShiftClassSelector(0, 1);
        }

    }

    private void ToggleClassSelector()
    {
        ClassListCanvas.SetActive(!ClassListCanvas.activeSelf);
    }   
    
    private void ShiftClassSelector(int player, int amount)
    {
        if (player == 0)
        {
            ClassSelectionIndex_Player1 += amount;
            
            if (ClassSelectionIndex_Player1 < 0)
                ClassSelectionIndex_Player1 = 0;
            if (ClassSelectionIndex_Player1 > ClassGrid.transform.childCount-1)
                ClassSelectionIndex_Player1 = ClassGrid.transform.childCount-1;

            ClassSelectionWidget_Player1.transform.position = ClassGrid.transform.GetChild(ClassSelectionIndex_Player1).transform.position;
        }
    }
}
