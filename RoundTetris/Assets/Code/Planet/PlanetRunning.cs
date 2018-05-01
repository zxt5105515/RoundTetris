using PlanetGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlanetGame.Planet
{
    public class PlantRunning : IPlanetState
    {
        GamePlanet m_Plant;

        public float m_PlantRotateSpeed = 90.0f;

        public float m_ResisAcceleration = 10.0f;

        float m_CurrentSpeed = 0;
        float m_CurrentResis = 0;

        //override public void Init()
        //{
        //    base.Init();
        //}

        public PlantRunning(GamePlanet plant)
        {
            m_Plant = plant;
        }
        public void Enter()
        {
            //throw new NotImplementedException();
        }

        public void Execute(float dt)
        {
            //m_Plant.GameUpdate(dt);
            if(m_PlantRotateSpeed != 0)
            {
                Debug.LogWarning("Execute m_PlantRotateSpeed");
            }

            float rot = dt* m_PlantRotateSpeed;
            m_Plant.UpdateGameRotate(rot);

            if(m_PlantRotateSpeed > 0)
            {
                m_PlantRotateSpeed -= m_ResisAcceleration * dt;
                if(m_PlantRotateSpeed<=0)
                {
                    m_PlantRotateSpeed = 0;

                    //to planet ready
                    m_Plant.ChangeState(new PlanetReady(m_Plant));
                }
                
            }
            else if (m_PlantRotateSpeed < 0)
            {
                m_PlantRotateSpeed += m_ResisAcceleration * dt;
                if (m_PlantRotateSpeed >= 0)
                {
                    m_PlantRotateSpeed = 0;

                    //to planet ready
                    m_Plant.ChangeState(new PlanetReady(m_Plant));
                }                
            }
        }

        public void Exit()
        {
            //throw new NotImplementedException();
        }
    }
}
