using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame
{
    public interface IGameState
    {
        void Enter();

        void Exit();

        void Execute(float dt);      
    }
}

