using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerialization
{
    static class GameConstants
    {
        // Window Parameters
        public const int WindowWidth = 800;
        public const int WindowHeight = 500;

        // Animation constants

        public const int MillisecondsPerSprite = 200;

        // Player constants
        public const float playerVelocityX = 0.2f;
        public const float playerVelocityY = 0.2f;


        // Moving epsilon


        // Enemy

        // Velocity
        public const float enemyVelocityX = -0.1f;
        public const float enemyVelocityY = 0.1f;
        //
        public const int enemyCount = 4;
        public const int enemySpawnTime = 200;


        public static Random rand = new Random();
    }
}
