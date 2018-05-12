using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetGame.UI
{
    public class UIDef
    {
        public static string UITitle = "TitlePage";

        public static string UIGame = "GamePage";

        public static string UIOver = "OverPage";
    }
    public class UIManager
    {
        static UIManager m_instance = null;
        public static UIManager getInstance()
        {
            if (m_instance == null)
            {
                m_instance = new UIManager();
            }

            return m_instance;
        }

        private List<UIPanel> m_listLoadedPanel = new List<UIPanel>();

        UIPage m_currentPage = null;

        private T Load<T>(string name) where T : UIPanel
        {
            T ui = UIRoot.Find<T>(name);

            if (ui == null)
            {
                GameObject original = UIRes.LoadPrefab(name);
                if (original != null)
                {
                    GameObject go = GameObject.Instantiate(original);
                    ui = go.GetComponent<T>();
                    if (ui != null)
                    {
                        go.name = name;
                        UIRoot.AddChild(ui);
                    }
                    else
                    {
                        Debug.LogError("Load() Prefab没有增加对应组件: " + name);
                    }
                }
                else
                {
                    Debug.LogError("Load() Res Not Found: " + name);
                }
            }

            if (ui != null)
            {
                if (m_listLoadedPanel.IndexOf(ui) < 0)
                {
                    m_listLoadedPanel.Add(ui);
                }
            }

            return ui;
        }

        private T Open<T>(string name, object arg = null) where T : UIPanel
        {
            T ui = Load<T>(name);
            if (ui != null)
            {
                ui.Open(arg);
            }

            return ui;
        }

        public void OpenPage(string page, object arg = null)
        {
            CloseAllLoadedPanels();

            Open<UIPage>(page, arg);
        }

        private void CloseAllLoadedPanels()
        {
            for (int i = 0; i < m_listLoadedPanel.Count; i++)
            {
                if (m_listLoadedPanel[i].IsOpen)
                {
                    m_listLoadedPanel[i].Close();
                }
            }
        }
    }
}
