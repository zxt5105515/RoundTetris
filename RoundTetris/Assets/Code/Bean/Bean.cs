using PlanetGame.Planet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGame.Bean
{
    public class Bean : MonoBehaviour
    {
        public Sprite[] beanSprites;
        public enum BeanType
        {
            Disable = 0,
            Min = 1,

            Red = 1,
            Yellow = 2,
            Green = 3,
            Blue = 4,

            Max = 4,
        }
        public enum BeanState
        {
            To_Stage,
            In_Stage,
            In_Planet,
            Drop,
        }

        public enum LayerDef
        {
            Wall = 8,
            To_Stage_Ball = 9,
            Stage_Ball = 10,
            Planet = 11,
            Drop_Ball = 12,
        }

        BeanState m_State;
        public BeanState State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;

                //修改碰撞layer
                CircleCollider2D collider = GetComponent<CircleCollider2D>();
                Rigidbody2D body = GetComponent<Rigidbody2D>();

                if (m_State == BeanState.To_Stage){
                    gameObject.layer = (int)LayerDef.To_Stage_Ball;
                    collider.isTrigger = true;
                }
                else if (m_State == BeanState.In_Stage){
                    gameObject.layer = (int)LayerDef.Stage_Ball;
                    collider.isTrigger = false;
                }

                else if (m_State == BeanState.In_Planet){
                    gameObject.layer = (int)LayerDef.Planet;
                    collider.isTrigger = false;
                    body.bodyType = RigidbodyType2D.Static;
                }

                else if (m_State == BeanState.Drop){
                    gameObject.layer = (int)LayerDef.Drop_Ball;
                    collider.isTrigger = false;
                    body.bodyType = RigidbodyType2D.Dynamic;
                    body.gravityScale = 1;
                }
            }
        }

        BeanType m_Type;
        public BeanType Type
        {
            get
            {
                return m_Type;
            }
            set
            {
                m_Type = value;

                //更换素材
                Sprite sp = beanSprites[(int)m_Type];
                SpriteRenderer render = GetComponent<SpriteRenderer>();
                render.sprite = sp;

                //隐藏disable的球
                if (m_Type == BeanType.Disable)
                {
                    render.enabled = false;
                }
            }
        }

        //行列
        public int row = 0;
        public int col = 0;

        //for dye check
        public bool dye_hasCheck = false;        

        //Bean m_Next = null;

        Bean m_Pre = null;
        List<Bean> m_PreList = new List<Bean>();

        float waittime = 0.3f;

        float destorytime = 3.0f;

        static public Bean CreateIns(BeanType type)
        {
            GameObject prefab = Resources.Load("Planet/bean") as GameObject;
            GameObject ins = Instantiate(prefab);
            Bean bean = ins.GetComponent<Bean>();
            bean.Type = type;
            return bean;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
           
        }

        public float getRadius()
        {
            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            return collider.radius;
        }

        public GamePlanet planet = null;

        //三消
        public void OnCombineDone()
        {            
            Destroy(this.gameObject);
        }

        //掉落
        public void OnDropFromPlanet()
        {
            State = BeanState.Drop;
            //Destroy(this.gameObject);
        }

        public void OnContactPlanet(Bean b)
        {
            //add
            State = BeanState.In_Planet;

            b.planet.AddBean(b ,this);
            b.planet.CheckCombine(this);
        }        

        void OnCollisionEnter2D(Collision2D other)
        {
           //碰到planet的球
            if(State == BeanState.In_Stage)
            {
                Bean pBean = other.gameObject.GetComponent<Bean>();
                if (pBean != null)
                {
                    if(pBean.State == BeanState.In_Planet)
                    {
                        OnContactPlanet(pBean);
                    }
                    else
                    {
                        Debug.LogError("can't be happen!");
                    }

                    return;
                }
            }
            else if (State == BeanState.Drop)
            {
                //回收了
                GCWall wall = other.gameObject.GetComponent<GCWall>();
                if (wall != null)
                {
                    Destroy(this.gameObject);
                    return;
                }
            }


            //墙
            // Wall wall = other.gameObject.GetComponent<Wall>();
            // if (wall != null)
            // {
            //     OnContactPlanet(wall);
            //     return;
            // }
        }


        void OnTriggerExit2D(Collider2D other)
        {
            //进入墙
            if(State == BeanState.To_Stage)
            {
                Wall wall = other.gameObject.GetComponent<Wall>();
                if (wall != null)
                {
                    State = BeanState.In_Stage;
                    return;
                }
            }
        }

    }
}

