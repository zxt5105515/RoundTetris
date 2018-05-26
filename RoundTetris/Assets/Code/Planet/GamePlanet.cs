using PlanetGame.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlanetGame;

namespace PlanetGame.Planet
{
    public class GamePlanet : MonoBehaviour
    {
        IPlanetState m_planetstate;

        Dictionary<string, Bean.Bean> allBeans = new Dictionary<string, Bean.Bean>();

        Bean.Bean createInitBean()
        {
            Bean.Bean bean = Bean.Bean.CreateIns(Bean.Bean.BeanType.Disable);
            bean.State = Bean.Bean.BeanState.In_Planet;
            bean.transform.localPosition = Vector2.zero;
            bean.transform.parent = this.transform;
            bean.gameObject.SetActive(true);
            bean.planet = this;
     
            return bean;
        }

        // Use this for initialization
        void Start()
        {
            //clean all 
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            //中心
            Bean.Bean bean = createInitBean();
            bean.row = 0;
            bean.col = 0;
            allBeans["0_0"] = bean;

            for (int index = 0; index < 6; index++)
            {
                Bean.Bean round = createInitBean();
                Vector2 correctDt = Utils.getVecByAngle(index*60) * (bean.getRadius() + round.getRadius());
                round.transform.localPosition = bean.transform.localPosition + (Vector3)correctDt;

                AddBean(bean, round);
            }

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void AddBean(Bean.Bean touchBean, Bean.Bean bean)
        {
            bean.transform.parent = transform;
            bean.planet = this;

            //校验修正位置
            Vector2 dt = bean.transform.localPosition - touchBean.transform.localPosition;
            float ang = Utils.getAngleByVec2(dt);

            int index = Mathf.FloorToInt((ang + 30) / 60) % 6;
            float correctAng = index * 60;

            
            Vector2 correctDt = Utils.getVecByAngle(correctAng)*(bean.getRadius() + touchBean.getRadius());
            bean.transform.localPosition = touchBean.transform.localPosition + (Vector3)correctDt;

            //加入roundbeans
            //touchBean.roundBeans[index] = bean;

            //int otherSideIndex = (index + 3)%6;
            //bean.roundBeans[otherSideIndex] = touchBean;

            //行列
            int[] coord = getCoordByIndex(touchBean.row, touchBean.col, index);

            int newRow = coord[0];
            int newCol = coord[1];
            
            bean.row = newRow;
            bean.col = newCol;

            string key = newRow.ToString() + "_" + newCol.ToString();
            allBeans[key] = bean;
        }

        public void CheckDrop()
        {
            List<Bean.Bean> matchBeans = new List<Bean.Bean>();

            var centerBean = allBeans["0_0"];
            matchBeans.Add(centerBean);
            centerBean.dye_hasCheck = true;
            DyeCheckLink(centerBean, matchBeans);

            foreach (Bean.Bean round in matchBeans)
            {
                round.dye_hasCheck = false;
            }

            if (matchBeans.Count != allBeans.Count)
            {
                Dictionary<string, bool> remainBeans = new Dictionary<string, bool>();
                foreach (Bean.Bean round in matchBeans)
                {
                    string key = round.row.ToString() + "_" + round.col.ToString();
                    remainBeans[key] = true;
                }

                List<Bean.Bean> dropBeans = new List<Bean.Bean>();
                foreach (KeyValuePair<string, Bean.Bean> kv in allBeans)
                {
                    if (!remainBeans.ContainsKey(kv.Key))
                    {
                        dropBeans.Add(kv.Value);
                    }
                }

                foreach (Bean.Bean round in dropBeans)
                {
                    string key = round.row.ToString() + "_" + round.col.ToString();
                    allBeans.Remove(key);
                    round.OnDropFromPlanet();
                }
            }
        }

        public void CheckCombine(Bean.Bean bean)
        {
            List<Bean.Bean> checkedBeans = new List<Bean.Bean>();
            List<Bean.Bean> matchBeans = new List<Bean.Bean>();
            matchBeans.Add(bean);
            checkedBeans.Add(bean);
            bean.dye_hasCheck = true;
            DyeCheckCombine(bean, bean.Type, checkedBeans, matchBeans);

            foreach (Bean.Bean round in checkedBeans)
            {
                round.dye_hasCheck = false;
            }
            checkedBeans.Clear();

            if (matchBeans.Count >= 3)
            {
                //三消
                foreach (Bean.Bean round in matchBeans)
                {
                    string key = round.row.ToString() + "_" + round.col.ToString();
                    allBeans.Remove(key);
                    round.OnCombineDone();
                }
                matchBeans.Clear();

                //掉落
                CheckDrop();
            }
        }

        void DyeCheckCombine(Bean.Bean bean, Bean.Bean.BeanType beanType 
            ,List<Bean.Bean> checkedBeans ,List<Bean.Bean> matchBeans)
        {
            List<Bean.Bean> beans = getRoundBeans(bean);
            foreach(Bean.Bean round in beans)
            {
                if (round.dye_hasCheck) continue;
                round.dye_hasCheck = true;
                checkedBeans.Add(round);
                if (round.Type == beanType)
                {
                    matchBeans.Add(round);
                    DyeCheckCombine(round, beanType, checkedBeans ,matchBeans);
                }                   
            }
        }

        //相连接的
        void DyeCheckLink(Bean.Bean bean ,List<Bean.Bean> matchBeans)
        {
            List<Bean.Bean> beans = getRoundBeans(bean);
            foreach (Bean.Bean round in beans)
            {
                if (round.dye_hasCheck) continue;
                round.dye_hasCheck = true;
                matchBeans.Add(round);
                DyeCheckLink(round, matchBeans);              
            }
        }

        public List<Bean.Bean> getRoundBeans(Bean.Bean bean)
        {
            List<Bean.Bean> beans = new List<Bean.Bean>();

            for(int index = 0;index < 6; index++)
            {
                int[] coord = getCoordByIndex(bean.row, bean.col, index);
                string key = coord[0].ToString() + "_" + coord[1].ToString();
                if (allBeans.ContainsKey(key))
                {
                    var find = allBeans[key];
                    beans.Add(find);
                }
            }

            return beans;
        }

        //index:0~5，共6个，角度:0 60 120 ...
        public static int[] getCoordByIndex(int row ,int col ,int index)
        {
            int[] nums = new int[2];

            bool isOddRow = row % 2 != 0;
            int newRow = row, newCol = col;
            if (index == 0)
            {
                newCol++;
            }
            else if (index == 1)
            {
                newRow++;
                if (isOddRow) newCol++;
            }
            else if (index == 2)
            {
                newRow++;
                if (!isOddRow) newCol--;
            }
            else if (index == 3)
            {
                newCol--;
            }
            else if (index == 4)
            {
                newRow--;
                if (!isOddRow) newCol--;
            }
            else if (index == 5)
            {
                newRow--;
                if (isOddRow) newCol++;
            }

            nums[0] = newRow;
            nums[1] = newCol;

            return nums;
        }

        public void GameUpdate(float dt)
        {
            m_planetstate.Execute(dt);
        }

        public void UpdateGameRotate(float rot)
        {
            this.transform.Rotate(new Vector3(0, 0, 1), rot);
        }

        public void UpdateGamePosition(Vector3 moveOffset)
        {
            this.transform.localPosition += moveOffset;
        }

        public void ChangeState(IPlanetState st)
        {
            if(m_planetstate == st)
            {
                return;
            }

            if(m_planetstate != null)
            {
                m_planetstate.Exit();
            }
            m_planetstate = st;

            m_planetstate.Enter();
        }
    }
}

