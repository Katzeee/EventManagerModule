/*****************************************
 *FileName: EventManager.cs
 *Author: Cat
 *E-Mail: 326578901@qq.com
 *Version: 1.0
 *UnityVersion: 2019.4.11f1
 *Date: 2021-04-11 00:09:08
 *Description: 事件管理器
*****************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class EventManager
{
    #region 单例模式/Singleton
    private static EventManager instance;
    private EventManager()
    {
        allEvents = new Dictionary<EventType, Action<object[]>>();//Initialize Dictionary stroing events/初始化储存事件的字典
    }
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }
    #endregion

    //因为TryGetValue返回值的copy,所以不能在此使用,仅用来作为TryGetValue的第二个参数传入
    //as TryGetValue returns the copy of the value in Dictionary, we won't use this variable, only use as the second param
    private Action<object[]> tmpEvent;


    /// <summary>
    /// dictonary for storing all events, the first EventType is event identifier, followed by a Func function
    /// 储存全部事件的字典,第一个EventType是事件标识符,后面是委托delegate函数
    /// </summary>
    private Dictionary<EventType, Action<object[]>> allEvents;

    /// <summary>
    /// register event with EnventManager
    /// 注册事件到管理器
    /// </summary>
    /// <param name="_eventType">枚举事件类型/the type of evnet</param>
    /// <param name="_action">委托函数/Func function</param>
    public void RegisterEvent(EventType _eventType, Action<object[]> _action)
    {
        if (allEvents.TryGetValue(_eventType, out tmpEvent))//ContainsKey花费事件更多/ContainsKey uses more time
        {
            //tmpEvent += _action;
            //allEvents[_eventType] = tmpEvent;
            allEvents[_eventType] += _action;
        }
        else
        {
            allEvents.Add(_eventType, _action);
        }
    }


    /// <summary>
    /// unregister evnet
    /// 注销事件
    /// </summary>
    /// <param name="_eventType">枚举事件类型/the type of evnet</param>
    /// <param name="_action">委托函数/Func function</param>
    public void UnregisterEvent(EventType _eventType, Action<object[]> _action)
    {
        if (allEvents.TryGetValue(_eventType, out tmpEvent))
        {
            allEvents[_eventType] -= _action;
        }
    }


    /// <summary>
    /// broadcast event
    /// 广播事件
    /// </summary>
    /// <param name="_eventType">枚举事件类型/the type of evnet</param>
    /// <param name="_object">委托函数需要的参数/the parameters need by Func functions</param>
    /// <returns>没有返回值,如果需要委托返回什么,请做引用reference传入/no returns, if you need return something, please use reference</returns>
    public void BroadcastEvent(EventType _eventType, object[] _object = null)
    {
        if (allEvents.TryGetValue(_eventType, out tmpEvent))
        {
            allEvents[_eventType](_object);
        }
        else
        {
            Debug.LogError("No Events named " + _eventType.ToString() + ", please check!!");
            return;
        }
    }



    /// <summary>
    /// delete event by type
    /// 按照类型删除事件
    /// </summary>
    /// <param name="_eventType">枚举事件类型/the type of evnet</param>
    public void DelEventByType(EventType _eventType)
    {
        if (allEvents.TryGetValue(_eventType, out tmpEvent))
        {
            allEvents.Remove(_eventType);
        }
    }



    /// <summary>
    /// delete all events
    /// 删除所有事件
    /// </summary>
    public void DelEventAll()
    {
        allEvents.Clear();
        allEvents.Clear();
    }
}
