/*****************************************
 *FileName: UsingEventManager.cs
 *Author: Cat
 *E-Mail: 326578901@qq.com
 *Version: 1.0
 *UnityVersion: 2019.4.11f1
 *Date: 2021-04-11 01:15:11
 *Description: ʹ���¼�����ϵͳ
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
    /// <param name="_object">��������/the params are zipped in a object array</param>
    /// �˴���_object[0]��Ϊ��������,��object[1]�������ô���
    /// void���Ƿ���ֵ����[1]��λ��/void but return values at the position of [1]
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
        //ͨ������ĺ�����ע���¼�/register Actions by function declaration
        EventManager.Instance.RegisterEvent(EventType.Event1, DoSomeThing1);
        EventManager.Instance.RegisterEvent(EventType.Event1, DoSomeThing2);
        EventManager.Instance.RegisterEvent(EventType.Event2, DoSomeThing3);

        //ͨ��lambda���ʽע��Action/register Actions by lambda expression
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

        //�㲥�¼�/broadcast events
        EventManager.Instance.BroadcastEvent(EventType.Event1, tmpObject);
        EventManager.Instance.BroadcastEvent(EventType.Event2, tmpObject);

        //ע�������i��Object����,Ҫʹ����Ҫǿת����/notice that i is object type,so if you want to use it, type cast is necessary 
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