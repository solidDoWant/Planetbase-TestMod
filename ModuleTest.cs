using System.Xml;
using Planetbase;
using PlanetbaseFramework.GameMechanics.Buildings;
using UnityEngine;

namespace TestMod
{
    public class ModuleTest : BaseModule
    {
        public const string GenerationMultiplierTagName = "generation-multiplier";
        public const float UpdatePeriodSeconds = 10f;
        public float PowerGenerationMultiplier { get; set; }    // This value will be saved
        public float OxygenGenerationMultiplier { get; set; }   // This value will not be saved
        protected float TimeSinceLastUpdate { get; set; } = UpdatePeriodSeconds;

        public ModuleTest()
        {
            PowerGenerationMultiplier = Random.value * 5;
        }

        public override void serialize(XmlNode parent, string name)
        {
            base.serialize(parent, name);
            var lastChild = parent.LastChild;
            Serialization.serializeFloat(lastChild, GenerationMultiplierTagName, PowerGenerationMultiplier);
        }

        public override void deserialize(XmlNode node)
        {
            base.deserialize(node);
            PowerGenerationMultiplier = Serialization.deserializeFloat(node[GenerationMultiplierTagName]);
        }

        public override int getMaxPowerGeneration()
        {
            return Mathf.RoundToInt(PowerGenerationMultiplier * base.getMaxPowerGeneration());
        }

        public override int getOxygenGeneration()
        {
            return Mathf.RoundToInt(OxygenGenerationMultiplier * base.getOxygenGeneration());
        }

        public override void update(float timeStep, long frameIndex)
        {
            // This is important. If the base update function is not called then many game functions will not
            // work properly.
            base.update(timeStep, frameIndex);
            
            // Only update every UpdatePeriodSeconds
            TimeSinceLastUpdate += timeStep;
            if (TimeSinceLastUpdate < UpdatePeriodSeconds)
                return;

            TimeSinceLastUpdate = 0;
            OxygenGenerationMultiplier = Random.value * 10;
        }
    }
}