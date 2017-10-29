using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace Assets.HoloBoard.Scripts.Board
{
    public class WriteScript : MonoBehaviour, IManipulationHandler, IInputClickHandler
    {
        /// <summary>
        /// Drawing line on the board
        /// </summary>
        private readonly List<GameObject> _lines = new List<GameObject>();
        

        /// <summary>
        /// Cursor app
        /// </summary>
        public GameObject HoloCursor;

        /// <summary>
        /// Create a new line render on the board
        /// </summary>
        public void InitNewLine()
        {
            GameObject line = new GameObject();
            LineRenderer lineR = line.AddComponent<LineRenderer>();
            lineR.material = HoloBoard.PenColor;
            lineR.positionCount = 0;

            // Width
            lineR.startWidth = HoloBoard.PenSize;
            lineR.endWidth = HoloBoard.PenSize;
        
            // Color
            lineR.startColor = HoloBoard.PenColor.color;
            lineR.endColor = HoloBoard.PenColor.color;
            lineR.useWorldSpace = true;

            this._lines.Add(line);

        }

        /// <summary>
        /// Add a new point in the current drawing line
        /// </summary>
        /// <param name="position">The new point position</param>
        public void AddNewPositionToTheCurrentLine(Vector3 position)
        {
            LineRenderer line = this._lines.Last().GetComponent<LineRenderer>();
            line.positionCount += 1;
            line.useWorldSpace = true;
            line.SetPosition(line.positionCount - 1, new Vector3(position.x, position.y, this.gameObject.transform.parent.position.z - 0.02f));
        }

        /// <summary>
        /// Get raycast hit point result from user hand position to board
        /// </summary>
        /// <param name="position"></param>
        /// <returns>The hit point on the board</returns>
        private Vector3 GetHandBoardHitPosition(Vector3 position)
        {
            RaycastHit hitInfo;
            Physics.Raycast(position, HoloCursor.transform.position, out hitInfo);
            return hitInfo.point;
        }

        /// <summary>
        /// When user tap, this method add a line point on the board
        /// </summary>
        /// <param name="startPosition"></param>
        private void AddPoint(Vector3 startPosition)
        {
            InitNewLine();
            AddNewPositionToTheCurrentLine(startPosition);
            AddNewPositionToTheCurrentLine(new Vector3(startPosition.x + HoloBoard.PenSize, startPosition.y + HoloBoard.PenSize));
        }

        /// <summary>
        /// When user start drawing on the board
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationStarted(ManipulationEventData eventData)
        {
            // Push board on modal
            InputManager.Instance.PushModalInputHandler(this.gameObject);
           
            HoloCursor.SetActive(false);

            // Initiation du trait
            InitNewLine();
            Vector3 startPosition = this.GetHandBoardHitPosition(eventData.CumulativeDelta);
            AddNewPositionToTheCurrentLine(startPosition);
        }

        /// <summary>
        /// When user is drawing event
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationUpdated(ManipulationEventData eventData)
        {
            Vector3 pos = this.GetHandBoardHitPosition(eventData.CumulativeDelta);
            AddNewPositionToTheCurrentLine(pos);
        }

        /// <summary>
        /// When user drawing is complete
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationCompleted(ManipulationEventData eventData)
        {
            HoloCursor.SetActive(true);
            InputManager.Instance.PopModalInputHandler();
        }

        /// <summary>
        /// When user drawing is canceled
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationCanceled(ManipulationEventData eventData)
        {
            HoloCursor.SetActive(true);
            InputManager.Instance.PopModalInputHandler();
        }

        /// <summary>
        /// When user tap on the board
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            Vector3 positionClick;
            eventData.InputSource.TryGetPosition(eventData.SourceId, out positionClick);
            // Dessin du point
            Vector3 position = this.GetHandBoardHitPosition(positionClick);
            AddPoint(position);
        }
    }
}

