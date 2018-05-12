using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlanetGame
{
    public class GameReady : IGameState
    {
        protected GameManager m_Game;
        public GameReady(GameManager game)
        {
            m_Game = game;
        }

        public void Enter()
        {
            //throw new NotImplementedException();
        }

        public void Execute(float dt)
        {
            //if(Input.GetKeyDown(KeyCode.S))
            //{
            //    m_Game.SetState(new GameRunning(this.m_Game));
            //}
        }

        public void Exit()
        {
            //throw new NotImplementedException();
        }
    }
}
