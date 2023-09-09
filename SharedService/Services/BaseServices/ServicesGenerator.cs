namespace  Services
{
    public sealed class ServicesGenerator
    {
         private static InavigationService INavigationService;
        private static IToasterServices IToasterServices;
       
        public static InavigationService GetINavigationService()
        {
            return INavigationService == null ? new NavigationService() : INavigationService;
        }
        public static IToasterServices GetIToasterServices()
        {
            return IToasterServices == null ? new ToasterServices() : IToasterServices;
        }
      }
}
