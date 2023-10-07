using System;
using System.Collections.Generic;
using Planetbase;
using PlanetbaseFramework;
using PlanetbaseFramework.GameMechanics.Buildings;
using UnityEngine;

namespace TestMod
{
    public class TestMod : ModBase
    {
        public const string AssemblyVersion = "1.0.0.0";    // This is declared as a const so that it can be referenced in AssemblyInfo.cs annotations
        public override string ModName => "Test mod";
        public override Version ModVersion => new Version(AssemblyVersion);

        public override void Init()
        {
            RegisterNewModuleTypes();
        }

        public void RegisterNewModuleTypes()
        {
            // Note that ModTexture and ModObject files are marked as "embedded resources" in the Visual Studio project.
            // See the TestMod.csproj file for details.

            // Create a model builder that can be used to produce models with the specified configuration
            var modelBuilder = new ModuleModelBuilder().AddPreStructuredObjects(ModObjects.FindObjectByFilename("test.obj"));

            // Add collision to appropriate parts of the model
            foreach (var floorObject in modelBuilder.FloorObjects)
                ModuleModelBuilder.AddCollisionGeometry(floorObject);

            // Generate several models with different sizes
            var models = new List<GameObject>();
            for (float i = 1; i <= 2; i += 0.5f)
            {
                var model = modelBuilder.GenerateObject($"size_scaled_{i}");
                model.transform.localScale = i * Vector3.one;
                models.Add(model);
            }

            // Create the new object type, providing the UI icon and models for each size
            var testModuleType = new ModuleTypeTest(ModTextures.FindTextureWithName("icon.png"), models.ToArray());

            // This is a global Planetbase object that contains all the ModuleTypes that players can choose from
            var moduleList = TypeList<ModuleType, ModuleTypeList>.getInstance();

            // Register the new module type
            moduleList.add(testModuleType);
        }

        public override ICollection<string> GetContributors()
        {
            return new[] { "solidDoWant" };
        }
    }
}