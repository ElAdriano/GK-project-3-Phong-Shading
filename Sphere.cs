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

        float IA = 1f;
        float IP = 0.7f;
        float KA = (float)0.1;
        float KD = (float)0.4;
        float KS = (float)0.3;
        float N = 100f;


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
            if (z2c < 0)
                Console.WriteLine("z2c jest ujemne jakims cudem");
            float z = (float)Math.Sqrt(z2c) + Origin.Z;
            if(z<0)
                Console.WriteLine("Z jest ujemne jakims cudem");
            return new Vector3(x, y, z);
        }

        public float getIlumination(float x, float y, Vector3 cameraPosition, Vector3 lightPosition)
        {
            //potrzebne vectory
            Vector3 point = getPoint(x, y);
            //Vector3 poVector = cameraPosition - point;
            Vector3 poVector = point - cameraPosition;
            Vector3 plVector = lightPosition - point;
            Vector3 normal = point - Origin;
            poVector.Normalize();
            plVector.Normalize();
            normal.Normalize();
            Vector3 r = Vector3.Subtract(Vector3.Multiply(normal, Vector3.Dot(normal, plVector) *2), plVector);
            r.Normalize();
            float ndotpl = Vector3.Dot(normal, plVector);
            float rdotpo = Vector3.Dot(r,poVector);
            return (float)(IA * KA + IP * KD * Math.Max(ndotpl,0) +  KS * Math.Pow( Math.Max(rdotpo,0), N));
        }

        public bool IsPixelOwned(float pixelX, float pixelY)
        {
            return Math.Pow((pixelX - Origin.X), 2) + Math.Pow((pixelY - Origin.Y), 2) <= Math.Pow(R, 2);
        }
    }
}
