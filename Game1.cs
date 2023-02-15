using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Super_Rusty_App05;
using Super_Rusty_App05.States;

namespace App05_Super_Rusty
{
    /// <summary>
    /// This class is responsible to run the
    /// Super Rusty game,set the sprites and backgrounds.
    /// It starts the game with a main menu state
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>19th May 2022</version>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private SpriteBatch playerBatch;
        // Separate SpriteBatch for backgrounds
        // (can be removed in further refactorings)
        private SpriteBatch backgroundBatch;

        // The player
        public static Rusty rusty;

        // Checks if the game is over or not
        public bool GameOver = true;
        // Checks if player lost all lives
        public static bool lostGame = false;
        // Checks if player reached the end of the world alive
        public static bool wonGame = false;
        // Checks if the game has started
        public static bool gameStarted = false;

        // Player Lives
        private OtherSprite heart1;
        private OtherSprite heart2;

        // Timer used to display message backgrounds
        public static int Timer = 1000;

        // Scrolling and other backgrounds
        private Scrolling background;
        public Scrolling GameLostBackground;
        public Scrolling GameWonBackground;

        // Special blocks that have enemies on top
        public static Block block2;
        public static Block block7;

        public SpriteFont Arial;

        public static List<Block> blocks;
        public static List<Scrolling> Scrollings;
        private List<Beer> beers;

        // Police Enemies and special enemies
        List<Police> enemies = new List<Police>();
        private Police enemyBlock2;
        private Police enemyBlock7;
        Random random = new Random();

        // States
        private State _currentState;
        private State _nextState;

        // All the sound effects
        public static SoundEffect JumpEffect;
        public static SoundEffect BeerEffect;
        public static SoundEffect WinEffect;
        public static SoundEffect LostLifeEffect;
        public static SoundEffect BgMusic;
        public static SoundEffectInstance music;

        // Default position in the Y axis of the player 
        public const int Y_GROUND = 360;
        public const int SCREEN_WIDTH = 800;
        // Translation of the background
        public const int TRANSLATION = 3;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Change from Main Menu to game
        /// </summary>
        public void ChangeState(State state)
        {
            _nextState = state;
            music = BgMusic.CreateInstance();
            music.Play();
        }

        /// <summary>
        /// Load all the sprites
        /// </summary>
        protected override void LoadContent()
        {
            playerBatch = new SpriteBatch(GraphicsDevice);
            backgroundBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

            JumpEffect = Content.Load<SoundEffect>("jump");
            BeerEffect = Content.Load<SoundEffect>("beer_sound");
            LostLifeEffect = Content.Load<SoundEffect>("lost-life");
            WinEffect = Content.Load<SoundEffect>("Retro-games-style-winning-trumpet-sound-effect-wav");
            BgMusic = Content.Load<SoundEffect>("Mario-theme-song-wav");

            Arial = Content.Load<SpriteFont>("Ubuntu");

            background = new Scrolling(Content.Load<Texture2D>("main_menu"), new Rectangle(0, 0, 800, 480));
            GameLostBackground = new Scrolling(Content.Load<Texture2D>("gameOver"), new Rectangle(0, 0, 800, 480));
            GameWonBackground = new Scrolling(Content.Load<Texture2D>("congrats"), new Rectangle(0, 0, 800, 480));
        }

        // Seconds for spawn
        float spawn = 0;

        /// <summary>
        /// The update method will make the sprites
        /// update all their state while the game is running
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // If player is playing the game
            if (gameStarted && !GameOver)
            {
                // Check if the game is over
                IsGameOver();

                rusty.Update(JumpEffect, gameTime, blocks);

                foreach (Scrolling scrolling in Scrollings)
                    scrolling.Update();

                foreach (Block block in blocks)
                    block.Update();

                foreach (Beer beer in beers)
                {
                    beer.Update();
                }

                spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
                foreach (Police enemy in enemies)
                {
                    enemy.Update();
                }

                enemyBlock2.Update();
                enemyBlock7.Update();

                LoadEnemies();
            }

            // If player is in the main menu
            else
            {
                _currentState.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Method responsible to draw all the components and sprites
        /// and check for state changes
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);
            _currentState.Draw(gameTime, playerBatch, background);

            // If the player just lauched the game after losing it
            if (gameStarted && GameOver)
            {
                GameOver = false;
                ReloadGame();
            }

            // If the player lauched the game
            if (gameStarted && !GameOver)
            {
                backgroundBatch.Begin();
                foreach (Scrolling scrolling in Scrollings)
                    scrolling.Draw(backgroundBatch);
                // Score label
                backgroundBatch.DrawString(Arial, $"Beers: {rusty.Score}", new Vector2(10, 10), Color.Black);
                backgroundBatch.End();

                playerBatch.Begin();
                rusty.Draw(playerBatch);

                // Heart Lives for the player on the right top of the screen
                if (rusty.Lives == 2)
                {
                    heart1.Draw(playerBatch);
                    heart2.Draw(playerBatch);
                }
                if (rusty.Lives == 1)
                {
                    heart2.Draw(playerBatch);
                }
                
                foreach (Beer beer in beers)
                    if (beer.IsVisible)
                        beer.Draw(playerBatch);
                foreach (Police enemy in enemies)
                    enemy.Draw(playerBatch);

                enemyBlock2.Draw(playerBatch);
                enemyBlock7.Draw(playerBatch);

                foreach (Block block in blocks)
                    block.Draw(playerBatch);
                playerBatch.End();
            }

            playerBatch.Begin();

            // if the player lost all lives
            if (lostGame)
            {
                // check if timer has reached the end and return to main menu
                if (Timer <= 0)
                {
                    System.Threading.Thread.Sleep(2500);
                    lostGame = false;
                    gameStarted = false;
                    GameOver = true;
                    Timer = 1000;
                }
                // start the timer and display game over message
                else
                {
                    GameLostBackground.Draw(playerBatch);
                    playerBatch.DrawString(Arial, $"Drunkness: {rusty.Score * 100 / beers.Count()}%", new Vector2(300, 420), Color.Blue);
                    startTimer();
                }
            }

            // if the player ahs reached the end of the level
            if (wonGame)
            {

                if (Timer <= 0)
                {
                    System.Threading.Thread.Sleep(5000);
                    wonGame = false;
                    gameStarted = false;
                    GameOver = true;
                    Timer = 1000;
                }
                // start the timer and display game won message
                else
                {
                    GameWonBackground.Draw(playerBatch);
                    playerBatch.DrawString(Arial, $"Drunkness: {rusty.Score * 100 / beers.Count()}%", new Vector2(310, 420), Color.Blue);
                    startTimer();
                }
            }

            playerBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// If the the player lost or won the game, the method
        /// stops the background music and changes states.
        /// </summary>
        public void IsGameOver()
        {
            if (Scrolling.IsLastBackground() && rusty.Position.X >= 650)
            {
                music.Stop();
                WinEffect.Play();
                wonGame = true;
            }
            if (rusty.Lives <= 0)
            {
                music.Stop();
                lostGame = true;
            }
        }

        /// <summary>
        /// Load the ground enemies randomly each 2 seconds
        /// </summary>
        public void LoadEnemies()
        {
            int randX = random.Next(810, 900);

            // Spawn at each 2 seconds
            if (spawn >= 2)
            {
                spawn = 0;

                if (enemies.Count() < 3 && !Scrolling.IsLastBackground())
                {
                    enemies.Add(new Police(Content.Load<Texture2D>("police_man"), new Vector2(randX, Y_GROUND), false));
                }
            }

            // take the enemies out when they are out the screen
            for (int i = 0; i < enemies.Count(); i++)
            {
                if (!enemies[i].isVisible)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Load all the collectable beers in the game
        /// </summary>
        public void LoadBeers()
        {
            Beer beer1 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(460, Y_GROUND));
            Beer beer2 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(430, Y_GROUND - 190));
            Beer beer3 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(560, Y_GROUND - 190));
            Beer beer4 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(1380, Y_GROUND - 190));
            Beer beer5 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(1360, Y_GROUND));
            Beer beer6 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(1460, Y_GROUND - 300));
            Beer beer7 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(2560, Y_GROUND - 190));
            Beer beer8 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(2860, Y_GROUND - 50));
            Beer beer9 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(2860, Y_GROUND - 300));
            Beer beer10 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(3300, Y_GROUND - 100));
            Beer beer11 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(1900, Y_GROUND));
            Beer beer12 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(2050, Y_GROUND));
            Beer beer13 = new Beer(Content.Load<Texture2D>("beer_outline"), new Vector2(2990, Y_GROUND - 300));

            beers = new List<Beer>
            {
                beer1,beer2,beer3,
                beer4,beer5,beer6,
                beer7,beer8,beer9,
                beer10,beer11,beer12,
                beer13
            };
        }

        /// <summary>
        /// Loads all the block platforms in the game
        /// </summary>
        public void LoadBlocks()
        {
            Block block1 = new Block(Content.Load<Texture2D>("3_block"), new Vector2(230, Y_GROUND - 50));
            // special block (enemy on top)
            block2 = new Block(Content.Load<Texture2D>("5_block"), new Vector2(420, Y_GROUND - 150));
            Block block3 = new Block(Content.Load<Texture2D>("5_block"), new Vector2(950, Y_GROUND - 50));
            Block block4 = new Block(Content.Load<Texture2D>("3_block"), new Vector2(1300, Y_GROUND - 150));
            Block block5 = new Block(Content.Load<Texture2D>("3_block"), new Vector2(2500, Y_GROUND - 50));
            Block block6 = new Block(Content.Load<Texture2D>("5_block"), new Vector2(2650, Y_GROUND - 150));
            // special block (enemy on top)
            block7 = new Block(Content.Load<Texture2D>("5_block"), new Vector2(2850, Y_GROUND - 230));
            Block block8 = new Block(Content.Load<Texture2D>("3_block"), new Vector2(3200, Y_GROUND - 50));

            blocks = new List<Block>
            {
                block1,
                block2,
                block3,
                block4,
                block5,
                block6,
                block7,
                block8
            };
        }

        /// <summary>
        /// Loads all the backgrounds and the player
        /// </summary>
        public void LoadBackgrounds()
        {
            rusty = new Rusty(Content.Load<Texture2D>("deer_still"), new Vector2(20, Y_GROUND));

            heart1 = new OtherSprite(Content.Load<Texture2D>("final_heart"), new Vector2(700, 20));
            heart2 = new OtherSprite(Content.Load<Texture2D>("final_heart"), new Vector2(740, 20));

            Scrolling scrolling1 = new Scrolling(Content.Load<Texture2D>("background0"), new Rectangle(0, 0, 800, 500));
            Scrolling scrolling2 = new Scrolling(Content.Load<Texture2D>("background1"), new Rectangle(SCREEN_WIDTH * 1, 0, 800, 500));
            Scrolling scrolling3 = new Scrolling(Content.Load<Texture2D>("background2"), new Rectangle(SCREEN_WIDTH * 2, 0, 800, 500));
            Scrolling scrolling4 = new Scrolling(Content.Load<Texture2D>("background3"), new Rectangle(SCREEN_WIDTH * 3, 0, 800, 500));
            Scrolling scrolling5 = new Scrolling(Content.Load<Texture2D>("background4"), new Rectangle(SCREEN_WIDTH * 4, 0, 800, 500));

            Scrollings = new List<Scrolling>
            {
                scrolling1,
                scrolling2,
                scrolling3,
                scrolling4,
                scrolling5
            };
        }

        /// <summary>
        /// Reloads the game to start again
        /// </summary>
        public void ReloadGame()
        {
            for (int i = 0; i < enemies.Count(); i++)
                enemies.RemoveAt(i);

            // Load special enemies on blocks -> needs refactoring
            enemyBlock2 = new Police(Content.Load<Texture2D>("police_man"), new Vector2(430, Y_GROUND - 210), true);
            enemyBlock7 = new Police(Content.Load<Texture2D>("police_man"), new Vector2(2860, Y_GROUND - 290), true);

            LoadBeers();
            LoadBlocks();
            LoadBackgrounds();
        }

        /// <summary>
        /// This method is used to check if a number is within an interval
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="lowBound">The lower bound of the interval</param>
        /// <param name="highBound">The higher bound of the interval</param>
        /// <returns>true or false</returns>
        public static bool CheckInterval(float value, float lowBound, float highBound)
        {
            if (value >= lowBound && value <= highBound)
                return true;
            return false;
        }

        /// <summary>
        /// Starts the timer by constantly subtracting the number
        /// </summary>
        private void startTimer()
        {
            for (int i = 1; i <= 1000; i++)
                Timer -= i;
        }
    }
}