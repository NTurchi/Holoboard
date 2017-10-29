using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity.InputModule;

using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace Assets.HoloBoard.Scripts
{
    public class WriteOnBoardScript : MonoBehaviour, IManipulationHandler, IInputClickHandler
	{
		/// <summary>
		/// Drawing lines on the board
		/// </summary>
		private readonly List<GameObject> _lines = new List<GameObject>();

		/// <summary>
		/// Backup of drawing lines on the board
		/// </summary>
		private readonly List<GameObject> _linesBackup = new List<GameObject>();

        /// <summary>
        /// Pen color
        /// </summary>
        private Material _lineColor;

        /// <summary>
        /// Pen size
        /// </summary>
        private float _lineWidth;

        // Prochaine fonctionnalités
        public GameObject InkObj;

        /// <summary>
        /// Cursor app
        /// </summary>
        public GameObject HoloCursor;
        

        void Awake()
        {
            this._lineWidth = 0.01f;

            // Prochaines fonctionnalités
            //InteractionManager.SourceDetected += HandDetected;
            //InteractionManager.SourceUpdated += HandDetected;
        }

        // Prochaines fonctionnalités
        public void HandDetected(InteractionSourceState source)
        {
            if (source.source.kind == InteractionSourceKind.Hand)
            {
                Vector3 handPos;
                source.properties.location.TryGetPosition(out handPos);

                if (source.properties.location.TryGetPosition(out handPos))
                {
                    RaycastHit hitInfo;
                    if (Physics.Raycast(handPos, Camera.main.transform.forward, out hitInfo))
                    {
                        Vector3 pos = hitInfo.point;
                        InkObj.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, InkObj.transform.position.z);
                    }    
                }
                
            }
        }

        /// <summary>
        /// When color selector change
        /// </summary>
        /// <param name="newColor">New pen color</param>
        public void ChangeColor(Material newColor)
        {
            this._lineColor = newColor;
        }

        /// <summary>
        /// When size selector change
        /// </summary>
        /// <param name="newSize">New Size</param>
        public void ChangeSize(float newSize)
        {
            this._lineWidth = newSize;
        }

        /// <summary>
        /// Create a new line render on the board
        /// </summary>
        public void InitNewLine()
        {
            GameObject line = new GameObject();
            LineRenderer lineR = line.AddComponent<LineRenderer>();
            lineR.material = _lineColor;
            lineR.positionCount = 0;

            // Width
            lineR.startWidth = _lineWidth;
            lineR.endWidth = _lineWidth;
        
            // Color
            lineR.startColor = _lineColor.color;
            lineR.endColor = _lineColor.color;
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
        /// <param name="eventData"></param>
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
            AddNewPositionToTheCurrentLine(new Vector3(startPosition.x + _lineWidth, startPosition.y + _lineWidth));
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

		/// <summary>
		/// Removes the board and backup it content.
		/// </summary>
		public void RemoveBoard()
		{
			Debug.Log ("RemoveBoard:");
			Debug.Log ("Lines backup: " + this._linesBackup.Count());
			Debug.Log ("Lines : " + this._lines.Count());
			//Remove the backup array
			if(this._linesBackup.Count() > 0){
				foreach(GameObject obj in _linesBackup){
					Destroy (obj);
				}
				_linesBackup.Clear ();
			}
			//Disactive all the lines on the board
			foreach(GameObject obj in _lines){
				obj.SetActive (false);
			}
			this._linesBackup.AddRange(_lines);
			this._lines.Clear();
		}

		/// <summary>
		/// Restores the board.
		/// </summary>
		public void RestoreBoard()
		{
			Debug.Log ("RestoreBoard:");
			Debug.Log ("Lines backup: " + this._linesBackup.Count());
			Debug.Log ("Lines : " + this._lines.Count());
			//Don't execute the fnc if backup array is empty or lines array isnt removed
			if(this._linesBackup.Count() <= 0 || this._lines.Count() > 0){
				return;
			}
			this._lines.AddRange (_linesBackup);
			foreach(GameObject obj in _lines){
				obj.SetActive (true);
			}
		}
    }
}
