using System.Text;

namespace Engine;

public class GameConsole : Canvas
{
    private static readonly string CharMap = "@QB#NgWM8RDHdOKq9$6khEPXwmeZaoS2yjufF]}{tx1zv7lciL/\\|?*>r^;:_\"~,'.-` ";
    
    public GameConsole(Game game, int horSize, int vertSize) : base(game, horSize, vertSize) { }
    
    public override void Draw(GameCamera cam)
    {
        Update(cam);
        StringBuilder image = new StringBuilder();
        for (int j = 0; j < Distances.GetLength(1); j++)
        {
            for (int i = 0; i < Distances.GetLength(0); i++)
                image.Append(GetChar(i, j, cam));

            image.Append('\n');
        }
        image.Append(cam["pos"].ToString());
        image.Append('\n');
        image.Append(cam["dir"].ToString());
        /*image.Append('\n');
        for (var i = 0; i < RayMatrix.GetLength(0); i++)
        {
            for (var j = 0; j < RayMatrix.GetLength(1); j++)
                image.Append(RayMatrix[i, j]);
            
            image.Append('\n');
        }*/
        Console.Clear();
        Console.Write(image.ToString());
    }

    public Ray[,] RayMatrix;

    public override void Update(GameCamera cam)
    {
        Ray[,] rays = cam.GetRays(HorSize, VertSize);
        RayMatrix = rays;
        for (int i = 0; i < HorSize; i++)
        for (int j = 0; j < VertSize; j++)
        {
            Distances[i, j] = null;
            for (int k = 0; k < Game.ObjList.Count; k++)
            {
                float? intersect = Game.ObjList[k].IntersectionDistance(rays[i, j]);
                if (intersect == null) continue;
                if (intersect > (float?)cam["drawDist"]) continue;
                if (Distances[i, j] == null) Distances[i, j] = intersect;
                if (Distances[i, j] > intersect) Distances[i, j] = intersect;
            }
        }
    }

    char GetChar(int i, int j, GameCamera cam)
    {
        if (Distances[i, j] == null) return CharMap[CharMap.Length - 1];
        float ratio = (float)Distances[i, j] / (float)cam["drawDist"];
        int index = (int)(ratio * (CharMap.Length - 1));
        return CharMap[index];
    }
}