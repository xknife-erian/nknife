using System;

namespace NKnife.Util
{
    public static class VersionUtil
    {
        private const uint MAJOR_BITS = 0xFF000000;
        private const uint MINOR_BITS = 0x00FF0000;
        private const uint PATCH_BITS = 0x0000FF00;
        private const uint BUILD_BITS = 0x000000FF;

        public static int NullVersion => 0;

        public static int FirstVersion => 1 << 16;

        public static int UpgradeMajor(int version)
        {
            return version + 0x1000000;
        }
        public static int UpgradeMinor(int version)
        {
            return version + 0x10000;
        }
        public static int UpgradePatch(int version)
        {
            return version + 0x100;
        }
        public static int UpgradeBuild(int version)
        {
            return version + 1;
        }

        public static int Create(int major, int minor, int patch = 0, int build = 0)
        {
            return (int)(((major << 24) & MAJOR_BITS) | ((minor << 16) & MINOR_BITS) | ((patch << 8) & PATCH_BITS) | (build & BUILD_BITS));
        }

        public static (int major, int minor, int patch, int build) ParseVersion(int version)
        {
            return ((version >> 24) & 0xFF, (version >> 16) & 0xFF, (version >> 8) & 0xFF, version & 0xFF);
        }

        public static int Major(int version) => (int)((version & MAJOR_BITS) >> 24);
        public static int Minor(int version) => (int)((version & MINOR_BITS) >> 16);
        public static int Patch(int version) => (int)((version & PATCH_BITS) >> 8);
        public static int Build(int version) => (int)(version & BUILD_BITS);
        
        public static string ToString(int version) => $"{Major(version)}.{Minor(version)}.{Patch(version)}.{Build(version)}";
        
        public static Version ToVersion(int version) => new Version(Major(version),Minor(version),Patch(version),Build(version));

        public static int Convert(Version version) => Create(version.Major, version.Minor, version.Build, version.Revision);
    }

}