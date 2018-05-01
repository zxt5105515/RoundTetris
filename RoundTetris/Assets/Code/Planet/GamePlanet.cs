using PlanetGame.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlanetGame.Planet
{    
    public class GamePlanet : MonoBehaviour
    {
        IPlanetState m_planetstate;
        // Update is called once per frame
        void Update()
        {
            
        }

        public void GameUpdate(float dt)
        {
            m_planetstate.Execute(dt);
        }

        public void UpdateGameRotate(float rot)
        {
            this.transform.Rotate(new Vector3(0, 0, 1), rot);
        }

        public void ChangeState(IPlanetState st)
        {
            if(m_planetstate == st)
            {
                return;
            }

            if(m_planetstate != null)
            {
                m_planetstate.Exit();
            }
            m_planetstate = st;

            m_planetstate.Enter();
        }
    }
}

