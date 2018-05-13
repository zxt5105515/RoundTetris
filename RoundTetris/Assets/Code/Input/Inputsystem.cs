using PlanetGame.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame
{
    public class ScrollSpeed : GameEvent
    {
        public override int EventID
        {
            get
            {
                return 1;
            }
        }
    }
    

    public class Inputsystem : MonoBehaviour
    {
        static Inputsystem m_Instance = null;
        static public Inputsystem getInstance
        {
            get
            {
                return m_Instance;
            }            
        }

        public enum m_MouseState
        {
            Mouse_Pressed,
            Mouse_Up,
        }

        m_MouseState m_MouseSt = m_MouseState.Mouse_Up;

        //Vector3 PressedPos = Vector3.zero;
        Vector2 LastPressPos = Vector2.zero;

        void Awake()
        {
            m_Instance = this;
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch(m_MouseSt)
            {
                case m_MouseState.Mouse_Pressed:
                    if(Input.GetMouseButtonUp(0))
                    {
                        m_MouseSt = m_MouseState.Mouse_Up;

                        Vector2 curpos = Input.mousePosition;
                        float offset = LastPressPos.y - curpos.y;

                        LastPressPos = curpos;

                        Debug.Log("offset up:" + offset);
                        PostEventData(offset,false);
                    }
                    else
                    {
                        //Vector3 curpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector2 curpos = Input.mousePosition;
                        float offset = LastPressPos.y - curpos.y;
                        //offset > 0 顺时针
                        //offset < 0 逆时针
                        //Camera.main.scree

                        LastPressPos = curpos;

                        if (offset != 0)
                        {
                            Debug.Log("offset pressed:" + offset);
                            if(curpos.x >Camera.main.pixelWidth/2)
                            {
                                PostEventData(offset);
                            }
                            else
                            {
                                PostEventData(-offset);
                            }
                            
                        }                        
                    }
                    break;
                case m_MouseState.Mouse_Up:
                    if (Input.GetMouseButtonDown(0))
                    {
                        m_MouseSt = m_MouseState.Mouse_Pressed;

                        LastPressPos = Input.mousePosition;

                        Debug.Log("pressed:");
                    }
                    break;
            }
            
        }

        void PostEventData(float speed, bool bControl = true)
        {
            ScrollSpeed pevent = new ScrollSpeed();
            pevent.reset();
            pevent.Sender = this.gameObject;
            pevent.SetParam(speed, bControl);

            EventCenter.getInstance.OnEvent<ScrollSpeed>(pevent);
        }
    }
}

