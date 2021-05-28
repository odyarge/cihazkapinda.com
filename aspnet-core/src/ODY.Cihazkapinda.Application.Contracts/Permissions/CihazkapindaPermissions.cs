namespace ODY.Cihazkapinda.Permissions
{
    public static class CihazkapindaPermissions
    {
        public const string GroupName = "Cihazkapinda";
        public const string SiteSettingsGroup = "SiteSettings";
        public const string ThemeSettingsGroup = "ThemeSettings";
        public const string GeneralSettingsGroup = "GeneralSettings";
        public const string BannerSettingsGroup = "BannerSettings";

        public static class SiteSettings
        {
            public const string SiteSettingDefault = SiteSettingsGroup + ".SiteSettings";
            public const string MenuList = SiteSettingDefault + ".MenuList";
            public const string List = SiteSettingDefault + ".List";
            public const string Create = SiteSettingDefault + ".Create";
            public const string Edit = SiteSettingDefault + ".Edit";
            public const string Delete = SiteSettingDefault + ".Delete";
        }

        public static class GeneralSettings
        {
            public const string GeneralSettingDefault = GeneralSettingsGroup + ".GeneralSettings";
            public const string MenuList = GeneralSettingDefault + ".MenuList";
            public const string List = GeneralSettingDefault + ".List";
            public const string Create = GeneralSettingDefault + ".Create";
            public const string Edit = GeneralSettingDefault + ".Edit";
            public const string Delete = GeneralSettingDefault + ".Delete";
        }

        public static class ThemeSettings
        {
            public const string ThemeSettingDefault = ThemeSettingsGroup + ".ThemeSettings";
            public const string MenuList = ThemeSettingDefault + ".MenuList";
            public const string List = ThemeSettingDefault + ".List";
            public const string Create = ThemeSettingDefault + ".Create";
            public const string Edit = ThemeSettingDefault + ".Edit";
            public const string Delete = ThemeSettingDefault + ".Delete";
        }

        public static class BannerSettings
        {
            public const string BannerSettingDefault = BannerSettingsGroup + ".BannerSettings";
            public const string MenuList = BannerSettingDefault + ".MenuList";
            public const string List = BannerSettingDefault + ".List";
            public const string Create = BannerSettingDefault + ".Create";
            public const string Edit = BannerSettingDefault + ".Edit";
            public const string Delete = BannerSettingDefault + ".Delete";
        }

        public static class BannerImages
        {
            public const string BannerImageDefault = BannerSettingsGroup + ".BannerImages";
            public const string MenuList = BannerImageDefault + ".MenuList";
            public const string List = BannerImageDefault + ".List";
            public const string Create = BannerImageDefault + ".Create";
            public const string Edit = BannerImageDefault + ".Edit";
            public const string Delete = BannerImageDefault + ".Delete";
        }
    }
}