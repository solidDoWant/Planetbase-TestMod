using Planetbase;
using PlanetbaseFramework.GameMechanics.Resources;
using UnityEngine;

namespace TestMod
{
    public class TestResourceType : BaseResourceType
    {
        public TestResourceType(Texture2D icon, GameObject model) : base(icon, model)
        {
            mStatsColor = Color.magenta;
            mValue = 123;
            mMerchantCategory = MerchantCategory.Electronics;
            mFlags = FlagManufactured + FlagWarningWhenNone;
        }
    }
}