using UnityEngine.Events;
using System.Collections.Generic;

// Based on wmiller's Gist: https://gist.github.com/wmiller/3903205#file-events-cs
public class EventManager : Singleton<EventManager> {

  #region Fields

  public delegate void EventDelegate<T>(T e) where T : UnityEvent;
  private delegate void EventDelegate(UnityEvent e);

  private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
  private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

  #endregion

  #region Public Behaviour

  public static void StartListening<T>(EventDelegate<T> del) where T : UnityEvent {
    if (Instance.delegateLookup.ContainsKey(del))
      return;
    EventDelegate internalDelegate = (e) => del((T) e);
    Instance.delegateLookup[del] = internalDelegate;    
    EventDelegate tempDel;
    if (Instance.delegates.TryGetValue(typeof(T), out tempDel)) {
      Instance.delegates[typeof(T)] = tempDel += internalDelegate; 
    } else {
      Instance.delegates[typeof(T)] = internalDelegate;
    }
  }

  public static void StopListening<T>(EventDelegate<T> del) where T : UnityEvent {
    EventDelegate internalDelegate;
    if (Instance.delegateLookup.TryGetValue(del, out internalDelegate)) {
      EventDelegate tempDel;
      if (Instance.delegates.TryGetValue(typeof(T), out tempDel)) {
        tempDel -= internalDelegate;
        if (tempDel == null) {
          Instance.delegates.Remove(typeof(T));
        } else {
          Instance.delegates[typeof(T)] = tempDel;
        }
      }
      Instance.delegateLookup.Remove(del);
    }
  }

  public static void TriggerEvent(UnityEvent e) {
    EventDelegate del;
    if (Instance.delegates.TryGetValue(e.GetType(), out del))
      del.Invoke(e);
  }

  #endregion

}