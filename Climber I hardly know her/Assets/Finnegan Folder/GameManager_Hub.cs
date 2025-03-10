using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private IEnumerator WaitForFrame()
    {
        //The icon widgets go to (0,0) before any frames are processed so the selection icons are stuck in the bottom left corner of the screen unless I do this hack.
        
        GameInstance.ClassType_Player1 = ClassGrid.transform.GetChild(ClassSelector_Player1.ClassSelectionIndex).GetComponent<ClassWidget>().ClassType;
        GameInstance.ClassType_Player2 = ClassGrid.transform.GetChild(ClassSelector_Player2.ClassSelectionIndex).GetComponent<ClassWidget>().ClassType;
        yield return null;
        ToggleClassSelector();
    }
    

    private void Update()
    {
        if(!Player1)
            Player1 = Player1Spawn.SpawnPlayer(Player.PlayerID.Player_1, GameInstance.ClassType_Player1);        
        
        if(!Player2)
            Player2 = Player2Spawn.SpawnPlayer(Player.PlayerID.Player_2, GameInstance.ClassType_Player2);

        
        
        if(Input.GetKeyDown(KeyCode.C))
            ToggleClassSelector();

        if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene(sceneName: "SampleScene");
        }

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

        // when class selection closes
        if (!ClassListCanvas.activeSelf){
            // gather class selection from widget
            GameObject p1_class_selection = ClassGrid.transform.GetChild(ClassSelector_Player1.ClassSelectionIndex).GetComponent<ClassWidget>().ClassType;
            GameObject p2_class_selection = ClassGrid.transform.GetChild(ClassSelector_Player2.ClassSelectionIndex).GetComponent<ClassWidget>().ClassType;

            // if selection is different from current class, change current class, kill player
            if (p1_class_selection != GameInstance.ClassType_Player1){
                GameInstance.ClassType_Player1 = p1_class_selection;
                Player1.GetComponent<Player>().TakeDamage(Player1.GetComponent<Player>().MaxHealth);  
            }
            
            // same as P1
            if (p2_class_selection != GameInstance.ClassType_Player2){
                GameInstance.ClassType_Player2 = p2_class_selection;
                Player2.GetComponent<Player>().TakeDamage(Player2.GetComponent<Player>().MaxHealth);
            }
        }
        
        GameInstance.PlayersImmobilized = ClassListCanvas.activeInHierarchy;
        
        ShiftClassSelector(ClassSelector_Player1, 0);
        ShiftClassSelector(ClassSelector_Player2, 0);

    }   
    
    
    private void ShiftClassSelector(ClassSelector selector, int amount)
    {
        selector.ClassSelectionIndex += amount;

       
        if (selector.ClassSelectionIndex < 0 || selector.ClassSelectionIndex > ClassGrid.transform.childCount-1)
            selector.ClassSelectionIndex -= amount;

        selector.ClassSelectionWidget.transform.position = ClassGrid.transform.GetChild(selector.ClassSelectionIndex).transform.position;
        
    }
}
