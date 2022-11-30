using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class PhysicsScene2D : AlgebraLinear.State
{
    CollisionWorld2D world2D;

    public PhysicsScene2D(AlgebraLinear.Game1 game) : base(game) {this.game = game;}

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void Update(GameTime delta)
    {
        base.Update(delta);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);   
    }
}