using System.Threading.Tasks;

namespace ttpClient.Helpers
{
    public class DisplayAlertHelper
    {
        public async Task ShowMessage(string title, string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, "Ok");
        }

        public async Task<bool> ShowConfirm(string title, string message)
        {
            return await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, "Si", "No");
        }
    }
}
