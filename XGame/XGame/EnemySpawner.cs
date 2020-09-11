using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XGame
{
    class EnemySpawner
    {

        private List<XAMLEntity> _enemies;
        public XAMLEntity[] enemies { get { return _enemies.ToArray(); } }

        private Dictionary<XAMLEntity, bool> enemySpawnMap;

        public EnemySpawner(View[]enemiesViewBoxArr)
        {
            _enemies = new List<XAMLEntity>();
            enemySpawnMap = new Dictionary<XAMLEntity, bool>();

            foreach (View eBox in enemiesViewBoxArr)
            {
                _enemies.Add(new XAMLEntity(eBox, 30, 0, 0));
            }

            hideEnemies();
        }

        private void hideEnemies()
        {
            foreach (var e in enemies)
            {
                despawn(e);
            }
        }


        public void spawnEnemy(XAMLEntity player, float gameWidth, float gameHeight)
        {
            Random r = new Random();
            foreach (var e in enemies)
            {
                if (!enemySpawnMap[e])
                {
                    float nx, ny;
                    do
                    {
                        nx = (float)(r.NextDouble() * (gameWidth-30));
                        ny = (float)(r.NextDouble() * (gameHeight-30));
                    } while (dist(nx,ny,player.pX,player.pY) < 50);

                    e.pX = nx;
                    e.pY = ny;
                    enemySpawnMap[e] = true;

                    break;
                }
            }
        }

        public void despawn(XAMLEntity enemy)
        {
            enemy.pX = -100;
            enemy.pY = -100;
            enemySpawnMap[enemy] = false;
        }


        private float dist(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}
