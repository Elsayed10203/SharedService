using System;
using Xamarin.Forms;

namespace Services
{
    public interface IToasterServices
    {
        void ShowAlertmessage(string Mess);
    }

    public class ToasterServices : IToasterServices
    {

        public void ShowAlertmessage(string Mess)
        {
            try
            {
                Application.Current.MainPage.DisplayAlert("Alert", Mess, "Ok");
            }
            catch (Exception)
            {

            }
        }
    }
}
