using Planetbase;
using PlanetbaseFramework;
using UnityEngine;

namespace TestMod
{
    // The class name is important! The Planetbase code does some funky type and reflection logic based upon the class name.
    // Non-abstract classes should always begin with "ModuleType". The in-game name of the module type is determined by
    // a "strings" (localization) lookup for the class suffix. For example, the in-game name of this module will be
    // "Test module", which is determined by getting the class name suffix ("Test"), converting it to lower case ("test"),
    // and looking up the corresponding string which is set in assets/strings/testmod_en.xml.
    class ModuleTypeTest : BaseModuleType
    {
        public ModuleTypeTest(Texture2D icon, GameObject[] models) : base(icon, models)
        {
            mPowerGeneration = -1000;                               // How much power each module instance produces (positive value) or consumes (negative value)
            mExterior = false;                                      // Colonist can walk in interior structures
            mHeight = 1f;
            mRequiredStructure.set<ModuleTypeOxygenGenerator>();    // This controls what structure is required to be built before this one may be placed
            mFlags = FlagDome + FlagLightAtNight + FlagWalkable;    // Flags control misc. properties of the module
            mLayoutType = LayoutType.Circular;                      // This controls where colonists will path
        }

        public override ResourceAmounts calculateCost(int sizeIndex)
        {
            var resources = new ResourceAmounts();

            var adjustedSizeIndex = sizeIndex + 1;  // By default size indices are 0-based

            resources.add(TypeList<ResourceType, ResourceTypeList>.find<Metal>(), adjustedSizeIndex * 2);
            resources.add(TypeList<ResourceType, ResourceTypeList>.find<Bioplastic>(), adjustedSizeIndex * 3);

            return resources;
        }
    }
}
