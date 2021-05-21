using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCamera
{
    public class Converter
    {
        public static void UpdatePixelValue(int x, int y, byte red, byte green, byte blue, byte alpha, Camera camera)
        {
            var index = (x + y * camera.CameraView.PixelSize.Width) * 4;

            camera.SceneCache[index] = red;
            camera.SceneCache[index + 1] = green;
            camera.SceneCache[index + 2] = blue;
            camera.SceneCache[index + 3] = alpha;
        }

        public static void Render(List<Sphere> objects, Camera camera)
        {
            for (int objNum = 0; objNum < objects.Count; objNum++)
            {
                Sphere obj = objects[objNum];
                if (obj.Origin.Z > 0)
                {
                    continue;
                }

                for(int width = 0; width < Camera.Width; width++)
                {
                    for(int height = 0; height < Camera.Height; height++)
                    {
                        float illumination = obj.getIlumination(width, height, camera.Position, camera.Light.Position);
                        byte intensity = (byte)Math.Min(illumination * 255, 255);
                        Console.WriteLine(intensity);
                        UpdatePixelValue(width, height, (byte)(255 * obj.Color.Red), (byte)(255 * obj.Color.Green), (byte)(255 * obj.Color.Blue), intensity, camera);
                    }
                }
            }
        }
    }
}
