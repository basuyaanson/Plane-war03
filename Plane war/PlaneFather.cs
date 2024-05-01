using Plane_warMain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane_war
{
    //飛機父類
    class PlaneFather : GameObject
    {
        private Image PlaneImg;
        public PlaneFather(int x, int y, Image img, int speed, int hp, Direction dir)
        : base(x, y, img.Width, img.Height, speed, hp, dir)
        {
            this.PlaneImg = img;
        }
        public virtual void Fire()
        {

        }
        public virtual void IsDead()
        {

        }
    }

    //玩家飛機
    class PlaneHero : PlaneFather
    {
    private static Image imgPlane = Resource1.hero;

    public PlaneHero(int x, int y, int speed, int hp, Direction dir)
                : base(x, y, imgPlane, speed, hp, dir)
        {

        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(imgPlane, this.x, this.y, this.Width / 2, this.Height / 2);
        }
        public void MouseMove(MouseEventArgs e)
        {
            this.x = e.X;//飛機的座標等於鼠標
            this.y = e.Y;
        }
        public override void Fire()
        {
            SingleObject.GetSingle().AddGameObject(new HeroBullet(this, 20, 3));
        }

        public void MouseDownLeft(MouseEventArgs e)
        {
            Fire();

        }
        public override void IsDead()
        {

        }
    }

    //敵人飛機
    class EnemyPlane : PlaneFather
    {
        private static Image img1 = Resource1.em1;
        private static Image img2 = Resource1.em2;

        public int EnemyType
        {
            get; set;
        }

        public static Image GetImageType(int type)
        {
            switch (type)
            {
                case 0:
                    return img1;
                case 1:
                    return img2;
            }
            return null;
        }

        public static int GetHpType(int type)
        {
            switch (type)
            {
                case 0:
                    return 3;
                case 1:
                    return 5;
            }
            return 0;
        }

        public static int GetSpeedType(int type)
        {
            switch (type)
            {
                case 0:
                    return 10;
                case 1:
                    return 5;
            }
            return 0;
        }

        public EnemyPlane(int x, int y, int Type)
                : base(x, y, GetImageType(Type), GetSpeedType(Type), GetHpType(Type), Direction.down)
        {
            this.EnemyType = Type;
        }

        public override void Draw(Graphics g)
        {
            this.Move();
            this.MoveToBorder();
            switch (this.EnemyType)
            {
                case 0:
                    g.DrawImage(img1, this.x, this.y);
                    break;
                case 1:
                    g.DrawImage(img2, this.x, this.y);
                    break;
            }
            Fire();

        }

        public void MoveToBorder()
        {
            if (this.y >= 700)//離開玩家位置
            {
                SingleObject.GetSingle().RemoveGameObject(this);
            }
        }

        public override void Fire()
        {
            Random r = new Random();

            int rr = r.Next(0,100);
            if (rr >= 90)      
            {
                SingleObject.GetSingle().AddGameObject(new EnemyBullet(this, 20, 3));
            }
           
        }

        public override void IsDead()
        {
            if (this.Hp <= 0)
            {
                SingleObject.GetSingle().AddGameObject(new BoomEnemy(this.x,this.y));
                SingleObject.GetSingle().RemoveGameObject(this);
            }
        }
    }
}
