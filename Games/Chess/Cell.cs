using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Cell
    {
        #region Fields

        int cellType;

        Texture2D cellTexture;
        Rectangle cellPosition;

        int currentCellState = (int) CellStates.IDLE;
        const int cellStattesCount = 3;

        #endregion

        #region Conststructor

        public Cell(int cellType, Rectangle cellPosition)
        {
            this.cellType = cellType;
            this.cellPosition = cellPosition;
        }

        #endregion

        #region Properties

        int Width
        {
            get { return cellTexture.Width / cellStattesCount; }
        }

        int Height
        {
            get { return cellTexture.Height; }
        }

        #endregion


        #region Methods

        public void loadContent(ContentManager Content)
        {
            if (cellType == (int)CellType.WHITE)
                cellTexture = Content.Load<Texture2D>(GameConstants.whiteCellStatesPath);
            else
                cellTexture = Content.Load<Texture2D>(GameConstants.blackCellStatesPath);
        }

        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                currentCellState = (int)CellStates.SELECTED;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle spriteRectangle = new Rectangle(currentCellState * Width, 0, Width, Height);

            spriteBatch.Draw(cellTexture, cellPosition, spriteRectangle, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
        }

        #endregion

    }
}
