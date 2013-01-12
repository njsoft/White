using White.Core.Configuration;

namespace White.Core
{
    public static class Constants
    {
        public const string MissingFrameworkId = "";
        public const string WPFFrameworkId = FrameworkIds.WPFFrameworkId;
        public const string WinFormFrameworkId = FrameworkIds.WinFormFrameworkId;
        public const string Win32FrameworkId = FrameworkIds.Win32FrameworkId;
        public const string SWT = FrameworkIds.SWT;
        public const string SilverlightFrameworkId = FrameworkIds.SilverlightFrameworkId;

        public static string BusyMessage
        {
            get { return string.Format(", after waiting for {0} ms", CoreAppXmlConfiguration.Instance.BusyTimeout); }
        }
    }
}