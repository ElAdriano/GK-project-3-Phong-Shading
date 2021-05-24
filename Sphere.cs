using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace VirtualCamera
{
    //sphere (x-a)^2 + (y-b)^2 + (z-c)^2 = r^2
    public class Sphere
    {
        public Vector3 Origin;
        public float R, AmbientIntensity, Brightness;
        public Color4 Color;
        public Vector3 Ambient;

        float IA;
        float IP;
        float KA;
        float KD;
        float KS;
        float n;

        public Sphere(Vector3 origin, float r, Color4 color, float IA, float IP, float KA, float KD, float KS, float N)
        {
            Origin = origin;
            R = r;
            Color = color;

            this.IA = IA;
            this.IP = IP;
            this.KA = KA;
            this.KD = KD;
            this.KS = KS;
            this.n = N;
        }

        public Vector3 getPoint(float x, float y)
        {
            //z = sqrt(r^2 - (x-a)^2 - (y-b)^2) + c
            float z2c = (float)(Math.Pow(R, 2) - Math.Pow((x - Origin.X), 2) - Math.Pow((y - Origin.Y), 2));
            float z = (float)Math.Sqrt(z2c) + Origin.Z;
            return new Vector3(x, y, z);
        }

        public float getIlumination(float x, float y, Vector3 cameraPosition, Vector3 lightPosition)
        {
            bool shouldIlluminate = IsPixelOwned(x, y);
            if (shouldIlluminate)
            {
                Vector3 point = getPoint(x, y);
                Vector3 N = point - Origin;
                N.Normalize();

                Vector3 L = lightPosition - point;
                L.Normalize();

                Vector3 V = point - cameraPosition;
                V.Normalize();

                Vector3 R = 2 * Vector3.Dot(N, L) * N - L;
                R.Normalize();

                return (float)(IA * KA + IP * KD * Math.Max(Vector3.Dot(N, L), 0) + IP * KS * Math.Pow(Math.Max(Vector3.Dot(R, V), 0), n));
            }
            else
            {
                return 0f;
            }
        }

        public bool IsPixelOwned(float pixelX, float pixelY)
        {
            return Math.Pow((pixelX - Origin.X), 2) + Math.Pow((pixelY - Origin.Y), 2) <= Math.Pow(R, 2);
        }
    }
}
