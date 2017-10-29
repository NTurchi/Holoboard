using System;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.EventManager
{
    /// <summary>
    /// Main app's event manager wich regroups all event manager
    /// </summary>
    public class HoloBoardEventManager : Singleton<HoloBoardEventManager>
    {
        /// <summary>
        /// Dictionnary of registered event manager
        /// </summary>
        private readonly Dictionary<Type, object> _managersList = new Dictionary<Type, object>();

        /// <summary>
        /// Register an event manager
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RegisterManager<T>() where T : class
        {
            if (_managersList.ContainsKey(typeof(T)))
            {
                throw new Exception("Event manager already exists");
            } 
            T instance = Activator.CreateInstance<T>();
            _managersList.Add(typeof(T), instance);
        }

        /// <summary>
        /// Return the specified <see cref="T"/> Event Manager
        /// </summary>
        /// <typeparam name="T">The specified event manger</typeparam>
        /// <returns>Event manager</returns>
        public T EventManager<T>() where T : class {
            if (!_managersList.ContainsKey(typeof(T)))
            {
               throw new Exception("Event manager not found");
            }
            return this._managersList[typeof(T)] as T;
        }

       
    }
}
