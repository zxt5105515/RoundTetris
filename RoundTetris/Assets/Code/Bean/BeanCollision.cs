using PlanetGame.Planet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame.Bean
{
    public class BeanCollision : MonoBehaviour
    {
        Bean m_SelfBean;


        // Use this for initialization
        void Start()
        {
            m_SelfBean = this.gameObject.GetComponent<Bean>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Bean pBean = other.gameObject.GetComponent<Bean>();
            if (pBean != null)
            {
                m_SelfBean.OnContactBean(pBean);
                return;
            }

            GamePlanet pPlanet = other.gameObject.GetComponent<GamePlanet>();
            if (pPlanet != null)
            {
                m_SelfBean.OnContactPlanet(pPlanet);
                return;
            }
        }
    }
}

