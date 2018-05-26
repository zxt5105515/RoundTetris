using PlanetGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using PlanetGame;

namespace PlanetGame.Planet
{
    public class PlanetControlRot : Sensor<ScrollSpeed>, IPlanetState
    {
        GamePlanet m_Planet;

        float m_PlantRotateSpeed = -90.0f;

        float m_LastedRot = 0.0f;

        public PlanetControlRot(GamePlanet plant)
        {
            m_Planet = plant;
            this.Init();

            m_LastedRot = 0;

            m_bChangeToRunning = false;
        }

        ~PlanetControlRot()
        {
            Uninit();
        }

        public void Enter()
        {
            //throw new NotImplementedException();
        }

        public void Execute(float dt)
        {
            //throw new NotImplementedException();
            if(m_bChangeToRunning)
            {
                m_bChangeToRunning = false;

                PlantRunning running = new PlantRunning(this.m_Planet);
                running.m_PlantRotateSpeed = m_LastedRot;
                m_Planet.ChangeState(running);
            }
        }

        public void Exit()
        {
            //throw new NotImplementedException();
            ScrollSpeed tt = new ScrollSpeed();
            tt.scrollType = ScrollSpeed.ScrollType.Null;
        }

        bool m_bChangeToRunning = false;
        public override void OnEvent<GameEvent>(GameEvent evt)
        {
            ScrollSpeed t = evt as ScrollSpeed;
            if (t == null) return;

            System.Object[] ps = t.GetParam();

            if(t.scrollType == ScrollSpeed.ScrollType.Rotate)
            {
                if (ps != null && ps.Length > 0)
                {
                    //在控制中 继续
                    if ((bool)ps[1])
                    {
                        float moveoffset = (float)ps[0];

                        float h = Camera.main.pixelHeight;
                        float strength = moveoffset / h;

                        float rot = strength * m_PlantRotateSpeed;

                        m_Planet.UpdateGameRotate(rot);
                        //m_PlantRotateSpeed = speed;                    
                    }
                    else
                    {
                        float moveoffset = (float)ps[0];

                        float h = Camera.main.pixelHeight;
                        float strength = moveoffset / h;

                        float rot = strength * m_PlantRotateSpeed;

                        m_LastedRot = rot;

                        m_bChangeToRunning = true;
                    }

                }
            }
            else if(t.scrollType == ScrollSpeed.ScrollType.Move)
            {
                Vector3 moveoffset = (Vector3)ps[0];
                m_Planet.UpdateGamePosition(moveoffset);
            }
        }
    }
}
