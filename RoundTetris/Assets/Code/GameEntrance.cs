using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame
{
    public class GameEntrance : MonoBehaviour
    {
        GameManager m_Game;

        // Use this for initialization
        void Start()
        {
            m_Game = new GameManager();
            m_Game.InitGame();
        }

        // Update is called once per frame
        void Update()
        {
            m_Game.UpdateGame(Time.deltaTime);
        }
    }
}

