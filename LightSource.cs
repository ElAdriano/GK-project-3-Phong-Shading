using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace VirtualCamera
{
    public class LightSource
    {
        public Vector3 Position;

        public LightSource(float x = 0, float y = 0, float z = 0)
        {
            Position = new Vector3(x, y, z);
        }
    }
}
