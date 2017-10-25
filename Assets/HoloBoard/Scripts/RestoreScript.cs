using System.Globalization;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace Assets.HoloBoard.Scripts
{
	public class RestoreScript : MonoBehaviour, IInputClickHandler
	{
		/// <summary>
		/// The application Board
		/// </summary>
		public GameObject Board;

		/// <summary>
		/// When user tap on the remove button
		/// </summary>
		/// <param name="eventData"></param>
		public void OnInputClicked(InputClickedEventData eventData)
		{
			Board.SendMessage("RestoreBoard");
		}
	}
}
