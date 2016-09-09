using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame
{
    public static class GameConstants
    {
        #region Player Constants

        public const float playerVelocityX = 0.15f;
        public const float playerVelocityY = 0.45f;

       
        // Total time in milliseconds during which player could jump 
        public const float totalJumpingTime = 400;

        #endregion

        #region Dropping object parameters

        public const float droppingObjectVelocityX = 0.25f;
        public const float droppingObjectVelocityY = 0.0f;

        public const int droppingObjectStatesCount = 6;
        #endregion

        #region Physics

        public const float gravityAcceleration = 0.1f;

        #endregion

        #region Window parameters

        public const int WindowWidth = 800;
        public const int WindowHeight = 600;

        #endregion
    }
}
