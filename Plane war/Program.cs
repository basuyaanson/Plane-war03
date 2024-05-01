using Plane_war;
using System;

namespace Plane_warMain
{
    //方向枚舉值
    enum Direction
    {
        up, down, left, right
    }

    //所有遊戲對象的父類
    class  GameObject 
        {
            //遊戲中的座標
            public int x
            {
                get; set;
            }
            public int y
            {
                get;
                set;
            }
            
            //物件的大小
            public int Width
            {
                get;
                set;
            }
            public int Height
            {
                get;
                set;
            }
            
            //基礎數值
            public int speed
            {
                get; set;
            }
            public int Hp
            {
                get; set;
            }
            public Direction dir
            {
                get;set;
            }

            //建構子
            public GameObject(int x, int y, int width, int height, int speed, int hp, Direction dir)
            {
                this.x = x;
                this.y = y;
                Width = width;
                Height = height;
                this.speed = speed;
                Hp = hp;
                this.dir = dir;
            }
            public GameObject(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

        //重繪事件(可複寫)
        public virtual void Draw(Graphics g)
            { 
            }

        //碰撞檢測
        public Rectangle GetRectangle()
            {
                return new Rectangle(this.x, this.y, this.Width,this.Height);
            }

        //移動事件
        public virtual void Move()
            {
            //通過改寫座標，形成位移效果
            switch (this.dir)
                {
            case Direction.up:
                        this.y -= this.speed;
                        break;
            case Direction.down:
                        this.y += this.speed;
                        break;
            case Direction.right:
                        this.x += this.speed;
                        break;
            case Direction.left:
                        this.x -= this.speed;
                        break;
                }
               /* //判斷是否超過視窗
                if(this.x<+ 0)
                {
                    this.x=0;
                }
                if(this.x>=400)
                {
                    this.x = 0;
                }*/
            }
    }     
        
    //遊戲背景
    class BackGround:GameObject
        {
            //建構子
            public BackGround(int x, int y,int speed) 
                : base(x, y, BkImg.Width,BkImg.Height, speed, 0, Direction.down)
            {
            }
            //引入圖片
            private static Image BkImg = Plane_war.Resource1.bk2;
            //重繪事件
            public override void Draw(Graphics g)
            {

            this.Move();
            //當到達頂部，重新循環
            if (this.y == 0)
            {
                this.y = -700;
            }
            //繪製
                g.DrawImage(BkImg, this.x, this.y);
            }
    }
        
    //遊戲開始後的唯一對象
    class SingleObject
        {
        private SingleObject()
            { }
        private static SingleObject _single = null;
        public static SingleObject GetSingle()
            { 
                if(_single == null)
                {
                    _single = new SingleObject();
                }
                return _single;
            }
        //遊戲背景對象
        public BackGround bg
        {
                get;set;
        }

        //玩家飛機對象
        public PlaneHero PH
        {
            get; set;
        }
        
        //敵人飛機對象
        public List<EnemyPlane> EnemyList = new List<EnemyPlane>();

        //敵人飛機對象
        public List<BoomEnemy> boomList = new List<BoomEnemy>();

        public List<HeroBullet> HeroBulletList = new List<HeroBullet>();
        public List<EnemyBullet> EnemyBulletList = new List<EnemyBullet>();
        public void AddGameObject(GameObject go)
            {
            if(go is BackGround)
            {
                this.bg = go as BackGround;
            }
            else if (go is PlaneHero)
            {
                this.PH = go as PlaneHero;
            }
            else if (go is EnemyPlane)
            {
                this.EnemyList.Add(go as EnemyPlane);
            }
            else if (go is HeroBullet)
            {
                HeroBulletList.Add(go as HeroBullet);
            }
            else if (go is EnemyBullet)
            {
                EnemyBulletList.Add(go as EnemyBullet);
            }
            else if (go is BoomEnemy)
            {
                boomList.Add(go as BoomEnemy);
            }

        }
        public void DrwaGameObject(Graphics g)
            {
            this.bg.Draw(g);
            this.PH.Draw(g);
            for(int i =0;i<EnemyList.Count;i++)
            {
               EnemyList[i].Draw(g);
            }
            for (int i = 0; i < HeroBulletList.Count; i++)
            {
                HeroBulletList[i].Draw(g);
            }
            for (int i = 0; i < EnemyBulletList.Count; i++)
            {
                EnemyBulletList[i].Draw(g);
            }
            for (int i = 0; i < boomList.Count; i++)
            {
                boomList[i].Draw(g);
            }

        }
        public void RemoveGameObject(GameObject go)
            {
            if (go is EnemyPlane)
            {
                EnemyList.Remove(go as EnemyPlane);
            }
            else if (go is HeroBullet)
            {
                HeroBulletList.Remove(go as HeroBullet);
            }
            else if (go is EnemyBullet)
            {
                EnemyBulletList.Remove(go as EnemyBullet);
            }
            else if (go is BoomEnemy)
            {
                boomList.Remove(go as BoomEnemy);
            }

        }
        public void collision()
        {
            //檢查 玩家子彈 與 敵人
            for(int i = 0; i < HeroBulletList.Count;i++)
            {
                for (int j = 0; j < EnemyList.Count; j++)
                {
                    if (HeroBulletList[i].GetRectangle()//如果子彈的矩形與目標相交
                        .IntersectsWith(EnemyList[j].GetRectangle()))
                    {
                        EnemyList[j].Hp -= HeroBulletList[i].damage;
                        EnemyList[j].IsDead();
                        HeroBulletList.Remove(HeroBulletList[i]);
                        break;//防止循環溢出

                    }
                }
            }
            //檢查 敵人子彈  與 玩家
            for (int i = 0; i < EnemyBulletList.Count; i++)
            {
               if(EnemyBulletList[i].GetRectangle()//如果子彈的矩形與目標相交
                        .IntersectsWith(this.PH.GetRectangle()))
               {
                    this.PH.IsDead();
                    EnemyBulletList.Remove(EnemyBulletList[i]);

               }


            }
           
            //檢查 敵人  與 玩家
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i].GetRectangle()//如果子彈的矩形與目標相交
                         .IntersectsWith(this.PH.GetRectangle()))
                {
                    EnemyList[i].Hp = 0;
                    EnemyList[i].IsDead();
                }


            }

            //檢查 玩家子彈 與 敵人子彈
            for (int i = 0; i < HeroBulletList.Count; i++)
            {
                for (int j = 0; j < EnemyBulletList.Count; j++)
                {
                    if (HeroBulletList[i].GetRectangle()//如果子彈的矩形與目標相交
                        .IntersectsWith(EnemyBulletList[j].GetRectangle()))
                    {
                        EnemyBulletList.Remove(EnemyBulletList[j]);
                        HeroBulletList.Remove(HeroBulletList[i]);
                        break;

                    }
                }
            }
        }


    }

    class boom:GameObject
    {
        public boom(int x,int y):base(x,y)
        {

        }

    }

    class BoomEnemy:boom
    {
        private Image[] img = { Resource1._15, Resource1._16, Resource1._17, Resource1._18, Resource1._19,
        Resource1._20,Resource1._21,Resource1._22,Resource1._23,Resource1._24,};

        public BoomEnemy(int x,int y):base(x,y)
        {

        }

        public override void Draw(Graphics g)
        {
            for (int i = 0; i < img.Length; i++)
            {
                g.DrawImage(img[i], this.x, this.y, img[i].Width/2, img[i].Height/2);
            }
            SingleObject.GetSingle().RemoveGameObject(this);
        }

    }

}