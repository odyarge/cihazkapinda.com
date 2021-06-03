namespace ODY.Cihazkapinda.Web.Menus
{
    public class CihazkapindaMenus
    {
        private const string Prefix = "Cihazkapinda";
        public const string Home = Prefix + ".Home";

        #region SETTINGS
        public const string Settings = Prefix + ".Settings";
        public const string SiteSettings = Settings + ".SiteSettings";
        public const string GeneralSettings = Settings + ".GeneralSettings";
        public const string ThemeSettings = Settings + ".ThemeSettings";
        public const string BannerSettings = Settings + ".BannerSettings";
        public const string OperatorSettings = Settings + ".OperatorSettings";
        public const string Licenses = Settings + ".Licenses";
        #endregion SETTINGS

        #region COMPONENTS
        public const string Components = Prefix + ".Components";
        public const string Banner = Components + ".Banner";
        public const string BannerImages = Components + ".BannerImages";
        public const string Categories = Components + ".Categories";
        #endregion COMPONENTS

        #region PRODUCTMANAGEMENT
        public const string ProductManagement = Components + ".ProductManagement";
        public const string Products = ProductManagement + ".Products";
        public const string ProductAdd = ProductManagement + ".ProductAdd";
        public const string ProductImages = ProductManagement + ".ProductImages";
        #endregion PRODUCTMANAGEMENT

        //Add your menu items here...

    }
}