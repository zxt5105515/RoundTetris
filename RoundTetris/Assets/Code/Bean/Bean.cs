using PlanetGame.Planet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame.Bean
{
    public class Bean : MonoBehaviour
    {
        public enum BeanType
        {
            Red,
            Yellow,
            Green,
            Blue,
        }
        public enum BeanState
        {
            Move,
            WaitStand,
            Stand,
            FlyAway,
        }

        BeanState m_State;
        public BeanState State
        {
            get
            {
                return m_State;
            }
        }

        public BeanType m_Type;

        //Bean m_Next = null;

        Bean m_Pre = null;
        List<Bean> m_PreList = new List<Bean>();

        float waittime = 0.3f;

        float destorytime = 3.0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch(m_State)
            {
                case BeanState.Stand:
                    if(this.transform.parent == null)
                    {
                        m_State = BeanState.FlyAway;
                    }
                    break;
                case BeanState.WaitStand:
                    {
                        waittime -= Time.deltaTime;
                        if(waittime <= 0)
                        {
                            m_State = BeanState.Stand;
                        }
                    }
                    break;
                case BeanState.FlyAway:
                    {
                        destorytime -= Time.deltaTime;
                        if(destorytime <= 0)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                    break;
            }
        }

        void CheckCombin()
        {
            List<Bean> sametypelist = new List<Bean>();
            Combin(ref sametypelist);

            if(sametypelist.Count > 1)
            {
                for(int i=0; i< sametypelist.Count; ++i)
                {
                    //+score
                    sametypelist[i].CombinBeanScore();
                }
            }
        }

        void Combin(ref List<Bean> sametypelist)
        {
            for (int i = 0; i < m_PreList.Count; ++i)
            {
                if (m_PreList[i] != null && m_PreList[i].m_Type == m_Type)
                {
                    sametypelist.Add(m_PreList[i]);
                    m_PreList[i].Combin(ref sametypelist);
                }
            }
        }

        public void CombinBeanScore()
        {
            DestoryBean();
        }

        public void DestoryBean()
        {
            List<Transform> m_Childs = new List<Transform>();
            for(int i=0; i<this.transform.childCount; ++i)
            {
                m_Childs.Add(this.transform.GetChild(i));
            }

            for (int i = 0; i < m_Childs.Count; ++i)
            {
                m_Childs[i].parent = null;
            }

            Destroy(this.gameObject);
        }

        public void OnContactBean(Bean b)
        {
            if(m_State != BeanState.Move && m_State != BeanState.WaitStand)
            {
                return;
            }
            m_State = BeanState.WaitStand;

            m_PreList.Add(b);
            //m_Pre = b;

            this.transform.parent = b.transform;

            CheckCombin();
        }

        public void OnContactPlanet(GamePlanet gp)
        {
            Debug.Log("OnContactPlanet");
            m_State = BeanState.Stand;

            this.transform.parent = gp.transform;
        }
    }
}

