/*****************************************
 *FileName: UsingEventManager.cs
 *Author: Cat
 *E-Mail: 326578901@qq.com
 *Version: 1.0
 *UnityVersion: 2019.4.11f1
 *Date: 2021-04-11 01:15:11
 *Description: 使用事件管理系统
*****************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UsingEventManager : MonoBehaviour
{


    #region Actions
    /// <summary>
    /// DoSomeThing1
    /// </summary>
    /// <param name="_object">no params</param>
    void DoSomeThing1(object[] _object = null)
    {
        Debug.Log("I use no param!!");
    }

    /// <summary>
    /// DoSomeThing2
    /// </summary>
    /// <param name="_object">参数数组/the params are zipped in a object array</param>
    /// 此处的_object[0]作为参数传入,而object[1]当作引用传入
    /// void但是返回值存在[1]的位置/void but return values at the position of [1]
    void DoSomeThing2(object[] _object = null)
    {
        if ((int)_object[0] % 4 == 0)
        {
            _object[1] = "123";
        }
        else if ((int)_object[0] % 4 == 1)
        {
            _object[1] = 123;
        }
        else if ((int)_object[0] % 4 == 2)
        {
            _object[1] = 1.2f;
        }
        else
        {
            _object[1] = true;
        }
    }


    /// <summary>
    /// DoSomeThing3
    /// </summary>
    /// <param name="_object">return value is stored in [2]</param>
    void DoSomeThing3(object[] _object = null)
    {
        _object[2] = Vector3.zero;
    }
    #endregion



    void Start()
    {
        //通过定义的函数来注册事件/register Actions by function declaration
        EventManager.Instance.RegisterEvent(EventType.Event1, DoSomeThing1);
        EventManager.Instance.RegisterEvent(EventType.Event1, DoSomeThing2);
        EventManager.Instance.RegisterEvent(EventType.Event2, DoSomeThing3);

        //通过lambda表达式注册Action/register Actions by lambda expression
        EventManager.Instance.RegisterEvent(EventType.Event2, (object[] _object) =>
        {
            if ((int)_object[0] % 4 == 1)
            {
                _object[3] = "return value 1";
            }
            else
            {
                _object[3] = "return value 2";
            }
        });



        object[] tmpObject = new object[4];//use as params
        tmpObject[0] = 1;

        //广播事件/broadcast events
        EventManager.Instance.BroadcastEvent(EventType.Event1, tmpObject);
        EventManager.Instance.BroadcastEvent(EventType.Event2, tmpObject);

        //注意这里的i是Object类型,要使用需要强转类型/notice that i is object type,so if you want to use it, type cast is necessary 
        foreach (var i in tmpObject)
        {
            Debug.Log(i);
        }


    }

}

public enum EventType
{
    Event1,
    Event2
}