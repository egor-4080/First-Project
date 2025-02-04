#if YandexGamesPlatform_yg
using System.Runtime.InteropServices;
using UnityEngine;
using YG.Insides;
#if NJSON_STORAGE_YG2
using Newtonsoft.Json;
#endif

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void LoadCloud()
        {
            if (!YG2.isSDKEnabled)
            {
                YGInsides.SetLoadSaves(InitCloud_js());
            }
            else
            {
                LoadCloud_js();
            }
        }

        public void SaveCloud()
        {
#if NJSON_STORAGE_YG2
            SaveCloud_js(JsonConvert.SerializeObject(YG2.saves), YG2.infoYG.Storage.flush);
#else
            SaveCloud_js(JsonUtility.ToJson(YG2.saves), YG2.infoYG.Storage.flush);
#endif
        }

        [DllImport("__Internal")]
        private static extern string InitCloud_js();

        [DllImport("__Internal")]
        private static extern string LoadCloud_js();

        [DllImport("__Internal")]
        private static extern void SaveCloud_js(string jsonData, bool flush);
    }
}

namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void SetLoadSaves(string data) => YGInsides.SetLoadSaves(data);
    }
}
#endif