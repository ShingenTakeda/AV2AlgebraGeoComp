using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Basic3dExampleCamera
{
    private GraphicsDevice graphicsDevice = null;
    private GameWindow gameWindow = null;

    private MouseState mState = default(MouseState);
    private KeyboardState kbState = default(KeyboardState);

    public float MovementUnitsPerSecond { get; set; } = 30f;
    public float RotationRadiansPerSecond { get; set; } = 60f;

    public float fieldOfViewDegrees = 80f;
    public float nearClipPlane = .05f;
    public float farClipPlane = 2000f;

    private float yMouseAngle = 0f;
    private float xMouseAngle = 0f;
    private bool mouseLookIsUsed = true;

    private int fpsKeyboardLayout = 1;
    private int cameraTypeOption = 1;


    public const int CAM_UI_OPTION_FPS_LAYOUT = 0;
    public const int CAM_UI_OPTION_EDIT_LAYOUT = 1;
    public const int CAM_TYPE_OPTION_FIXED = 0;

    public const int CAM_TYPE_OPTION_FREE = 1;


    public Basic3dExampleCamera(GraphicsDevice gfxDevice, GameWindow window)
    {
        graphicsDevice = gfxDevice;
        gameWindow = window;
        ReCreateWorldAndView();
        ReCreateThePerspectiveProjectionMatrix(gfxDevice, fieldOfViewDegrees);
    }

    public void CameraUi(int UiOption)
    {
        fpsKeyboardLayout = UiOption;
    }
    public void CameraType(int cameraOption)
    {
        cameraTypeOption = cameraOption;
    }

    private Vector3 up = Vector3.Up;

    private Matrix camerasWorld = Matrix.Identity;

    private Matrix viewMatrix = Matrix.Identity;

    private Matrix projectionMatrix = Matrix.Identity;

    public Vector3 Position
    {
        set
        {
            camerasWorld.Translation = value;
           
            ReCreateWorldAndView();
        }
        get { return camerasWorld.Translation; }
    }
    public Vector3 Forward
    {
        set
        {
            camerasWorld = Matrix.CreateWorld(camerasWorld.Translation, value, up);
            ReCreateWorldAndView();
        }
        get { return camerasWorld.Forward; }
    }

    public Vector3 Up
    {
        set
        {
            up = value;
            camerasWorld = Matrix.CreateWorld(camerasWorld.Translation, camerasWorld.Forward, value);

            ReCreateWorldAndView();
        }
        get { return up; }
    }
    public Vector3 LookAtDirection
    {
        set
        {
            camerasWorld = Matrix.CreateWorld(camerasWorld.Translation, value, up);
            ReCreateWorldAndView();
        }
        get { return camerasWorld.Forward; }
    }
    public Vector3 TargetPositionToLookAt
    {
        set
        {
            camerasWorld = Matrix.CreateWorld(camerasWorld.Translation, Vector3.Normalize(value - camerasWorld.Translation), up);
            
            ReCreateWorldAndView();
        }
    }
    public Matrix LookAtTheTargetMatrix
    {
        set
        {
            camerasWorld = Matrix.CreateWorld(camerasWorld.Translation, Vector3.Normalize(value.Translation - camerasWorld.Translation), up);
            ReCreateWorldAndView();
        }
    }
    public Matrix World
    {
        get
        {
            return camerasWorld;
        }
        set
        {
            camerasWorld = value;
            viewMatrix = Matrix.CreateLookAt(camerasWorld.Translation, camerasWorld.Forward + camerasWorld.Translation, camerasWorld.Up);
        }
    }
    public Matrix View
    {
        get
        {
            return viewMatrix;
        }
    }

    public Matrix Projection
    {
        get
        {
            return projectionMatrix;
        }
    }

    private void ReCreateWorldAndView()
    {
        if (cameraTypeOption == CAM_TYPE_OPTION_FIXED)
            up = Vector3.Up;
        if (cameraTypeOption == CAM_UI_OPTION_EDIT_LAYOUT )
            up = camerasWorld.Up;

        camerasWorld = Matrix.CreateWorld(camerasWorld.Translation, camerasWorld.Forward, up);
        viewMatrix = Matrix.CreateLookAt(camerasWorld.Translation, camerasWorld.Forward + camerasWorld.Translation, camerasWorld.Up);
    }

    public void ReCreateThePerspectiveProjectionMatrix(GraphicsDevice gd, float fovInDegrees)
    {
        projectionMatrix = Matrix.CreatePerspectiveFieldOfView(fovInDegrees * (float)((3.14159265358f) / 180f), gd.Viewport.Width / gd.Viewport.Height, .05f, 1000f);
    }
    /// <summary>
    /// Changes the perspective matrix to a new near far and field of view.
    /// The projection matrix is typically only set up once at the start of the app.
    /// </summary>
    public void ReCreateThePerspectiveProjectionMatrix(float fieldOfViewInDegrees, float nearPlane, float farPlane)
    {
        // create the projection matrix.
        this.fieldOfViewDegrees = MathHelper.ToRadians(fieldOfViewInDegrees);
        nearClipPlane = nearPlane;
        farClipPlane = farPlane;
        float aspectRatio = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;
        projectionMatrix = Matrix.CreatePerspectiveFieldOfView(this.fieldOfViewDegrees, aspectRatio, nearClipPlane, farClipPlane);
    }

    /// <summary>
    /// update the camera.
    /// </summary>
    public void Update(GameTime gameTime)
    {
        if (fpsKeyboardLayout == CAM_UI_OPTION_FPS_LAYOUT)
            FpsUiControlsLayout(gameTime);
        if (fpsKeyboardLayout == CAM_UI_OPTION_EDIT_LAYOUT)
            EditingUiControlsLayout(gameTime);
    }

    /// <summary>
    /// like a fps games camera right clicking turns mouse look on or off same for the edit mode.
    /// </summary>
    /// <param name="gameTime"></param>
    private void FpsUiControlsLayout(GameTime gameTime)
    {
        MouseState state = Mouse.GetState(gameWindow);
        KeyboardState kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.W))
        {
            MoveForward(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.S) == true)
        {
            MoveBackward(gameTime);
        }
        // strafe. 
        if (kstate.IsKeyDown(Keys.A) == true)
        {
            MoveLeft(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.D) == true)
        {
            MoveRight(gameTime);
        }

        // rotate 
        if (kstate.IsKeyDown(Keys.Left) == true)
        {
            RotateLeft(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.Right) == true)
        {
            RotateRight(gameTime);
        }
        // rotate 
        if (kstate.IsKeyDown(Keys.Up) == true)
        {
            RotateUp(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.Down) == true)
        {
            RotateDown(gameTime);
        }

        if (kstate.IsKeyDown(Keys.Q) == true)
        {
            if (cameraTypeOption == CAM_TYPE_OPTION_FIXED)
                MoveUpInNonLocalSystemCoordinates(gameTime);
            if (cameraTypeOption == CAM_TYPE_OPTION_FREE)
                MoveUp(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.E) == true)
        {
            if (cameraTypeOption == CAM_TYPE_OPTION_FIXED)
                MoveDownInNonLocalSystemCoordinates(gameTime);
            if (cameraTypeOption == CAM_TYPE_OPTION_FREE)
                MoveDown(gameTime);
        }


        if (state.LeftButton == ButtonState.Pressed)
        {
            if (mouseLookIsUsed == false)
                mouseLookIsUsed = true;
            else
                mouseLookIsUsed = false;
        }
        if (mouseLookIsUsed)
        {
            Vector2 diff = state.Position.ToVector2() - mState.Position.ToVector2();
            if (diff.X != 0f)
                RotateLeftOrRight(gameTime, diff.X);
            if (diff.Y != 0f)
                RotateUpOrDown(gameTime, diff.Y);
        }
        mState = state;
        kbState = kstate;
    }

    /// <summary>
    /// when working like programing editing and stuff.
    /// </summary>
    /// <param name="gameTime"></param>
    private void EditingUiControlsLayout(GameTime gameTime)
    {
        MouseState state = Mouse.GetState(gameWindow);
        KeyboardState kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.E))
        {
            MoveForward(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.Q) == true)
        {
            MoveBackward(gameTime);
        }
        if (kstate.IsKeyDown(Keys.W))
        {
            RotateUp(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.S) == true)
        {
            RotateDown(gameTime);
        }
        if (kstate.IsKeyDown(Keys.A) == true)
        {
            RotateLeft(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.D) == true)
        {
            RotateRight(gameTime);
        }

        if (kstate.IsKeyDown(Keys.Left) == true)
        {
            MoveLeft(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.Right) == true)
        {
            MoveRight(gameTime);
        }
        // rotate 
        if (kstate.IsKeyDown(Keys.Up) == true)
        {
            MoveUp(gameTime);
        }
        else if (kstate.IsKeyDown(Keys.Down) == true)
        {
            MoveDown(gameTime);
        }

        // roll counter clockwise
        if (kstate.IsKeyDown(Keys.Z) == true)
        {
            if (cameraTypeOption == CAM_TYPE_OPTION_FREE)
                RotateRollCounterClockwise(gameTime);
        }
        // roll clockwise
        else if (kstate.IsKeyDown(Keys.C) == true)
        {
            if (cameraTypeOption == CAM_TYPE_OPTION_FREE)
                RotateRollClockwise(gameTime);
        }

        if (state.RightButton == ButtonState.Pressed)
                mouseLookIsUsed = true;
        else
            mouseLookIsUsed = false;
        if (mouseLookIsUsed)
        {
            Vector2 diff = state.Position.ToVector2() - mState.Position.ToVector2();
            if (diff.X != 0f)
                RotateLeftOrRight(gameTime, diff.X);
            if (diff.Y != 0f)
                RotateUpOrDown(gameTime, diff.Y);
        }
        mState = state;
        kbState = kstate;
    }

    /// <summary>
    /// This function can be used to check if gimble is about to occur in a fixed camera.
    /// If this value returns 1.0f you are in a state of gimble lock, However even as it gets near to 1.0f you are in danger of problems.
    /// In this case you should interpolate towards a free camera. Or begin to handle it.
    /// Earlier then .9 in some manner you deem to appear fitting otherwise you will get a hard spin effect. Though you may want that.
    /// </summary>
    public float GetGimbleLockDangerValue()
    {
        var c0 = Vector3.Dot(World.Forward, World.Up);
        if (c0 < 0f) c0 = -c0;
        return c0;
    }
    
    #region Local Translations and Rotations.

    public void MoveForward(GameTime gameTime)
    {
        Position += (camerasWorld.Forward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveBackward(GameTime gameTime)
    {
        Position += (camerasWorld.Backward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveLeft(GameTime gameTime)
    {
        Position += (camerasWorld.Left * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveRight(GameTime gameTime)
    {
        Position += (camerasWorld.Right * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveUp(GameTime gameTime)
    {
        Position += (camerasWorld.Up * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveDown(GameTime gameTime)
    {
        Position += (camerasWorld.Down * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void RotateUp(GameTime gameTime)
    {
        var radians = RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Matrix matrix = Matrix.CreateFromAxisAngle(camerasWorld.Right, MathHelper.ToRadians(radians));
        LookAtDirection = Vector3.TransformNormal(LookAtDirection, matrix);
        ReCreateWorldAndView();
    }
    public void RotateDown(GameTime gameTime)
    {
        var radians = -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Matrix matrix = Matrix.CreateFromAxisAngle(camerasWorld.Right, MathHelper.ToRadians(radians));
        LookAtDirection = Vector3.TransformNormal(LookAtDirection, matrix);
        ReCreateWorldAndView();
    }
    public void RotateLeft(GameTime gameTime)
    {
        var radians = RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Matrix matrix = Matrix.CreateFromAxisAngle(camerasWorld.Up, MathHelper.ToRadians(radians));
        LookAtDirection = Vector3.TransformNormal(LookAtDirection, matrix);
        ReCreateWorldAndView();
    }
    public void RotateRight(GameTime gameTime)
    {
        var radians = -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Matrix matrix = Matrix.CreateFromAxisAngle(camerasWorld.Up, MathHelper.ToRadians(radians));
        LookAtDirection = Vector3.TransformNormal(LookAtDirection, matrix);
        ReCreateWorldAndView();
    }
    public void RotateRollClockwise(GameTime gameTime)
    {
        var radians = RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        var pos = camerasWorld.Translation;
        camerasWorld *= Matrix.CreateFromAxisAngle(camerasWorld.Forward, MathHelper.ToRadians(radians));
        camerasWorld.Translation = pos;
        ReCreateWorldAndView();
    }
    public void RotateRollCounterClockwise(GameTime gameTime)
    {
        var radians = -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        var pos = camerasWorld.Translation;
        camerasWorld *= Matrix.CreateFromAxisAngle(camerasWorld.Forward, MathHelper.ToRadians(radians));
        camerasWorld.Translation = pos;
        ReCreateWorldAndView();
    }

    // just for example this is the same as the above rotate left or right.
    public void RotateLeftOrRight(GameTime gameTime, float amount)
    {
        var radians = amount * -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Matrix matrix = Matrix.CreateFromAxisAngle(camerasWorld.Up, MathHelper.ToRadians(radians));
        LookAtDirection = Vector3.TransformNormal(LookAtDirection, matrix);
        ReCreateWorldAndView();
    }
    public void RotateUpOrDown(GameTime gameTime, float amount)
    {
        var radians = amount * -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Matrix matrix = Matrix.CreateFromAxisAngle(camerasWorld.Right, MathHelper.ToRadians(radians));
        LookAtDirection = Vector3.TransformNormal(LookAtDirection, matrix);
        ReCreateWorldAndView();
    } 

    #endregion

    #region Non Local System Translations and Rotations.

    public void MoveForwardInNonLocalSystemCoordinates(GameTime gameTime)
    {
        Position += (Vector3.Forward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveBackwardsInNonLocalSystemCoordinates(GameTime gameTime)
    {
        Position += (Vector3.Backward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveUpInNonLocalSystemCoordinates(GameTime gameTime)
    {
        Position += (Vector3.Up * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveDownInNonLocalSystemCoordinates(GameTime gameTime)
    {
        Position += (Vector3.Down * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveLeftInNonLocalSystemCoordinates(GameTime gameTime)
    {
        Position += (Vector3.Left * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void MoveRightInNonLocalSystemCoordinates(GameTime gameTime)
    {
        Position += (Vector3.Right * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    /// <summary>
    /// These aren't typically useful and you would just use create world for a camera snap to a new view. I leave them for completeness.
    /// </summary>
    public void NonLocalRotateLeftOrRight(GameTime gameTime, float amount)
    {
        var radians = amount * -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Matrix matrix = Matrix.CreateFromAxisAngle(Vector3.Up, MathHelper.ToRadians(radians));
        LookAtDirection = Vector3.TransformNormal(LookAtDirection, matrix);
        ReCreateWorldAndView();
    }
    /// <summary>
    /// These aren't typically useful and you would just use create world for a camera snap to a new view.  I leave them for completeness.
    /// </summary>
    public void NonLocalRotateUpOrDown(GameTime gameTime, float amount)
    {
        var radians = amount * -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Matrix matrix = Matrix.CreateFromAxisAngle(Vector3.Right, MathHelper.ToRadians(radians));
        LookAtDirection = Vector3.TransformNormal(LookAtDirection, matrix);
        ReCreateWorldAndView();
    }

    #endregion
}