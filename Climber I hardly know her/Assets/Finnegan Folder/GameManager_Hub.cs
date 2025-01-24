using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Hub : MonoBehaviour
{
    [SerializeField] private PlayerSpawner Player1Spawn;
    [SerializeField] private PlayerSpawner Player2Spawn;
    private bool Player1Spawned;
    private bool Player2Spawned;
    private GameObject Player1;
    private GameObject Player2;
    
    [Header("Class Selector")]
    [SerializeField] private GameObject ClassListCanvas;
    [SerializeField] private GameObject ClassGrid;
    [SerializeField] private GameObject[] ClassArray;
    [SerializeField] private GameObject ClassWidget;
    private ClassSelector ClassSelector_Player1;
    private ClassSelector ClassSelector_Player2;
    [SerializeField] private GameObject ClassSelectionWidget_Player1;
    [SerializeField] private GameObject ClassSelectionWidget_Player2;
    
    
    private class ClassSelector
    {
        public int ClassSelectionIndex;
        public GameObject ClassSelectionWidget;
    }
    
    
    private void Start()
    {
        foreach (GameObject i in ClassArray)
        {
            GameObject newWidget = Instantiate(ClassWidget, ClassGrid.transform);
            newWidget.GetComponent<ClassWidget>().ClassIcon.sprite = i.GetComponent<Player>().ClassIcon;
            newWidget.GetComponent<ClassWidget>().ClassName.text = i.gameObject.name;
            newWidget.GetComponent<ClassWidget>().ClassType = i.gameObject;
        }
        
        ClassSelector_Player1 = new ClassSelector(); 
        ClassSelector_Player2 = new ClassSelector();

        ClassSelector_Player1.ClassSelectionWidget = ClassSelectionWidget_Player1;
        ClassSelector_Player2.ClassSelectionWidget = ClassSelectionWidget_Player2;

        
        
        StartCoroutine(WaitForFrame());

    }
    
    IEnumerator WaitForFrame()
    {
        yield return null; 
        ShiftClassSelector(ClassSelector_Player1, 0);
        ShiftClassSelector(ClassSelector_Player2, 0);
    }

    private void Update()
    {
        if(!Player1)
            Player1 = Player1Spawn.SpawnPlayer(Player.PlayerID.Player_1, GameInstance.ClassType_Player1);        
        
        if(!Player2)
            Player2 = Player2Spawn.SpawnPlayer(Player.PlayerID.Player_2, GameInstance.ClassType_Player2);

        
        
        if(Input.GetKeyDown(KeyCode.C))
            ToggleClassSelector();

        if (ClassListCanvas.activeSelf)
        {
            if (Input.GetKeyDown(Player1.GetComponent<Player>().key_Up))
                ShiftClassSelector(ClassSelector_Player1, -10);
            if (Input.GetKeyDown(Player1.GetComponent<Player>().key_Down))
                ShiftClassSelector(ClassSelector_Player1, 10);
            if (Input.GetKeyDown(Player1.GetComponent<Player>().key_Left))
                ShiftClassSelector(ClassSelector_Player1, -1);
            if (Input.GetKeyDown(Player1.GetComponent<Player>().key_Right))
                ShiftClassSelector(ClassSelector_Player1, 1);
            
            
            if (Input.GetKeyDown(Player2.GetComponent<Player>().key_Up))
                ShiftClassSelector(ClassSelector_Player2, -10);
            if (Input.GetKeyDown(Player2.GetComponent<Player>().key_Down))
                ShiftClassSelector(ClassSelector_Player2, 10);
            if (Input.GetKeyDown(Player2.GetComponent<Player>().key_Left))
                ShiftClassSelector(ClassSelector_Player2, -1);
            if (Input.GetKeyDown(Player2.GetComponent<Player>().key_Right))
                ShiftClassSelector(ClassSelector_Player2, 1);
        }


    }

    private void ToggleClassSelector()
    {
        ClassListCanvas.SetActive(!ClassListCanvas.activeSelf);
        
        Player1.GetComponent<Player>().immobilized = ClassListCanvas.activeSelf;
        Player2.GetComponent<Player>().immobilized = ClassListCanvas.activeSelf;
        
        ShiftClassSelector(ClassSelector_Player1, 0);
        ShiftClassSelector(ClassSelector_Player2, 0);
    }   
    
    
    private void ShiftClassSelector(ClassSelector selector, int amount)
    {
        selector.ClassSelectionIndex += amount;

       
        if (selector.ClassSelectionIndex < 0 || selector.ClassSelectionIndex > ClassGrid.transform.childCount-1)
            selector.ClassSelectionIndex -= amount;

        selector.ClassSelectionWidget.transform.position = ClassGrid.transform.GetChild(selector.ClassSelectionIndex).transform.position;
        
        
        GameInstance.ClassType_Player1 = ClassGrid.transform.GetChild(ClassSelector_Player1.ClassSelectionIndex).GetComponent<ClassWidget>().ClassType;
        GameInstance.ClassType_Player2 = ClassGrid.transform.GetChild(ClassSelector_Player2.ClassSelectionIndex).GetComponent<ClassWidget>().ClassType;
    }
}
