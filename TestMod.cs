using System;
using System.Collections.Generic;
using Planetbase;
using PlanetbaseFramework;
using PlanetbaseFramework.GameMechanics.Buildings;
using PlanetbaseFramework.GameMechanics.Components;
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
            RegisterNewComponentTypes();    // This should be called before registering modules types that may reference the new component types
            RegisterNewModuleTypes();
        }

        public void RegisterNewComponentTypes()
        {
            // Build the new component model
            var componentModel = new ComponentModelBuilder()
                .AddMeshObject(ComponentModelBuilder.AddCollisionGeometryRecursively(ModObjects.FindObjectByFilename("test.obj")))
                .GenerateObject();
            componentModel.transform.localScale = 0.5f * Vector3.one;   // Reduce the size to 1/8th the original volume (0.5 ^ (number of dimensions, 3))

            // Create the new component type and register it
            ComponentTypeList.getInstance().add(new TestComponentType(ModTextures.FindTextureWithName("icon.png"), componentModel));
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
            // Register the new module type
            ModuleTypeList.getInstance().add(testModuleType);
        }

        public override ICollection<string> GetContributors()
        {
            return new[] { "solidDoWant" };
        }
    }
}