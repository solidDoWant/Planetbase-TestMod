using Planetbase;
using PlanetbaseFramework;

namespace TestMod
{
    public class TestMod : ModBase
    {
        public override string ModName => "Test mod";

        public override void Init()
        {
            RegisterNewModuleTypes();
        }

        public void RegisterNewModuleTypes()
        {
            // This is a global Planetbase object that contains all the ModuleTypes that players can choose from
            var moduleList = TypeList<ModuleType, ModuleTypeList>.getInstance();
            moduleList.add(
                // Note that these files are marked as "embedded resources" in the Visual Studio project.
                // See the TestMod.csproj file for details.
                new ModuleTypeTest(
                    // This is the icon that will be used on the module selection screen.
                    ModTextures.FindTextureWithName("icon.png"),
                    // This is an array of GameObjects that will be used as models for each module instance.
                    // Each entry in the array corresponds with a "size" of the module, which players can select
                    // by scrolling up and down when placing.
                    new[]
                    {
                        ModObjects.FindObjectByFilename("test.obj")
                    }
                )
            );
        }
    }
}