using System.Globalization;
using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.Menu.PenSize
{
    public class SizeButtonScript : MonoBehaviour, IInputClickHandler
    {
        /// <summary>
        /// Sizing scale for pen size 
        /// </summary>
        public float SizeIncreaseScale;

        /// <summary>
        /// If attached gameObject is the increase or decrease button
        /// </summary>
        public bool IsIncrease;

        /// <summary>
        /// Current board pen size
        /// </summary>
        public GameObject Size;
        

        /// <summary>
        /// When user tap on increase or decrease size button
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            float tmpSize = float.Parse(Size.GetComponent<TextMesh>().text);

            if (!(tmpSize > 0)) return;
            if (IsIncrease)
            {
                tmpSize += SizeIncreaseScale;
            }
            else
            {
                tmpSize -= SizeIncreaseScale;
            }
            Size.GetComponent<TextMesh>().text = tmpSize.ToString(CultureInfo.InvariantCulture);
            HoloBoardEventManager.Instance.EventManager<SizeEventManager>().PenSizeChanged(tmpSize);
        }
    }
}
