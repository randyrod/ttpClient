using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using ttpClient.Api.RequestModels;
using ttpClient.Helpers;

namespace ttpClient.ViewModels
{
    public class RegistrationPageViewModel : BaseViewModel
    {
        private DelegateCommand _authenticateCommand;
        public DelegateCommand AuthenticateCommand => _authenticateCommand != null ? _authenticateCommand : (_authenticateCommand = new DelegateCommand(Authenticate));

        public RegistrationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            IsEmpty = false;
            RegisterVisible = false;
        }

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetProperty(ref _isEmpty, value);
        }

        private bool _registerVisible;
        public bool RegisterVisible
        {
            get => _registerVisible;
            set => SetProperty(ref _registerVisible, value);
        }

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
            base.OnNavigatedTo(parameters);

            ValidateAccess();
        }

        private async void ValidateAccess()
        {
            Loading = true;

            if(string.IsNullOrEmpty(Settings.AccessToken))
            {
                RegisterVisible = true;
            }
            else
            {
                await _navigationService.NavigateAsync("Navigation/MainPage");
            }

            Loading = false;
        }

        private async void Authenticate()
        {
            try
            {
                Loading = true;

                if (string.IsNullOrEmpty(Settings.AccessToken))
                {
                    Message = "Authenticating with service...";

                    if(string.IsNullOrEmpty(Name))
                    {
                        DisplayAlert("Error", "Please provide an identifier");
                        return;
                    }

                    var keyPair = CryptoHelper.CreateRSAKey();



                    if(keyPair == null)
                    {
                        DisplayAlert("Error", "There was an issue performing the request");
                        return;
                    }

                    var requestContent = new AuthenticationRequestModel
                    {
                        Name = this.Name,
                        PublicKey = keyPair.Item2
                    };

                    var parameters = new ApiRequestParameters
                    {
                        Url = "",
                        Content = JsonConvert.SerializeObject(requestContent),
                        Method = HttpMethod.Post
                    };

                    var response = await PerformApiRequest(parameters);

                    if (response == null) return;
                }
                else
                {
                    await _navigationService.NavigateAsync("Navigation/MainPage");
                }

                Loading = false;
            } catch (Exception) 
            {
                Loading = false;
            }
        }
	}
}
