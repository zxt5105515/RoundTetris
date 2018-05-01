using PlanetGame.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlanetGame.Planet
{
    public class PlanetReady : Sensor<ScrollSpeed>, IPlanetState
    {
        GamePlanet m_Planet;
        public PlanetReady(GamePlanet plant)
        {
            UnityEngine.Debug.Log("PlanetReady Created");
            m_Planet = plant;
            this.Init();

            m_bChangeToRunning = false;
        }

        ~PlanetReady()
        {
            Uninit();
        }


        public void Enter()
        {
            //throw new NotImplementedException();
        }

        public void Execute(float dt)
        {
            if (m_bChangeToRunning)
            {
                m_bChangeToRunning = false;
                m_Planet.ChangeState(new PlanetControlRot(m_Planet));
            }
        }

        public void Exit()
        {
            //throw new NotImplementedException();
        }

        bool m_bChangeToRunning = false;
        public override void OnEvent<ScrollSpeed>(ScrollSpeed t)
        {
            System.Object[] ps = t.GetParam();
            if (ps != null && ps.Length > 0)
            {
                //在控制中 继续
                if ((bool)ps[1])
                {
                    m_bChangeToRunning = true;
                    
                }
                
            }
        }
    }
}
