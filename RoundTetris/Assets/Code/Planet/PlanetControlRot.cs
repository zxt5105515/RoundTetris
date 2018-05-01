using PlanetGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
        }

        bool m_bChangeToRunning = false;
        public override void OnEvent<ScrollSpeed>(ScrollSpeed t)
        {
            System.Object[] ps = t.GetParam();
            if (ps != null && ps.Length > 0)
            {
                //在控制中 继续
                if((bool)ps[1])
                {
                    float moveoffset = (float)ps[0];

                    float h = Camera.main.pixelHeight;
                    float strength =  moveoffset / h;

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
    }
}
