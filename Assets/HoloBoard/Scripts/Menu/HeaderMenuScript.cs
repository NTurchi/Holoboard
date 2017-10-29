using System.Collections.Generic;
using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.Menu
{
    public class HeaderMenuScript : MonoBehaviour, IInputClickHandler
    {
        /// <summary>
        /// When user tap on heade menu (GameObject with "menu" label)
        /// </summary>
        /// <param name="eventData">Event Data</param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            HoloBoardEventManager.Instance.EventManager<MenuEventManager>().MenuHeaderClicked();
        }
    }
}
