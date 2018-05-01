﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetGame
{
    public class GameManager
    {
        IGameState m_GameState;

        public void InitGame()
        {
            m_GameState = new GameReady(this);
            m_GameState.Enter();
        }

        public void UpdateGame(float dt)
        {
            m_GameState.Execute(dt);
        }

        public void SetState(IGameState st)
        {
            m_GameState = st;
            if(m_GameState != null)
            {
                m_GameState.Exit();
            }
            m_GameState = st;
            m_GameState.Enter();
        }

        public IGameState GetState()
        {
            return m_GameState;
        }
    }
}
