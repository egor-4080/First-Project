#if YandexGamesPlatform_yg
using System.Runtime.InteropServices;
using UnityEngine;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void InitEnirData()
        {
            string data = InitEnvironmentData_js();
            if (data != InfoYG.NO_DATA)
                YG2.envir = JsonUtility.FromJson<YG2.EnvirData>(data);
        }

        public void GetEnvirData()
        {
            RequestingEnvironmentData_js();
        }

        [DllImport("__Internal")]
        private static extern string InitEnvironmentData_js();

        [DllImport("__Internal")]
        private static extern string RequestingEnvironmentData_js();
    }
}

namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void SetEnvirData(string data)
        {
            YG2.envir = JsonUtility.FromJson<YG2.EnvirData>(data);
            GetDataInvoke();
        }
    }
}
#endif