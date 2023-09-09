using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Services
{
    public interface InavigationService
    {
        Task NavigatePush(ContentPage page);
        void NavigateToMainPage(ContentPage page);
        Task NavigateModal(ContentPage page);
        void NavigateModalOnePageNavigation(ContentPage page);

        void PopPage();
        void PopToRootPage();
        void PopModalPage();
    }

    public class NavigationService : InavigationService
    {
        public void NavigateToMainPage(ContentPage page)
        {
            try
            {
                Application.Current.MainPage = new NavigationPage(page);
            }
            catch (Exception)
            {
            }
        }
        public async Task NavigateModal(ContentPage page)
        {
            try
            {
                if (Application.Current.MainPage.Navigation.ModalStack.Count > 0 &&
                  Application.Current.MainPage.Navigation.ModalStack.LastOrDefault().GetType().Name == page.GetType().Name)
                {
                    return;
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(page);

                }

            }
            catch (Exception Excep)
            {


            }
        }
        public async Task NavigatePush(ContentPage page)
        {
            try
            {
                if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0 &&
               Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault().GetType().Name == page.GetType().Name)
                {
                    return;
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(page);

                }

            }
            catch (Exception Excep)
            {
            }
        }

        public void PopPage()
        {
            try
            {
                if (Application.Current.MainPage.Navigation.ModalStack.Count > 0)
                {
                    Application.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                    Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception EXCEP)
            {

            }
        }

        public void PopModalPage()
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public void PopToRootPage()
        {
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public void NavigateModalOnePageNavigation(ContentPage page)
        {
            try
            {
                if (Application.Current.MainPage.Navigation.ModalStack.Count > 0)
                {
                    Application.Current.MainPage.Navigation.PopModalAsync();
                }
                Application.Current.MainPage.Navigation.PushModalAsync(page);
            }
            catch (Exception Excep)
            {

            }
        }
    }
}
