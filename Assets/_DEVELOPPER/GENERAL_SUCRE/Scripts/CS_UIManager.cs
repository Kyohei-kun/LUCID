using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CS_UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UI_Interact;
    public void DrawInteractUI(string objectName)
    {
        UI_Interact.text = "Left clic to use " + objectName;
    }

    public void UnDrawInteractUI()
    {
        UI_Interact.text = "";
    }
}
