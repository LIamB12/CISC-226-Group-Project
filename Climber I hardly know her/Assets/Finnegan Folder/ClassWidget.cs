using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassWidget : MonoBehaviour
{
    //Should be assigned at runtime by references to player class prefab. To not set in inspector, as it's redundant.
    [HideInInspector] public Image ClassIcon;
    [HideInInspector] public TextMeshProUGUI ClassName;
    [HideInInspector] public GameObject ClassType;
}
