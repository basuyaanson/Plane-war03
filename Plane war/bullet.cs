using Plane_warMain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane_war
{
    //子彈父類
    class bullet : GameObject
    {
        //處存子彈圖片
        private Image ImgBullet;
        //子彈傷害
        public int damage
        {
            get; set;
        }
        public bullet(PlaneFather pf, int damage, Image img, int x, int y, int speed)
        : base(pf.x+pf.Width/4 , pf.y , img.Width, img.Height, speed, 0, pf.dir)
        {
            this.damage = damage;
            this.ImgBullet = img;
        }

        public override void Draw(Graphics g)
        {
            this.Move();
            g.DrawImage(ImgBullet, this.x, this.y);
        }

        public void MoveToBorder()
        {
            if (this.y <= 0 || this.x >= 812)//離開玩家位置
            {
                SingleObject.GetSingle().RemoveGameObject(this);
            }
        }
    }

    //玩家子彈
    class HeroBullet : bullet
    {
        private static Image ImgHeroBullet = Resource1.bullet1;
        public HeroBullet(PlaneFather pf, int speed, int damage)
            : base(pf, damage, ImgHeroBullet, pf.x , pf.y, speed)
        {
        }
    }

    //敵人子彈
    class EnemyBullet : bullet
    {
        private static Image ImgEnemyBullet = Resource1.bullet2;
        public EnemyBullet(PlaneFather pf, int speed, int damage)
            : base(pf, damage, ImgEnemyBullet, pf.x + pf.Width / 2, pf.y, speed)
        {
        }
       
    }
}
