using PlanetGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame
{
    public class GameEntrance : MonoBehaviour
    {
        UIManager m_UImgr;

        // Use this for initialization
        void Start()
        {
            GameManager.getInstance().InitGame();

            UIManager.getInstance().OpenPage(UIDef.UITitle);
        }

        // Update is called once per frame
        void Update()
        {
            GameManager.getInstance().UpdateGame(Time.deltaTime);
        }
    }
}

