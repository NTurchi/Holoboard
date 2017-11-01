using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.Menu
{
    public class SubsetMenuClickScript : MonoBehaviour, IInputClickHandler
    {
        /// <summary>
        /// When user on subset menu
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            HoloBoardEventManager.Instance.EventManager<MenuEventManager>().SubsetMenuClicked(this.gameObject);
        }
    }
}
