using Planetbase;
using PlanetbaseFramework;
using System;
using UnityEngine;

namespace TestMod
{
    public class TestMod : ModBase
    {
        public TestMod() : base("Test mod")
        {
        }

        public override void Init()
        {
            TypeList<ModuleType, ModuleTypeList> moduleList = TypeList<ModuleType, ModuleTypeList>.getInstance();
            moduleList.add(new TestModuleType());
        }

        public override void Update()
        {
        }
    }
}
