using System;
using Prism.Commands;
using Prism.Navigation;

namespace ttpClient.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public DelegateCommand<string> NavigationCommand { get; }

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationCommand = new DelegateCommand<string>(OnNavigationExecuted);
        }

        private async void OnNavigationExecuted(string param)
        {
            await _navigationService.NavigateAsync(param);
        }
    }
}
