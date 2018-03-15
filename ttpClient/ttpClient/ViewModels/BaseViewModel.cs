using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Prism.Mvvm;
using Prism.Navigation;
using ttpClient.Api;
using ttpClient.Api.RequestModels;
using ttpClient.Helpers;

namespace ttpClient.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected INavigationService _navigationService;

        private readonly DisplayAlertHelper _displayAlertHelper;

        private ApiRequestHelper _apiRequest;

        public BaseViewModel(INavigationService navigationService)
        {
            _displayAlertHelper = new DisplayAlertHelper();
            _navigationService = navigationService;
            _apiRequest = new ApiRequestHelper();
        }

        public async Task<bool> CheckInternetStatus()
        {
            try
            {
                var connected = !(!CrossConnectivity.Current.IsConnected ||
                    !await CrossConnectivity.Current.IsRemoteReachable("http://www.google.com"));

                if (!connected)
                {
                    DisplayAlert("Error", "There is no network access");
                }

                return connected;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async void DisplayAlert(string title, string message)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(message)) return;

            await _displayAlertHelper.ShowMessage(title, message);
        }

        public async Task<string> PerformApiRequest(ApiRequestParameters parameters)
        {
            try
            {
                if (parameters == null) return null;

                if (!await CheckInternetStatus()) return null;

                var response = await _apiRequest.GetHttpResponseFromRequest(parameters);

                if (response == null || !response.IsSuccessStatusCode)
                {
                    DisplayAlert("Error", "There was an issue communicating with the services");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content))
                {
                    DisplayAlert("Error", "The content had an issue.");
                    return null;
                }

                return content;
            }
            catch (Exception)
            {
                DisplayAlert("Error", "Communication issue");
                return null;
            }
        }


        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
