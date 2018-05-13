using PlanetGame.Bean;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour {

    public Transform m_FlyCenter;

    public float m_Speed = 10.0f;

    Bean m_SelfBean;

	// Use this for initialization
	void Start () {
        m_SelfBean = this.gameObject.GetComponent<Bean>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch(m_SelfBean.State)
        {
            case Bean.BeanState.Move:
                Vector2 dir = m_FlyCenter.position - this.transform.position;
                dir.Normalize();

                this.transform.position = new Vector2(
                    this.transform.position.x + dir.x * m_Speed * Time.deltaTime,
                    this.transform.position.y + dir.y * m_Speed * Time.deltaTime);
                break;
            case Bean.BeanState.FlyAway:
                Vector2 dirback = m_FlyCenter.position - this.transform.position;
                dirback.Normalize();

                this.transform.position = new Vector2(
                    this.transform.position.x - dirback.x * m_Speed * Time.deltaTime,
                    this.transform.position.y - dirback.y * m_Speed * Time.deltaTime);
                break;
        }
        
    }
}
