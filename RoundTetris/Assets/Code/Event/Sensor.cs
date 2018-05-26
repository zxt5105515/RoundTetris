using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlanetGame.Event
{
    public abstract class Sensor<T> : BaseSensor where T : GameEvent, new()
    {
        virtual public void Init()
        {
            EventCenter.getInstance.RigisterSensor<T>(this);
        }

        virtual public void Uninit()
        {
            EventCenter.getInstance.UnRigisterSensor<T>(this);
        }

        /////////////////BaseSensor interface imp  

        //是否激活  
        bool _enableSensor = true;
        public bool EnableSensor
        {
            get { return _enableSensor; }
            set { _enableSensor = value; }
        }

        int priority = 0;
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        int _eventid = 0;
        public int EventID
        {
            get
            {
                if (_eventid == 0)
                {
                    T t = new T();
                    _eventid = t.EventID;
                }
                return _eventid;
            }
        }

        //GameObject BaseSensor.gb
        //{
        //    get
        //    {
        //        return gameObject;
        //    }
        //}

        ///////////////  

        public int CompareTo(BaseSensor other) { return other.Priority - priority; }

        public abstract void OnEvent<T1>(T1 t) where T1 : GameEvent;

    }
}
