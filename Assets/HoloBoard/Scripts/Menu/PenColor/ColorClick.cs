using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.Menu.PenColor
{
    public class ColorClick : MonoBehaviour, IInputClickHandler {

        /// <summary>
        /// Color changed trigger event
        /// </summary>
        /// <param name="eventData">The current color of the board pen</param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            HoloBoardEventManager.Instance.EventManager<ColorEventManager>().ColorChanged(this.gameObject);
        } 
    }
}
