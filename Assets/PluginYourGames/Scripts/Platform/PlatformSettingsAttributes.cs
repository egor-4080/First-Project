using System;
using UnityEngine;

namespace YG.Insides
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApplySettingsAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SelectPlatformAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class DeletePlatformAttribute : Attribute
    {
    }

    public class PlatformAttribute : PropertyAttribute
    {
        public PlatformAttribute(string platformName)
        {
            name = platformName;
        }

        public string name { get; private set; }
    }
}