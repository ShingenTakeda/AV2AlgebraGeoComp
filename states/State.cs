
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace AlgebraLinear;

public abstract class State
{
    protected Game1 game = null;
    protected ContentManager cmanager;

    public State(Game1 game)
    {
        if (game == null)
        {
            throw new ArgumentNullException(nameof(game), "Game cannot be null!");
        }
        this.game = game;
    }

    public virtual void Initialize()
    {
        cmanager = new ContentManager(game.Services);
        cmanager.RootDirectory = game.Content.RootDirectory;
        LoadContent();
    }

    public virtual void LoadContent() {}
    public virtual void Unload()
    {
        cmanager.Unload();
        cmanager = null;
    }

    public virtual void Update(GameTime delta) {}

    public virtual void BeforeDraw(SpriteBatch spriteBatch, Color clearColor)
    {
        game.GraphicsDevice.Clear(clearColor);
        spriteBatch.Begin();
    }

    public virtual void Draw(SpriteBatch spriteBatch) {}

    public virtual void AfterDraw(SpriteBatch spriteBatch)
    {
        spriteBatch.End();
    }
}