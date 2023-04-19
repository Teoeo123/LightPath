
using System.Collections.Generic;
using UnityEngine;

namespace Laser
{
    internal class LaserHitParticles
    {
        List<ParticleSystem> Particles= new List<ParticleSystem>();
        ParticleSystem.MainModule settings;
        ParticleSystem sample;
        private Color color;


        public LaserHitParticles(ParticleSystem sample, Color color) {
            settings = sample.GetComponent<ParticleSystem>().main;
            sample.emission.Equals(false);
            this.color = color;
            this.sample=sample;
        }

        [System.Obsolete]
        public void SetParticle(Vector2 position, Vector2 normal, float power)
        {
            ParticleSystem buf = UnityEngine.Object.Instantiate(sample);
            color.a = power;
            buf.startColor = color;
            buf.transform.position = position;
            Particles.Add(buf);
        }


        public void Delete(float offset)
        {
            foreach (var d in Particles)
                UnityEngine.Object.Destroy(d.gameObject,offset);
            Particles.Clear();
        }

    }
}
