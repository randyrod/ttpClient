using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using ttpClient.Helpers;

namespace ttpClient.ViewModels
{
    public class RegistrationPageViewModel : BaseViewModel, INavigationAware
    {

        private readonly INavigationService _navigationService;

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand => _refreshCommand != null ? _refreshCommand : (_refreshCommand = new DelegateCommand(Authenticate));

        public RegistrationPageViewModel()
        {
            IsEmpty = false;
        }

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set { SetProperty(ref _loading, value); }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set { SetProperty(ref _message, value); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { SetProperty(ref _isEmpty, value); }
        }

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
            base.OnNavigatedTo(parameters);

            Authenticate();
        }

        private async void Authenticate()
        {
            Loading = true;

            if (string.IsNullOrEmpty(Settings.AccessToken))
            {
                //perform request
                Message = "Authenticating with service...";
                await Task.Delay(2000);
            }
            else
            {
                //await _navigationService.NavigateAsync("");
            }

            Loading = false;
        }
	}
}
