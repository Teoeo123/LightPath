
using System.Collections.Generic;
using UnityEngine;

namespace Laser
{
    internal class LaserHitParticles
    {
        List<ParticleSystem> Particles= new List<ParticleSystem>();
        ParticleSystem.MainModule settings;
        ParticleSystem sample;


        public LaserHitParticles(ParticleSystem sample, Color color) {
            settings = sample.GetComponent<ParticleSystem>().main;
            settings.startColor = new ParticleSystem.MinMaxGradient(color);
            sample.emission.Equals(false);
            this.sample=sample;
        }

        public void SetParticle(Vector2 position, Vector2 normal)
        {
            var buf = UnityEngine.Object.Instantiate(sample);
           
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
