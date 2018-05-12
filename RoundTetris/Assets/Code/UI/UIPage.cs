using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetGame.UI
{
    public class UIPage : UIPanel
    {
        private bool m_isOpenedOnce;

        protected void OnEnable()
        {
        }

        protected void OnDisable()
        {
        }

        public sealed override void Open(object arg = null)
        {
            //m_openArg = arg;
            m_isOpenedOnce = false;

            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(arg);
            m_isOpenedOnce = true;
        }

        public sealed override void Close(object arg = null)
        {
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose(arg);
        }
    }
}
