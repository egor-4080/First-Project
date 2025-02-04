using System;
using YG.Insides;

namespace YG
{
    public partial class InfoYG
    {
        public PlatformInfo platformInfo = new PlatformInfo();
    }
}

namespace YG.Insides
{
    [Serializable]
    public partial class PlatformInfo
    {
        /// [ApplySettings] [SelectPlatform] [DeletePlatform]
    }
}