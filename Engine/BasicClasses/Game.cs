using System.Text.Json;
using EngineExceptions;
using GraphicsMath;

namespace Engine;

public class Game
{
    private readonly CoordinateSystem _cs;
    private readonly GameObjectsList _objects;
    private readonly MainLoop _loop = new ();
    private readonly KeyMapper _mapper;
    private readonly Config? _config;
    private readonly Canvas _canvas;
    private readonly GameCamera _playerCam;

    public Game(CoordinateSystem cs, GameObjectsList objects)
    {
        _config = JsonSerializer.Deserialize<Config>(File.ReadAllText("Config.json"));
        _canvas = new GameConsole(this, _config.HorizontalBlocks, _config.VerticalBlocks);
        _cs = cs;
        _objects = objects;
        _mapper = new(_loop);
        _playerCam = new PlayerCamera(this, new Point(new float[] { 0, 0, 0 }), new Vector(new float[] { 1, 0, 0 }),
            _config.HorizontalFoV, _config.VerticalFoV, _config.DrawDist);
        _loop.Update += Update; 
        _loop.Exit += Exit;
    }

    public CoordinateSystem CS => _cs;

    public GameObjectsList ObjList => _objects;

    public KeyMapper Mapper => _mapper;

    public void Run()
    {
        _loop.Begin(1000/_config.FPS);
    }

    private void Update(object? sender, EventArgs e)
    {
        _canvas.Draw(_playerCam);
    }

    public void Exit(object? sender, EventArgs e)
    {
        Console.WriteLine("The end.");
    }

    public void Append(GameObject obj)
    {
        _objects.Append(obj);
    }

    public void Remove(Identifier idSet)
    {
        if (_objects.Count == 0) //todo condition
            throw new EngineException.ObjectNotIncludeItem();
        
        _objects.Remove(_objects[idSet]);
    }
}