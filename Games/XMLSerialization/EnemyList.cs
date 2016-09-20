using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerialization
{
    class EnemyList
    {
        #region Fields
        Enemy enemy;
        List<Enemy> enemyList = new List<Enemy>();
        int currentGameElapsedTime = 0;

        #endregion

        #region Constructor

        public EnemyList(Enemy enemy)
        {
            this.enemy = enemy;
        }

        #endregion

        #region Properties

        public Enemy this[int key]
        {
            get { return enemyList[key]; }
            set { enemyList[key] = value; }
        }

        #endregion

        #region Methods

        void AddEnemy(Enemy enemy)
        {
            enemyList.Add(enemy);
        }

        void checkVisibility()
        {
            for (int i = 0; i < enemyList.Count; ++i)
            {
                if (!enemyList[i].IsVisible)
                    enemyList.RemoveAt(i);
            }
        }

        public void Update(GameTime gameTime)
        {
            currentGameElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (currentGameElapsedTime >= GameConstants.enemySpawnTime)
            {
                AddEnemy(new Enemy(enemy.Texture));
                currentGameElapsedTime = 0;
            }
            checkVisibility();
            foreach (Enemy en in enemyList)
                en.Update(gameTime);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemyList)
                enemy.Draw(spriteBatch);
        }

        #endregion


    }
}
