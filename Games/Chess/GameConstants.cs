using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    static class GameConstants
    {
        #region Window parameners

        public const int WindowWidth = 800;
        public const int WindowHeight = 800;

        #endregion

        #region Cell parametres

        public const string blackCellStatesPath = @"Cells\blackCellsStates";
        public const string whiteCellStatesPath = @"Cells\blackCellsStates";


        #endregion
    }

    enum CellType
    {
        WHITE = 0,
        BLACK = 1
    }

    enum CellStates
    {
        IDLE = 0,
        POSSIBLE = 1,
        SELECTED = 2
    }
}
