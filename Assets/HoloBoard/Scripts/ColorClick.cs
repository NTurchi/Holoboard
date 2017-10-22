using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace Assets.HoloBoard.Scripts
{
    public class ColorClick : MonoBehaviour, IInputClickHandler {

        /// <summary>
        /// We notify the selector of the color changement
        /// </summary>
        /// <param name="eventData">The current color of the board pen</param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            SendMessageUpwards("ColorClicked", this.gameObject);
        }
    }
}
