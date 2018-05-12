using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame.UI
{
    public class UIPanel : MonoBehaviour
    {

        public virtual void Open(object arg = null)
        {
        }

        public virtual void Close(object arg = null)
        {
        }

        public bool IsOpen { get { return this.gameObject.activeSelf; } }


        /// <summary>
        /// 当UI关闭时，会响应这个函数
        /// 该函数在重写时，需要支持可重复调用
        /// </summary>
        protected virtual void OnClose(object arg = null)
        {
        }

        /// <summary>
        /// 当UI打开时，会响应这个函数
        /// </summary>
        /// <param name="arg"></param>
        protected virtual void OnOpen(object arg = null)
        {
        }
    }
}

