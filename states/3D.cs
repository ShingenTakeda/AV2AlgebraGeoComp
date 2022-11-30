using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class TriDim : AlgebraLinear.State
{
    private Model cube;
    float angle;
    public TriDim(AlgebraLinear.Game1 game) : base(game) {this.game = game;}

    private Matrix world;
    private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
    private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 100f);

    Basic3dExampleCamera camera3D;

    Vector3 pos = new Vector3(0, 0, 0);
    Vector3 ytrans = new Vector3(0, 0.01f, 0);
    Vector3 nYTrans = new Vector3(0, -0.01f, 0);
    Vector3 xtrans = new Vector3(0.01f, 0, 0);
    Vector3 nXTrans = new Vector3(-0.01f, 0, 0);

    void BuildPerspProjMat(float []m, float fov, float aspect,
    float znear, float zfar)
    {
    float xymax = znear * (float)Math.Tan((double)fov * (double)Math.PI);
    float ymin = -xymax;
    float xmin = -xymax;

    float width = xymax - xmin;
    float height = xymax - ymin;

    float depth = zfar - znear;
    float q = -(zfar + znear) / depth;
    float qn = -2 * (zfar * znear) / depth;

    float w = 2 * znear / width;
    w = w / aspect;
    float h = 2 * znear / height;

    m[0]  = w;
    m[1]  = 0;
    m[2]  = 0;
    m[3]  = 0;

    m[4]  = 0;
    m[5]  = h;
    m[6]  = 0;
    m[7]  = 0;

    m[8]  = 0;
    m[9]  = 0;
    m[10] = q;
    m[11] = -1;

    m[12] = 0;
    m[13] = 0;
    m[14] = qn;
    m[15] = 0;
    }

    private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
    {
        foreach (ModelMesh mesh in model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.EnableDefaultLighting();
                effect.World = world;
                effect.View = view;
                effect.Projection = projection;
            }
    
            mesh.Draw();
        }
    }

    private void Translation(Vector3 v1, Vector3 v2)
    {
        v1 += v2;
        world = Matrix.CreateTranslation(v1);
    }

    private void RotationX(Matrix world, float angle)
    {
        this.angle += angle;
        world = Matrix.CreateRotationX(angle);
    }

    private void RotationY(Matrix world, float angle)
    {
        this.angle += angle;
        world =  Matrix.CreateRotationY(angle);
    }

    private void RotationZ( Matrix world, float angle)
    {
        this.angle += angle;
        world =  Matrix.CreateRotationY(angle);
    }
    

    public override void LoadContent()
    {
        VectorOperations vo = new VectorOperations();
        angle = 0.0f;
        camera3D = new Basic3dExampleCamera(game.GraphicsDevice, game.Window);
        cube = cmanager.Load<Model>("cube");
        world = Matrix.CreateTranslation(pos) * Matrix.CreateRotationX(angle);
        base.LoadContent();

        Vector3 v1 = vo.Project(new Vector3(2, 3, 4), new Vector3(4, 4, 4));
        Vector3 v2 = vo.Reflection(new Vector3(2, 3, 4), new Vector3(4, 4, 4));

        Vector2 v3 = vo.Project(new Vector2(2, 3), new Vector2(4, 4));
        Vector2 v4 = vo.Reflection(new Vector2(2, 3), new Vector2(4, 4));
    }

    public override void Update(GameTime delta)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        if(keyboardState.IsKeyDown(Keys.A))
        {
            pos += new Vector3(0, 0.01f, 0);
            angle += 0.03f;
            world = Matrix.CreateTranslation(pos);
            world = Matrix.CreateTranslation(pos) * Matrix.CreateRotationX(angle);
        }

        if(keyboardState.IsKeyDown(Keys.D))
        {
            pos -= new Vector3(0, 0.01f, 0);
            angle -= 0.03f;
            world = Matrix.CreateTranslation(pos);
            world = Matrix.CreateTranslation(pos) * Matrix.CreateRotationX(angle);
        }

        if(keyboardState.IsKeyDown(Keys.W))
        {
            pos += new Vector3(0.01f, 0, 0);
            angle += 0.03f;
            world = Matrix.CreateTranslation(pos);
            world = Matrix.CreateTranslation(pos) * Matrix.CreateRotationY(angle);
        }
        if(keyboardState.IsKeyDown(Keys.S))
        {
            pos -= new Vector3(0.01f, 0, 0);
            angle -= 0.03f;
            world = Matrix.CreateTranslation(pos);
            world = Matrix.CreateTranslation(pos) * Matrix.CreateRotationY(angle);
        }

        if(keyboardState.IsKeyDown(Keys.Y))
        {
            pos += new Vector3(0, 0, 0);
            angle += 0.03f;
            world = Matrix.CreateTranslation(pos);
            world = Matrix.CreateTranslation(pos) * Matrix.CreateRotationZ(angle);
        }

        if(keyboardState.IsKeyDown(Keys.U))
        {
            pos += new Vector3(0, 0, 0);
            angle -= 0.03f;
            world = Matrix.CreateTranslation(pos);
            world = Matrix.CreateTranslation(pos) * Matrix.CreateRotationZ(angle);
        }

        if(keyboardState.IsKeyDown(Keys.I))
        {
            view = Matrix.CreateOrthographic(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 100f);
        }

        //pos += new Vector3(0, 0.1f, 0);


        base.Update(delta);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        DrawModel(cube, world, view, projection);
        base.Draw(spriteBatch);
    }
}