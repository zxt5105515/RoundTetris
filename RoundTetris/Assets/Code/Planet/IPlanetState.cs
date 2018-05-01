using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame.Planet
{
    public interface IPlanetState
    {
        void Enter();

        void Execute(float dt);

        void Exit();
    }
}

