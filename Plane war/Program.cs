using Plane_war;
using System;

namespace Plane_warMain
{
    //��V�T�|��
    enum Direction
    {
        up, down, left, right
    }

    //�Ҧ��C����H������
    class  GameObject 
        {
            //�C�������y��
            public int x
            {
                get; set;
            }
            public int y
            {
                get;
                set;
            }
            
            //���󪺤j�p
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
            
            //��¦�ƭ�
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

            //�غc�l
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

        //��ø�ƥ�(�i�Ƽg)
        public virtual void Draw(Graphics g)
            { 
            }

        //�I���˴�
        public Rectangle GetRectangle()
            {
                return new Rectangle(this.x, this.y, this.Width,this.Height);
            }

        //���ʨƥ�
        public virtual void Move()
            {
            //�q�L��g�y�СA�Φ��첾�ĪG
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
               /* //�P�_�O�_�W�L����
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
        
    //�C���I��
    class BackGround:GameObject
        {
            //�غc�l
            public BackGround(int x, int y,int speed) 
                : base(x, y, BkImg.Width,BkImg.Height, speed, 0, Direction.down)
            {
            }
            //�ޤJ�Ϥ�
            private static Image BkImg = Plane_war.Resource1.bk2;
            //��ø�ƥ�
            public override void Draw(Graphics g)
            {

            this.Move();
            //���F�����A���s�`��
            if (this.y == 0)
            {
                this.y = -700;
            }
            //ø�s
                g.DrawImage(BkImg, this.x, this.y);
            }
    }
        
    //�C���}�l�᪺�ߤ@��H
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
        //�C���I����H
        public BackGround bg
        {
                get;set;
        }

        //���a������H
        public PlaneHero PH
        {
            get; set;
        }
        
        //�ĤH������H
        public List<EnemyPlane> EnemyList = new List<EnemyPlane>();

        //�ĤH������H
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
            //�ˬd ���a�l�u �P �ĤH
            for(int i = 0; i < HeroBulletList.Count;i++)
            {
                for (int j = 0; j < EnemyList.Count; j++)
                {
                    if (HeroBulletList[i].GetRectangle()//�p�G�l�u���x�λP�ؼЬۥ�
                        .IntersectsWith(EnemyList[j].GetRectangle()))
                    {
                        EnemyList[j].Hp -= HeroBulletList[i].damage;
                        EnemyList[j].IsDead();
                        HeroBulletList.Remove(HeroBulletList[i]);
                        break;//����`�����X

                    }
                }
            }
            //�ˬd �ĤH�l�u  �P ���a
            for (int i = 0; i < EnemyBulletList.Count; i++)
            {
               if(EnemyBulletList[i].GetRectangle()//�p�G�l�u���x�λP�ؼЬۥ�
                        .IntersectsWith(this.PH.GetRectangle()))
               {
                    this.PH.IsDead();
                    EnemyBulletList.Remove(EnemyBulletList[i]);

               }


            }
           
            //�ˬd �ĤH  �P ���a
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i].GetRectangle()//�p�G�l�u���x�λP�ؼЬۥ�
                         .IntersectsWith(this.PH.GetRectangle()))
                {
                    EnemyList[i].Hp = 0;
                    EnemyList[i].IsDead();
                }


            }

            //�ˬd ���a�l�u �P �ĤH�l�u
            for (int i = 0; i < HeroBulletList.Count; i++)
            {
                for (int j = 0; j < EnemyBulletList.Count; j++)
                {
                    if (HeroBulletList[i].GetRectangle()//�p�G�l�u���x�λP�ؼЬۥ�
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