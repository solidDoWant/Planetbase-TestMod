using Planetbase;
using PlanetbaseFramework.GameMechanics.Components;
using UnityEngine;

namespace TestMod
{
    public class TestComponentType : BaseComponent
    {
        public TestComponentType(Texture2D icon, GameObject model) : base(icon, model)
        {
            mConstructionCosts.add(TypeList<ResourceType, ResourceTypeList>.find<Meal>(), 3);

            // The maximum possible status recovery per usage, for a given status, is defined as:
            // maxPossibleStatRecovery = mMaxUsageTime / mStatusRecoveryTimes[status_index];

            mPrimaryStatusRecovery = CharacterIndicator.Hydration;          // This is the character stat that will be recovered by using this component
            mStatusRecoveryTimes[(int)CharacterIndicator.Hydration] = 360f; // How long the given status will take to improve from 0 to 100%, in seconds
            addUsageAnimation(CharacterAnimationType.Shoot);                // This is the animation that characters will use when using this component
            mMaxUsageTime = 30f;                                            // How long this can be used before the character must go do something else, in seconds
            mUsageCooldown = 120f;                                          // How long the character must wait before using one of these components again, in seconds
            mFlags = FlagNoPowerNeeded + FlagQuadrantAutoRotation + FlagRequiresTracksuit + FlagRequiresUncoveredHead + FlagHydrating;
            mRadius = 0.75f;    // This is used in several places to determine the distance at which two things should interact, such as the distance that the player must be within to use the component.
        }
    }
}