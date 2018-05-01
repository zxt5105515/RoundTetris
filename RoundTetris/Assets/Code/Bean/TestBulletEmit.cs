using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBulletEmit : MonoBehaviour {

    public GameObject m_Bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            GameObject newbullet = Instantiate(m_Bullet);

            newbullet.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            newbullet.SetActive(true);
        }
	}
}
