#if YandexGamesPlatform_yg
using System.Runtime.InteropServices;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void InitReview()
        {
#if !UNITY_EDITOR
            if (InitReview_js() == "true")
                YG2.reviewCanShow = true;
#else
            YG2.reviewCanShow = true;
#endif
        }

        public void ReviewShow()
        {
#if !UNITY_EDITOR
            Review_js();
#else
            YGInsides.ReviewSent(true);
#endif
        }

        [DllImport("__Internal")]
        private static extern string InitReview_js();

        [DllImport("__Internal")]
        private static extern void Review_js();
    }
}

namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void ReviewSent(string feedbackSent)
        {
            bool sent = feedbackSent == "true" ? true : false;
            YGInsides.ReviewSent(sent);
        }
    }
}
#endif