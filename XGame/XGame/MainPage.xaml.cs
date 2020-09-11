using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XGame
{
    public partial class MainPage : ContentPage
    {
        // Player Attributes
        private XAMLEntity player;
        private float speed = 15;

        // Enemies
        private EnemySpawner spawner;

        // Game
        private float gameWidth = 0;
        private float gameHeight = 0;
        public int points = 0;

        public MainPage()
        {
            InitializeComponent();

            // Calculate Canvas Width and Height

            // Setup Enemy Spawners
            player = new XAMLEntity(PlayerBox, 30, 50, 50);
            spawner = new EnemySpawner(new View[] { EnemyBox1, EnemyBox2, EnemyBox3, EnemyBox4, EnemyBox5 });

            // Mount Spawning Thread
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(3000);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        spawner.spawnEnemy(player, gameWidth, gameHeight);
                    });
                }
            });
        }

        private void checkLogic()
        {
            // Position within screen checks
            if (player.pX < 0)
                player.pX = 0;
            if (player.pX + player.size > gameWidth)
                player.pX = gameWidth - player.size;
            if (player.pY < 0)
                player.pY = 0;
            if (player.pY + player.size > gameHeight)
                player.pY = gameHeight - player.size;

            // Enemy Intersection Checks
            foreach (var enemy in spawner.enemies)
            {
                if (enemy.intersect(player))
                {
                    Random r = new Random();
                    int rot;
                    if (r.NextDouble() > 0.5)
                        rot = 360;
                    else
                        rot = -360;

                    player.size = player.size + 5;
                    player.rotate(rot);

                    spawner.despawn(enemy);

                    points++;
                    PointsLabel.Text = "Points: " + points;
                }
            }
        }

        private void Left_Clicked(object sender, EventArgs e)
        {
            player.pX = player.pX - speed;
            checkLogic();
        }
        private void Right_Clicked(object sender, EventArgs e)
        {
            player.pX = player.pX + speed;
            checkLogic();
        }
        private void Up_Clicked(object sender, EventArgs e)
        {
            player.pY = player.pY - speed;
            checkLogic();
        }
        private void Down_Clicked(object sender, EventArgs e)
        {
            player.pY = player.pY + speed;
            checkLogic();
        }
        private void Size_Clicked(object sender, EventArgs e)
        {
            player.size = player.size - 5;
            checkLogic();
        }

        private void Canvas_Size_Changed(object sender, EventArgs e)
        {
            gameWidth = (float)Canvas.Width;
            gameHeight = (float)Canvas.Height;
        }
    }
}
