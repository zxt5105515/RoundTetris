using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlanetGame.UI
{
    public class UIRoot : MonoBehaviour
    {
        public static T Find<T>() where T : MonoBehaviour
        {
            string name = typeof(T).Name;
            GameObject obj = Find(name);
            if (obj != null)
            {
                return obj.GetComponent<T>();
            }

            return null;
        }

        public static T Find<T>(string name) where T : MonoBehaviour
        {
            GameObject obj = Find(name);
            if (obj != null)
            {
                return obj.GetComponent<T>();

            }

            return null;
        }

        public static GameObject Find(string name)
        {
            Transform obj = null;
            GameObject root = FindUIRoot();
            if (root != null)
            {
                obj = root.transform.Find(name);
            }

            if (obj != null)
            {
                return obj.gameObject;
            }

            return null;
        }

        public static GameObject FindUIRoot()
        {
            GameObject root = GameObject.Find("UIRoot");
            if (root != null && root.GetComponent<UIRoot>() != null)
            {
                return root;
            }
            return null;
        }

        public static void AddChild(UIPanel child)
        {
            GameObject root = FindUIRoot();
            if (root == null || child == null)
            {
                return;
            }


            child.transform.SetParent(root.transform, false);
            return;
        }
    }
}
