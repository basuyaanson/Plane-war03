using Plane_warMain;


namespace Plane_war
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitialGame();
        }

        /*//顯示主控台(測試用)
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        //[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern void FreeConsole();*/

        //初始化遊戲
        public void InitialGame()
        {
            /*AllocConsole();*/
            Random r = new Random();
            SingleObject.GetSingle().AddGameObject(new BackGround(0, -700, 5));
            SingleObject.GetSingle().AddGameObject(new PlaneHero(100, 100, 5, 3, Direction.up));
           
            for (int i = 0; i < 4; i++)
            {
                SingleObject.GetSingle().AddGameObject(new EnemyPlane
                    (r.Next(0, this.Width), 100, r.Next(0, 2)));
            }

        }
        
        //新增敵人
        public void InitialEnemy()
        {
            Random r = new Random();
            SingleObject.GetSingle().AddGameObject(new EnemyPlane
                 (r.Next(0, this.Width), -200, r.Next(0, 2)));
        }




        //畫面繪製
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //當窗體被重新繪製時，會被調用
            SingleObject.GetSingle().DrwaGameObject(e.Graphics);
        }

        //計時器
        private void timerBG_Tick(object sender, EventArgs e)
        {
            this.Invalidate();//圖像刷新
            SingleObject.GetSingle().collision();
            if (SingleObject.GetSingle().EnemyList.Count <= 3)
            {
                InitialEnemy();
            }
        }

        //載入畫面時觸發
        private void Form1_Load(object sender, EventArgs e)
        {
            //將圖像繪製到緩存區，解決閃屏的問題
            this.SetStyle
                (ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw,
                true);
        }

        //當鼠標在窗口中移動
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            SingleObject.GetSingle().PH.MouseMove(e);
        }

        //按下左鍵
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            /*if(e.Button == MouseButtons.Left)
            {}*/
            SingleObject.GetSingle().PH.MouseDownLeft(e);
        }
    }
}