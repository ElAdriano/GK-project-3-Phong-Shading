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

        float IA = 1;
        float IP = 1;
        float KA = (float)0.05;
        float KD = (float)0.5;
        float KS = (float)0.5;
        float N = 5;


        public Sphere(Vector3 origin, float r, Color4 color)
        {
            Origin = origin;
            R = r;
            Color = color;
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
            //potrzebne vectory
            Vector3 point = getPoint(x, y);
            Vector3 poVector = cameraPosition - point;
            Vector3 plVector = lightPosition - point;
            Vector3 normal = point - Origin;
            poVector.Normalize();
            plVector.Normalize();
            normal.Normalize();
            //subtract(multiply(multiply(n, 2), multiply(n, l)), l)
            Vector3 r = new Vector3((normal.X * 2 * normal.X * plVector.X) - plVector.X, (normal.Y * 2 * normal.Y * plVector.Y) - plVector.Y, (normal.Z * 2 * normal.Z * plVector.Z) - plVector.Z);
            r.Normalize();

            float ndotpl = Vector3.Dot(normal, plVector);
            float rdotpo = Vector3.Dot(r,poVector);

            return (float)(IA * KA + IP * KD * Math.Max(ndotpl,0) +  Math.Pow(KS * Math.Max(rdotpo,0), N));
        }
    }
}
