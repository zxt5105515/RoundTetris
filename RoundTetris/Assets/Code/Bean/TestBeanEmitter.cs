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

        float readyEmit = 1.0f;
        
        void Awake()
        {
            m_Circle = this.gameObject.GetComponent<CircleCollider2D>();
        }

        public void EmitBean()
        {
            Vector2 pos = FindOneEmitPos();

            int type = UnityEngine.Random.Range((int)Bean.BeanType.Min, (int)Bean.BeanType.Max + 1);

            //debug
            //type = 1;

            Bean newbullet = Bean.CreateIns((Bean.BeanType)type);

            newbullet.State = Bean.BeanState.To_Stage;

            //add
            newbullet.transform.parent = this.transform;
            newbullet.gameObject.SetActive(true);
            newbullet.transform.localPosition = pos;

            //add force
            Rigidbody2D body = newbullet.GetComponent<Rigidbody2D>();
            Vector2 force = (Vector2)m_Circle.bounds.center - pos;
            force.Normalize();
            force *= 150;
            body.AddForce(force);
        }

        Vector2 FindOneEmitPos()
        {
            float x = UnityEngine.Random.Range(-1.0f, 1.0f);
            float y = UnityEngine.Random.Range(-1.0f, 1.0f);

            //debug
            //x = 1;
            //y = 0;

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
