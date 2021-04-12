/*****************************************
 *FileName: EventManager.cs
 *Author: Cat
 *E-Mail: 326578901@qq.com
 *Version: 1.0
 *UnityVersion: 2019.4.11f1
 *Date: 2021-04-11 00:09:08
 *Description: �¼�������
*****************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class EventManager
{
    #region ����ģʽ/Singleton
    private static EventManager instance;
    private EventManager()
    {
        allEvents = new Dictionary<EventType, Action<object[]>>();//Initialize Dictionary stroing events/��ʼ�������¼����ֵ�
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

    //��ΪTryGetValue����ֵ��copy,���Բ����ڴ�ʹ��,��������ΪTryGetValue�ĵڶ�����������
    //as TryGetValue returns the copy of the value in Dictionary, we won't use this variable, only use as the second param
    private Action<object[]> tmpEvent;


    /// <summary>
    /// dictonary for storing all events, the first EventType is event identifier, followed by a Func function
    /// ����ȫ���¼����ֵ�,��һ��EventType���¼���ʶ��,������ί��delegate����
    /// </summary>
    private Dictionary<EventType, Action<object[]>> allEvents;

    /// <summary>
    /// register event with EnventManager
    /// ע���¼���������
    /// </summary>
    /// <param name="_eventType">ö���¼�����/the type of evnet</param>
    /// <param name="_action">ί�к���/Func function</param>
    public void RegisterEvent(EventType _eventType, Action<object[]> _action)
    {
        if (allEvents.TryGetValue(_eventType, out tmpEvent))//ContainsKey�����¼�����/ContainsKey uses more time
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
    /// ע���¼�
    /// </summary>
    /// <param name="_eventType">ö���¼�����/the type of evnet</param>
    /// <param name="_action">ί�к���/Func function</param>
    public void UnregisterEvent(EventType _eventType, Action<object[]> _action)
    {
        if (allEvents.TryGetValue(_eventType, out tmpEvent))
        {
            allEvents[_eventType] -= _action;
        }
    }


    /// <summary>
    /// broadcast event
    /// �㲥�¼�
    /// </summary>
    /// <param name="_eventType">ö���¼�����/the type of evnet</param>
    /// <param name="_object">ί�к�����Ҫ�Ĳ���/the parameters need by Func functions</param>
    /// <returns>û�з���ֵ,�����Ҫί�з���ʲô,��������reference����/no returns, if you need return something, please use reference</returns>
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
    /// ��������ɾ���¼�
    /// </summary>
    /// <param name="_eventType">ö���¼�����/the type of evnet</param>
    public void DelEventByType(EventType _eventType)
    {
        if (allEvents.TryGetValue(_eventType, out tmpEvent))
        {
            allEvents.Remove(_eventType);
        }
    }



    /// <summary>
    /// delete all events
    /// ɾ�������¼�
    /// </summary>
    public void DelEventAll()
    {
        allEvents.Clear();
        allEvents.Clear();
    }
}
