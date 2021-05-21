using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCamera
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (Camera VirtualCamera = new Camera())
            {
                Sphere sphere1 = new Sphere(new Vector3(0, 0, 10), 0.1f, new Color4(new Color3(0.2f, 1f, 0.2f), 255));
                VirtualCamera.AddSphere(sphere1);
                VirtualCamera.Run();
            }
        }
    }
}
