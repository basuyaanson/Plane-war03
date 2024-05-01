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

        /*//��ܥD���x(���ե�)
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        //[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern void FreeConsole();*/

        //��l�ƹC��
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
        
        //�s�W�ĤH
        public void InitialEnemy()
        {
            Random r = new Random();
            SingleObject.GetSingle().AddGameObject(new EnemyPlane
                 (r.Next(0, this.Width), -200, r.Next(0, 2)));
        }




        //�e��ø�s
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //����Q���sø�s�ɡA�|�Q�ե�
            SingleObject.GetSingle().DrwaGameObject(e.Graphics);
        }

        //�p�ɾ�
        private void timerBG_Tick(object sender, EventArgs e)
        {
            this.Invalidate();//�Ϲ���s
            SingleObject.GetSingle().collision();
            if (SingleObject.GetSingle().EnemyList.Count <= 3)
            {
                InitialEnemy();
            }
        }

        //���J�e����Ĳ�o
        private void Form1_Load(object sender, EventArgs e)
        {
            //�N�Ϲ�ø�s��w�s�ϡA�ѨM�{�̪����D
            this.SetStyle
                (ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw,
                true);
        }

        //���Цb���f������
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            SingleObject.GetSingle().PH.MouseMove(e);
        }

        //���U����
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            /*if(e.Button == MouseButtons.Left)
            {}*/
            SingleObject.GetSingle().PH.MouseDownLeft(e);
        }
    }
}