using GraphicsMath;

namespace Engine;

public class PlayerCamera : GameCamera
{
    public PlayerCamera(Game game, Point pos, Vector dir, float fov, float vFov, float drawDist)
        : base(game, pos, dir, fov, vFov, drawDist)
    {
        game.Mapper.Add(ConsoleKey.W, this, MoveForward);
        game.Mapper.Add(ConsoleKey.S, this, MoveBackward);
        game.Mapper.Add(ConsoleKey.A, this, MoveLeft);
        game.Mapper.Add(ConsoleKey.D, this, MoveRight);
        game.Mapper.Add(ConsoleKey.Spacebar, this, MoveUp);
        game.Mapper.Add(ConsoleKey.C, this, MoveDown);
        game.Mapper.Add(ConsoleKey.LeftArrow, this, RotateLeft);
        game.Mapper.Add(ConsoleKey.RightArrow, this, RotateRight);
        game.Mapper.Add(ConsoleKey.UpArrow, this, RotateUp);
        game.Mapper.Add(ConsoleKey.DownArrow, this, RotateDown);
    }

    private static void MoveUp(Entity e)
    {
        var camera = (PlayerCamera)e;
        camera.Move(new Vector( new [] { 0, 0, 0.1f }));
    }

    private static void MoveDown(Entity e)
    {
        var camera = (PlayerCamera)e;
        camera.Move(new Vector(new [] { 0, 0, -0.1f }));
    }

    private static void MoveForward(Entity e)
    {
        var camera = (PlayerCamera) e;
        camera.Move(camera.CS.Basis.Normalize((Vector)camera["dir"]) / 1);
    }

    private static void MoveLeft(Entity e)
    {
        var camera = (PlayerCamera)e;
        var left = camera.CS.Basis.VectorProduct(((Vector)camera["dir"]), new Vector(new float[] { 0, 0, 1 }));
        camera.Move(camera.CS.Basis.Normalize(left) / 1);
    }

    private static void MoveRight(Entity e)
    {
        var camera = (PlayerCamera)e;
        var right = camera.CS.Basis.VectorProduct((Vector)camera["dir"], new Vector(new float[] { 0, 0, 1 }));
        camera.Move(-1 * camera.CS.Basis.Normalize(right) / 1);
    }

    private static void MoveBackward(Entity e)
    {
        var camera = (PlayerCamera)e;
        camera.Move(-1 * camera.CS.Basis.Normalize((Vector)camera["dir"]) / 1);
    }

    private static void RotateLeft(Entity e)
    {
        var camera = (PlayerCamera)e;
        camera.Rotate3D(0, 0, 10);
    }

    private static void RotateRight(Entity e)
    {
        var camera = (PlayerCamera)e;
        camera.Rotate3D(0, 0, -10);
    }

    private static void RotateUp(Entity e)
    {
        var camera = (PlayerCamera)e;
        camera.Rotate3D(0, -10, 0);
    }

    private static void RotateDown(Entity e)
    {
        var camera = (PlayerCamera)e;
        camera.Rotate3D(0, 10, 0);
    }
    
}