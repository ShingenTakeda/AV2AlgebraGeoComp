using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class BiDim : AlgebraLinear.State
{
    private Texture2D arrow;
    private float angle = 0.0f;
    Vector2 origin;
    Vector2 location = new Vector2(400, 200);

    public BiDim(AlgebraLinear.Game1 game) : base(game) {this.game = game;}

    public override void LoadContent()
    {
        arrow = game.Content.Load<Texture2D>("arrow");
        origin = new Vector2(arrow.Width / 2, arrow.Height);
        base.LoadContent();
    }

    public override void Update(GameTime delta)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        if(keyboardState.IsKeyDown(Keys.Q))
        {
            Console.WriteLine("Pressing Q");
            angle += 0.1f;
        }
        if(keyboardState.IsKeyDown(Keys.E))
        {
            angle -= 0.1f;
        }
        if(keyboardState.IsKeyDown(Keys.A))
        {
            location.X -= 3;
        }
        if(keyboardState.IsKeyDown(Keys.D))
        {
            location.X += 3;
        }
        if(keyboardState.IsKeyDown(Keys.W))
        {
            location.Y -= 3;
        }
        if(keyboardState.IsKeyDown(Keys.S))
        {
            location.Y += 3;
        }
        if(keyboardState.IsKeyDown(Keys.Space))
        {
            game.ChangeState(new TriDim(game));
        }
        
        if(keyboardState.IsKeyDown(Keys.Y))
        {
            location.X = -location.X;
        }

        if(keyboardState.IsKeyDown(Keys.U))
        {
            location.Y = -location.Y;
        }

        base.Update(delta);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        
        Rectangle srcRec = new Rectangle(0, 0, arrow.Width, arrow.Height);
        spriteBatch.Draw(arrow, location, srcRec, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        base.Draw(spriteBatch);
    }
}