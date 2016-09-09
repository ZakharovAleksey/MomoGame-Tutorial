using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame
{
    /// <summary>
    /// Class in which only dropping object content stores
    /// </summary>
    class DropObjectStates
    {
        #region Fields

        // Massive of possible states of droping object
        Texture2D[] states;
        // Max number of droping object states
        int statesCount;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor with one parameter: store max number of droping object states and allocate memory for massive of possible states
        /// </summary>
        /// <param name="statesCount"> max number of droping object states </param>
        public DropObjectStates(int statesCount)
        {
            this.statesCount = statesCount;
            states = new Texture2D[this.statesCount];
        }

        #endregion


        #region Properties

        /// <summary>
        /// Return max number of droping object possible states
        /// </summary>
        public int StatesCount
        {
            get { return statesCount; }
        }

        /// <summary>
        /// Return texture of droping object with currentStateID index
        /// </summary>
        /// <param name="currentStateID"> index of state </param>
        /// <returns></returns>
        public Texture2D this[int currentStateID]
        {
            get
            {
                if (currentStateID >= 0 && currentStateID <= statesCount)
                    return states[currentStateID];
                else
                    throw new IndexOutOfRangeException();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load textures for all possible droping object states
        /// </summary>
        /// <param name="Content"> current content </param>
        public void LoadContent(ContentManager Content)
        {
            for (int i = 0; i < statesCount; ++i)
            {
                string filePath = @"sword\sword" + (i + 1).ToString();
                states[i] = Content.Load<Texture2D>(filePath);
            }
        }

        #endregion
    }

    class DropObject
    {
        #region Fields

        // Store all textures for animation of droping
        DropObjectStates states;
        // index of droping object current state in animation process
        int currentState;

        // Current position of droping object
        Vector2 position;

        // true if droping object is in the screen area
        bool isVisible;

        // Look at spriteEffect inital position if left - drop to left, if right drop to right
        SpriteEffects currentSpriteEffect;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor with three parametres:
        /// Initilize position, current state of droping object
        /// </summary>
        /// <param name="states"> DropObjectStates object witch store all textures for droping object animation process </param>
        /// <param name="position"> Initial position of droping object </param>
        public DropObject(DropObjectStates states, Vector2 position, SpriteEffects currentSpriteEffect)
        {
            this.position = position;
            this.states = states;

            isVisible = false;

            currentState = 0;

            this.currentSpriteEffect = currentSpriteEffect;
        }

        #endregion

        #region Properties

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update position and idex of droping object 
        /// </summary>
        /// <param name="gameTime"> current game time </param>
        /// <param name="spritePosition"> store position of sprite witch drop this object </param>
        public void Update(GameTime gameTime, Vector2 spritePosition)
        {
            if (isVisible)
            {
                // If we drop to the right
                if (currentSpriteEffect == SpriteEffects.None)
                {
                    // while current object in the screen set it to the next animation frame
                    position.X += GameConstants.droppingObjectVelocityX * gameTime.ElapsedGameTime.Milliseconds;
                    ++currentState;

                    if (currentState >= states.StatesCount)
                        currentState = 0;

                    // If current object leaves screen 
                    if (position.X > GameConstants.WindowWidth)
                    {
                        // Set it invisible beacause we did not drop it yet
                        isVisible = false;
                        // Set initial position for droping is equal to current sprite position
                        position = spritePosition;
                    }
                }
                // If we drop to the left
                else if (currentSpriteEffect == SpriteEffects.FlipHorizontally)
                {
                    // while current object in the screen set it to the next animation frame
                    position.X -= GameConstants.droppingObjectVelocityX * gameTime.ElapsedGameTime.Milliseconds;
                    ++currentState;

                    if (currentState >= states.StatesCount)
                        currentState = 0;

                    // If current object leaves screen 
                    if (position.X < 0)
                    {
                        // Set it invisible beacause we did not drop it yet
                        isVisible = false;
                        // Set initial position for droping is equal to current sprite position
                        position = spritePosition;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(states[currentState], new Rectangle((int)position.X, (int)position.Y, states[currentState].Width, states[currentState].Height), Color.White);
        }

        #endregion
    }

    class DropSetOfObjects
    {
        #region Fields

        // List witch stores all current droping objects
        List<DropObject> dropObjectList;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor: allocates memory for list
        /// </summary>
        public DropSetOfObjects()
        {
            dropObjectList = new List<DropObject>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check all list objects on invisible propertie: if invisible - removes it from list
        /// </summary>
        void clearList()
        {
            for (int currentObj = 0; currentObj < dropObjectList.Count; ++currentObj)
            {
                // If droping object leaves the screen
                if (dropObjectList.ElementAt(currentObj).IsVisible == false)
                {
                    // Remove current object from the list
                    dropObjectList.RemoveAt(currentObj);
                    --currentObj;
                }

            }
        }

        /// <summary>
        /// Add new objects to list if distance from last shot is bigger then distanceForNewDrop
        /// </summary>
        /// <param name="states"> Stores sprites for drioping animation </param>
        /// <param name="position"> Store current position of sprite, witch droping object </param>
        /// <param name="distanceForNewDrop"> distance from last last droping object </param>
        public void Add(DropObjectStates states, Vector2 position, int distanceForNewDrop, SpriteEffects currentSpriteEffect)
        {
            // If list is null we need to add object in any case
            if (dropObjectList.Count == 0)
            {
                // Add new object and set it state to visible
                dropObjectList.Add(new DropObject(states, position, currentSpriteEffect));
                dropObjectList.First().IsVisible = true;
            }
            else
            {
                // If list is not empty we check distanse from last drop
                if (Math.Abs(dropObjectList.Last().Position.X - position.X) > distanceForNewDrop)
                {
                    // Add new object and set it state to visible
                    dropObjectList.Add(new DropObject(states, position, currentSpriteEffect));
                    dropObjectList.Last().IsVisible = true;
                }

            }
        }

        /// <summary>
        /// Update positions of all droping objects
        /// </summary>
        /// <param name="gameTime"> current game time </param>
        /// <param name="spritePosition"> sprinte batch </param>
        public void Update(GameTime gameTime, Vector2 spritePosition, SpriteEffects currentSpriteEffect)
        {
            // Remove all droping objects, witch leave the screen
            clearList();

            // Update all left objects
            foreach (DropObject currentDropObj in dropObjectList)
                currentDropObj.Update(gameTime, spritePosition);

        }

        /// <summary>
        /// Draw all droping objects
        /// </summary>
        /// <param name="spriteBatch"> current sprite batch </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (DropObject currentDropObj in dropObjectList)
                currentDropObj.Draw(spriteBatch);
        }

        #endregion
    }
}
