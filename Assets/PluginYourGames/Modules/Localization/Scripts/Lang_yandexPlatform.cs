#if YandexGamesPlatform_yg
using System.Runtime.InteropServices;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public string GetLanguage()
        {
            return LangRequest_js();
        }

        [DllImport("__Internal")]
        private static extern string LangRequest_js();
    }
}
#endif