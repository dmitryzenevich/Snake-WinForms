using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snake.Classes;

namespace Snake
{
    public partial class Form1 : Form
    {
        private GameController gameController;
        private int baseInterval;
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.DoubleBuffer, true);

            gameController = GameController.Instance;
            baseInterval = gameUpdater.Interval;

            gameController.Redraw += GameControllerOnRedraw;
            gameController.EatFoodEvent += GameControllerOnEatFoodEvent;
            gameController.ResetEvent += GameControllerOnResetEvent;
        }

        private void GameControllerOnRedraw()
        {
            Refresh();
        }

        private void GameControllerOnEatFoodEvent()
        {
            if (gameUpdater.Interval > 350)
                gameUpdater.Interval -= 5;
        }

        private void GameControllerOnResetEvent()
        {
            gameUpdater.Interval = baseInterval;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gameController.DrawObjects(e.Graphics);
        }

        private void gameUpdater_Tick(object sender, EventArgs e)
        {
            gameController.GameLoop();
            Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    gameController.SetDirection(Vector2.left);
                    break;
                case Keys.Left:
                    gameController.SetDirection(Vector2.left);
                    break;
                case Keys.D:
                    gameController.SetDirection(Vector2.right);
                    break;
                case Keys.Right:
                    gameController.SetDirection(Vector2.right);
                    break;
                case Keys.W:
                    gameController.SetDirection(Vector2.up);
                    break;
                case Keys.Up:
                    gameController.SetDirection(Vector2.up);
                    break;
                case Keys.S:
                    gameController.SetDirection(Vector2.down);
                    break;
                case Keys.Down:
                    gameController.SetDirection(Vector2.down);
                    break;
                default:
                    break;
            }
            Refresh();
        }
    }
}
