using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    /// <summary>
    /// All subset menu (Ex: ColorSubsetMenu)
    /// </summary>
    public List<GameObject> SubsetSelectorMenu;

    /// <summary>
    /// Reset opened selector menu
    /// </summary>
    public void ResetOpenedSubsetSelectorMenu(GameObject sender)
    {
        foreach (GameObject menu in SubsetSelectorMenu)
        {
            if (menu != sender)
            {
                menu.SendMessage("DisableSelectorMenu");
            }
        }
    }
}
