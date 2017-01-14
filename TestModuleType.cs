using Planetbase;
using PlanetbaseFramework;
using UnityEngine;

namespace TestMod
{
    class TestModuleType : ModuleType
    {
        public TestModuleType()
        {
            this.mIcon = Utils.LoadPNGFromFile(@"somepng.png");
            this.mPowerGeneration = -1000;
            this.mExterior = false;
            this.mMinSize = 2;
            this.mMaxSize = 2;
            this.mHeight = 1f;
            this.mRequiredStructure.set<ModuleTypeOxygenGenerator>();
            //this.mExteriorNavRadius = 3f;
            this.initStrings();
            this.mCost = new ResourceAmounts();
            this.mFlags = 16 + 32 + 2048;
            this.mLayoutType = ModuleType.LayoutType.Circular;
        }

        public new ResourceAmounts calculateCost(int sizeIndex)
        {
            ResourceAmounts resources = new ResourceAmounts();

            resources.add(TypeList<ResourceType, ResourceTypeList>.find<Metal>(), sizeIndex * 2);
            resources.add(TypeList<ResourceType, ResourceTypeList>.find<Bioplastic>(), sizeIndex * 3);

            return resources;
        }

        public override GameObject loadPrefab(int sizeIndex)
        {
            GameObject moduleObject = ImportOBJ.createGameObject(ImportOBJ.import(@"pathtoobj.obj"));
            //moduleObject.calculateSmoothMeshRecursive(ModuleType.mMeshes);
            if (moduleObject.GetComponent<Collider>() != null)
            {
                Debug.LogWarning("COLLISION IN THE ROOT");
            }
            GameObject moduleObject2 = GameObject.Find(ModuleType.GroupName);
            if (moduleObject2 == null)
            {
                moduleObject2 = new GameObject();
                moduleObject2.name = ModuleType.GroupName;
            }
            moduleObject.transform.SetParent(moduleObject2.transform, false);
            moduleObject.SetActive(false);
            return moduleObject;
        }
    }
}
