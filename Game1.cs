using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;

namespace AlgebraLinear;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private State activeState;
    private State nextState;

    public void ChangeState(State next)
    {
            //  Only set the _nextScene value if it is not the
            //  same instance as the _activeScene.
        if (activeState != next)
        {
            nextState = next;
        }
    }


    private void TransitionScene()
    {
        if (activeState != null)
        {
            activeState.Unload();
        }

        //  Perform a garbage collection to ensure memory is cleared
        GC.Collect();

        //  Set the active scene.
        activeState = nextState;

            //  Null the next scene value
        nextState = null;

            //  If the active scene isn't null, initialize it.
            //  Remember, the Initialize method also calls the LoadContent method
        if(activeState != null)
        {
            activeState.Initialize();
        }
    }

    private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
    {
        foreach (ModelMesh mesh in model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.World = world;
                effect.View = view;
                effect.Projection = projection;
            }
    
            mesh.Draw();
        }
    }


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        ChangeState(new BiDim(this));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        //testModel = this.Content.Load<Model>("cube");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        if(nextState != null)
        {
            TransitionScene();
        }

        if(activeState != null)
        {
            activeState.Update(gameTime);
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if(activeState != null)
        {
            activeState.BeforeDraw(_spriteBatch, Color.Black);
            activeState.Draw(_spriteBatch);
            activeState.AfterDraw(_spriteBatch);
        }

        base.Draw(gameTime);
    }
}
