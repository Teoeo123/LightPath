using System.Collections.Generic;
using UnityEngine;

namespace Laser
{
    public class LaserBeam
    {
        private List<LineRenderer> buforBeams = new List<LineRenderer>();
        private List<LineRenderer> displayBeams = new List<LineRenderer>();
        private LineRenderer sample;
        public LaserBeam(LineRenderer sample)
        {
            sample.positionCount= 0;
            sample.forceRenderingOff = true;
            this.sample = sample;
        }

        public void SetLine(Vector2 from, Vector2 to, float opacity =1f)
        {
            var buf = UnityEngine.Object.Instantiate(sample);
            buf.positionCount = 2;
            buf.SetPosition(0, from);
            buf.SetPosition(1, to);
            var bufrgb = buf.startColor;
            buf.startColor = new Color(bufrgb.r, bufrgb.g, bufrgb.b, opacity);
            buf.endColor = new Color(bufrgb.r, bufrgb.g, bufrgb.b, opacity);
            buforBeams.Add(buf);
        }

        public void Update()
        {
            foreach(var d in displayBeams)
                UnityEngine.Object.Destroy(d.gameObject);
            displayBeams.Clear();
            foreach(var b in buforBeams)
            {
                b.forceRenderingOff= false;
                displayBeams.Add(b);
            }
            buforBeams.Clear();
        }
    }
}
