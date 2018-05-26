using PlanetGame.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame
{
    public class ScrollSpeed : GameEvent
    {
        public enum ScrollType
        {
            Null,
            Rotate,
            Move,
        }

        //public ScrollType scrollType = ScrollType.Null;
        ScrollType _scrollType = ScrollType.Null;
        public ScrollType scrollType
        {
            get { return _scrollType; }
            set { _scrollType = value; }
        }

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

        void OnGUI()
        {
            if(Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.C))
            {                
                float speed = 10* (Input.GetKey(KeyCode.X) ? 1 : -1);
                if (Input.GetKey(KeyCode.Z))
                {
                    //加速
                    speed *= 2;
                }
                PostRotEventData(speed);
            }

            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)
                || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                //移动
                Vector3 move = Vector3.zero;

                float moveSpeed = 1.0f/60;

                if (Input.GetKey(KeyCode.UpArrow))
                    move.y = 1;
                else if (Input.GetKey(KeyCode.DownArrow))
                    move.y = -1;

                if (Input.GetKey(KeyCode.LeftArrow))
                    move.x = -1;
                else if (Input.GetKey(KeyCode.RightArrow))
                    move.x = 1;

                move.Normalize(); 
                move*= moveSpeed;
                
                PostMoveEventData(move);
            }
        }


        // Update is called once per frame
        void Update()
        {
            //switch(m_MouseSt)
            //{
            //    case m_MouseState.Mouse_Pressed:
            //        if(Input.GetMouseButtonUp(0))
            //        {
            //            m_MouseSt = m_MouseState.Mouse_Up;

            //            Vector2 curpos = Input.mousePosition;
            //            float offset = LastPressPos.y - curpos.y;

            //            LastPressPos = curpos;

            //            Debug.Log("offset up:" + offset);
            //            PostRotEventData(offset,false);
            //        }
            //        else
            //        {
            //            //Vector3 curpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //            Vector2 curpos = Input.mousePosition;
            //            float offset = LastPressPos.y - curpos.y;
            //            //offset > 0 顺时针
            //            //offset < 0 逆时针
            //            //Camera.main.scree

            //            LastPressPos = curpos;

            //            if (offset != 0)
            //            {
            //                Debug.Log("offset pressed:" + offset);
            //                if(curpos.x >Camera.main.pixelWidth/2)
            //                {
            //                    PostRotEventData(offset);
            //                }
            //                else
            //                {
            //                    PostRotEventData(-offset);
            //                }

            //            }                        
            //        }
            //        break;
            //    case m_MouseState.Mouse_Up:
            //        if (Input.GetMouseButtonDown(0))
            //        {
            //            m_MouseSt = m_MouseState.Mouse_Pressed;

            //            LastPressPos = Input.mousePosition;

            //            Debug.Log("pressed:");
            //        }
            //        break;
            //}

        }

        void PostRotEventData(float speed, bool bControl = true)
        {
            ScrollSpeed pevent = new ScrollSpeed();
            pevent.reset();
            pevent.scrollType = ScrollSpeed.ScrollType.Rotate;
            pevent.Sender = this.gameObject;
            pevent.SetParam(speed, bControl);

            EventCenter.getInstance.OnEvent<ScrollSpeed>(pevent);
        }

        void PostMoveEventData(Vector3 move, bool bControl = true)
        {
            ScrollSpeed pevent = new ScrollSpeed();
            pevent.reset();
            pevent.scrollType = ScrollSpeed.ScrollType.Move;
            pevent.Sender = this.gameObject;
            pevent.SetParam(move, bControl);

            EventCenter.getInstance.OnEvent<ScrollSpeed>(pevent);
        }
    }
}

