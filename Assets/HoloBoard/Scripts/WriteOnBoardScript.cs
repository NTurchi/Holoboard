﻿using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace Assets.HoloBoard.Scripts
{
    public class WriteOnBoardScript : MonoBehaviour, IManipulationHandler, IInputClickHandler
    {
        /// <summary>
        /// Drawing line on the board
        /// </summary>
        private readonly List<GameObject> _lines = new List<GameObject>();

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
            Debug.unityLogger.Log("TS" + source.source.kind);
            if (source.source.kind == InteractionSourceKind.Hand)
            {
                Vector3 handPos;
                source.properties.location.TryGetPosition(out handPos);

                if (source.properties.location.TryGetPosition(out handPos))
                {
                    RaycastHit hitInfo;
                    if (Physics.Raycast(handPos, Vector3.forward, out hitInfo))
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

            Debug.unityLogger.Log(_lines.Count);

            this._lines.Add(line);
            Debug.unityLogger.Log(_lines.Count);

        }

        /// <summary>
        /// Add a new point in the current drawing line
        /// </summary>
        /// <param name="position">The new point position</param>
        public void AddNewPositionToTheCurrentLine(Vector3 position)
        {
            Debug.unityLogger.Log(_lines.Count);
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
        private Vector3 GetHandBoardHitPosition(InputEventData eventData)
        {
            Vector3 position;
            eventData.InputSource.TryGetPosition(eventData.SourceId, out position);
            RaycastHit hitInfo;
            Physics.Raycast(position, Vector3.forward, out hitInfo);
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

            // Initiation du trait
            InitNewLine();
            Vector3 startPosition = this.GetHandBoardHitPosition(eventData);
            AddNewPositionToTheCurrentLine(startPosition);
        }

        /// <summary>
        /// When user is drawing event
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationUpdated(ManipulationEventData eventData)
        {
            AddNewPositionToTheCurrentLine(this.GetHandBoardHitPosition(eventData));
        }

        /// <summary>
        /// When user drawing is complete
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationCompleted(ManipulationEventData eventData)
        {
            InputManager.Instance.PopModalInputHandler();
        }

        /// <summary>
        /// When user drawing is canceled
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationCanceled(ManipulationEventData eventData)
        {
            InputManager.Instance.PopModalInputHandler();
        }

        /// <summary>
        /// When user tap on the board
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            // Dessin du point
            Vector3 position = this.GetHandBoardHitPosition(eventData);
            AddPoint(position);
        }
    }
}
