using PlanetGame.Planet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame.Bean
{
    public class Bean : MonoBehaviour
    {
        public enum BeanState
        {
            Move,
            Stand,
        }

        BeanState m_State;
        public BeanState State
        {
            get
            {
                return m_State;
            }
        }

        //Bean m_Next = null;

        Bean m_Pre = null;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnContactBean(Bean b)
        {
            m_State = BeanState.Stand;

            m_Pre = b;

            this.transform.parent = b.transform;
        }

        public void OnContactPlanet(GamePlanet gp)
        {
            Debug.Log("OnContactPlanet");
            m_State = BeanState.Stand;

            this.transform.parent = gp.transform;
        }
    }
}

