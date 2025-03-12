using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Platformer397
{
    public abstract class Subject : MonoBehaviour
    {
        [SerializeField] private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer) => observers.Add(observer);

        public void RemoveObserver(IObserver observer) => observers.Remove(observer);

        public void NotifyObservers ()
        {
            foreach (IObserver observer in observers)
            {
                observer.OnNotify();
            }
        }
    }
}
