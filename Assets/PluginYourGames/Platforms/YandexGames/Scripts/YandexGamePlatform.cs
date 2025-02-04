#if YandexGamesPlatform_yg
using System.Runtime.InteropServices;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void InitAwake()
        {
            if (YG2.infoYG.Basic.syncInitSDK)
            {
#if !UNITY_EDITOR
                if (IsInitSDK_js())
                    YG2.SyncInitialization();
#else
                YG2.SyncInitialization();
#endif
            }
        }

        public void InitComplete()
        {
#if !UNITY_EDITOR
            InitGame_js();
#endif
        }

        public void GameReadyAPI()
        {
#if !UNITY_EDITOR
            GameReadyAPI_js();
#endif
        }

        public void GameplayStart()
        {
#if !UNITY_EDITOR
            GameplayStart_js();
#endif
        }

        public void GameplayStop()
        {
#if !UNITY_EDITOR
            GameplayStop_js();
#endif
        }

        public void Message(string message) => LogStyledMessage(message);

        [DllImport("__Internal")]
        private static extern bool IsInitSDK_js();

        [DllImport("__Internal")]
        private static extern void InitGame_js();

        [DllImport("__Internal")]
        private static extern void GameReadyAPI_js();

        [DllImport("__Internal")]
        private static extern void GameplayStart_js();

        [DllImport("__Internal")]
        private static extern void GameplayStop_js();

        [DllImport("__Internal")]
        private static extern void LogStyledMessage(string message);
    }
}

namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void InitSDKComplete() => YG2.SyncInitialization();
    }
}
#endif