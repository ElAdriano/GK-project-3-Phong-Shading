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
                Sphere greenBall = new Sphere(
                    new Vector3(-0.25f, 0.3f, 10), // origin
                    0.125f,   // r
                    new Color4(new Color3(0.2f, 1f, 0.2f), 255), // object color
                    1f,     // Ambient intensity
                    0.75f,  // Incident light intensity
                    0.15f,  // ambient k coefficient [0, 1]
                    0.35f,  // diffuse k coefficient [0, 1]
                    0.15f,  // specular k coefficient [0, 1]
                    2       // n
                );

                Sphere blueBall = new Sphere(
                    new Vector3(0.25f, 0.3f, 10), // origin
                    0.125f,  // r
                    new Color4(new Color3(0.2f, 0.2f, 1f), 255), // object color
                    1f,     // Ambient intensity
                    0.7f,   // Incident light intensity
                    0.1f,   // ambient k coefficient [0, 1]
                    1f,     // diffuse k coefficient [0, 1]
                    0.3f,   // specular k coefficient [0, 1]
                    80      // n
                );

                Sphere orangeBall = new Sphere(
                    new Vector3(-0.25f, -0.1f, 10), // origin
                    0.125f,  // r
                    new Color4(new Color3(1f, 0.5f, 0f), 255), // object color
                    1f,     // Ambient intensity
                    0.7f,   // Incident light intensity
                    0.1f,   // ambient k coefficient [0, 1]
                    1f,     // diffuse k coefficient [0, 1]
                    0.3f,   // specular k coefficient [0, 1]
                    50      // n
                );

                Sphere redBall = new Sphere(
                    new Vector3(0.25f, -0.1f, 10), // origin
                    0.125f,  // r
                    new Color4(new Color3(1f, 0f, 0f), 255), // object color
                    1f,     // Ambient intensity
                    0.9f,  // Incident light intensity
                    0.1f,   // ambient k coefficient [0, 1]
                    0.7f,     // diffuse k coefficient [0, 1]
                    1f,   // specular k coefficient [0, 1]
                    150      // n
                );

                VirtualCamera.AddSphere(greenBall);
                VirtualCamera.AddSphere(blueBall);
                VirtualCamera.AddSphere(orangeBall);
                VirtualCamera.AddSphere(redBall);

                VirtualCamera.Run();
            }
        }
    }
}
