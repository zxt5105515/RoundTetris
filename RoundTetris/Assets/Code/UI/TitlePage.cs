using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetGame;

namespace PlanetGame.UI
{
    public class TitlePage : UIPage
    {
        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);
            //UpdateUserInfo();
        }

        private void EnterGame()
        {
            GameManager.getInstance().SetState(new GameRunning(GameManager.getInstance()));
            UIManager.getInstance().OpenPage(UIDef.UIGame);
        }

        public void OnBtnStart()
        {
            //是否要wait一会儿进入啥的 逻辑写在这个之前
            EnterGame();
        }
    }
}
