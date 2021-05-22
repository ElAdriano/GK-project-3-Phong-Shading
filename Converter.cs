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

        private static Vector2 UncastFrom2D(int pixelX, int pixelY)
        {
            var x = (2 * (float)pixelX - (float)Camera.Width) / (2 * (float)Camera.Width);
            var y = ((float)Camera.Height - 2 * (float)pixelY) / (2 * (float)Camera.Height);
            return new Vector2(x, y);
        }

        public static void Render(List<Sphere> objects, Camera camera)
        {
            for (int objNum = 0; objNum < objects.Count; objNum++)
            {
                Sphere obj = objects[objNum];
                if (obj.Origin.Z <= 0)
                {
                    continue;
                }

                for(int width = 0; width < Camera.Width; width++)
                {
                    for(int height = 0; height < Camera.Height; height++)
                    {
                        Vector2 point = UncastFrom2D(width, height);

                        if (obj.IsPixelOwned(point.X, point.Y))
                        {
                            float illumination = obj.getIlumination(point.X, point.Y, camera.Position, camera.Light.Position);
                            float intensity = (float)Math.Min(illumination, 1);
                            var calculatedColor = intensity * obj.Color;

                            var redColorValue = calculatedColor.Red * 255;
                            var greenColorValue = calculatedColor.Green * 255;
                            var blueColorValue = calculatedColor.Blue * 255;

                            UpdatePixelValue(width, height, (byte)redColorValue, (byte)greenColorValue, (byte)blueColorValue, 255, camera);
                        }
                    }
                }
            }
        }
    }
}
