using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Input;
using WaveEngine.Components.Animation;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using PlatformerProject;

/*
 * Sample Platformer Project
 * Based on the online getting started tutorial from Wave Engine
 */
namespace PlatformerProject
{
    class TimBehavior : Behavior
    {
        private const int SPEED = 5;
        private const int RIGHT = 1;
        private const int LEFT = -1;
        private const int NONE = 0;
        private const int UP = -2;
        private const int DOWN = -3;
        private const int BORDER_OFFSET = 25;

        [RequiredComponent]
        public Animation2D anim2D;
        [RequiredComponent]
        public Transform2D trans2D;

        /// <summary>
        /// 1 or -1 indicating right or left respectively
        /// </summary>
        private int direction;
        private AnimState currentState, lastState;
        private enum AnimState { Idle, Right, Left, Up, Down };

        public TimBehavior()
            : base("TimBehavior")
        {
            this.direction = NONE;
            this.anim2D = null;
            this.trans2D = null;
            this.currentState = AnimState.Idle;
        }

        protected override void Update(TimeSpan gameTime)
        {
            currentState = AnimState.Idle;

            // touch panel
            var touches = WaveServices.Input.TouchPanelState;
            if (touches.Count > 0)
            {
                var firstTouch = touches[0];
                if (firstTouch.Position.X > WaveServices.Platform.ScreenWidth / 2)
                {
                    currentState = AnimState.Right;
                }
                else
                {
                    currentState = AnimState.Left;
                }
            }

            // Keyboard
            var keyboard = WaveServices.Input.KeyboardState;
            if (keyboard.Right == ButtonState.Pressed)
            {
                currentState = AnimState.Right;
            }
            else if (keyboard.Left == ButtonState.Pressed)
            {
                currentState = AnimState.Left;
            }
            else if (keyboard.Up == ButtonState.Pressed)
            {
                currentState = AnimState.Up;
            }
            else if (keyboard.Down == ButtonState.Pressed)
            {
                currentState = AnimState.Down;
            }

            // Set current animation if that one is diferent
            if (currentState != lastState)
            {
                switch (currentState)
                {
                    case AnimState.Idle:
                        anim2D.CurrentAnimation = "Idle";
                        anim2D.Play(true);
                        direction = NONE;
                        break;
                    case AnimState.Right:
                        anim2D.CurrentAnimation = "Running";
                        trans2D.Effect = SpriteEffects.None;
                        anim2D.Play(true);
                        direction = RIGHT;
                        break;
                    case AnimState.Left:
                        anim2D.CurrentAnimation = "Running";
                        trans2D.Effect = SpriteEffects.FlipHorizontally;
                        anim2D.Play(true);
                        direction = LEFT;
                        break;
                    case AnimState.Up:
                        anim2D.CurrentAnimation = "Idle";
                        trans2D.Effect = SpriteEffects.None;
                        anim2D.Play(true);
                        direction = UP;
                        break;
                    case AnimState.Down:
                        anim2D.CurrentAnimation = "Idle";
                        trans2D.Effect = SpriteEffects.None;
                        anim2D.Play(true);
                        direction = DOWN;
                        break;
                }
            }

            lastState = currentState;

            // this is terribly put together in 30 seconds (the up and down) - no sprite for it yet
            if (direction == -2) // up
                // up movement
                trans2D.Y -= SPEED * (gameTime.Milliseconds / 10);
            else if (direction == -3) // down
                trans2D.Y += SPEED * (gameTime.Milliseconds / 10);
            else 
                // Move sprite
                trans2D.X += direction * SPEED * (gameTime.Milliseconds / 10);

            // Check borders
            if (trans2D.X < BORDER_OFFSET)
            {
                trans2D.X = BORDER_OFFSET;
            }
            else if (trans2D.X > WaveServices.Platform.ScreenWidth - BORDER_OFFSET)
            {
                trans2D.X = WaveServices.Platform.ScreenWidth - BORDER_OFFSET;
            }
        }
    }
}

