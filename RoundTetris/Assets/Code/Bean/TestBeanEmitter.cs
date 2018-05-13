using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlanetGame.Bean
{
    public class TestBeanEmitter : MonoBehaviour
    {
        CircleCollider2D m_Circle;

        public GameObject[] m_Bullet;

        float readyEmit = 1.0f;
        
        void Awake()
        {
            m_Circle = this.gameObject.GetComponent<CircleCollider2D>();
        }

        public void EmitBean()
        {
            Vector2 pos = FindOneEmitPos();

            if(m_Bullet != null && m_Bullet.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, m_Bullet.Length);

                GameObject newbullet = Instantiate(m_Bullet[index]);

                newbullet.SetActive(true);

                newbullet.transform.position = pos;
            }
        }

        Vector2 FindOneEmitPos()
        {
            float x = UnityEngine.Random.Range(-1.0f, 1.0f);
            float y = UnityEngine.Random.Range(-1.0f, 1.0f);
            Vector2 dir = new Vector2(x, y);
            dir.Normalize();
            Vector2 offset = dir * m_Circle.radius;

            return new Vector2(m_Circle.bounds.center.x + offset.x, m_Circle.bounds.center.y + offset.y);
        }

        public void UpdateEmit(float dt)
        {
            readyEmit -= dt;
            if(readyEmit <= 0)
            {
                readyEmit = 0.5f;

                EmitBean();
            }
        }
    }
}
