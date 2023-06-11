using GraphicsMath;

namespace Engine;

public class Canvas
{
    private readonly int _horSize;
    private readonly int _vertSize;
    protected readonly float?[,] Distances;
    protected readonly Game Game;

    public int HorSize => _horSize;
    public int VertSize => _vertSize;

    public Canvas(Game game, int horSize, int vertSize)
    {
        _horSize = horSize;
        _vertSize = vertSize;
        Distances = new float?[horSize, vertSize];
        Game = game;
    }

    public virtual void Draw(GameCamera cam) { }

    public virtual void Update(GameCamera cam) { }
}