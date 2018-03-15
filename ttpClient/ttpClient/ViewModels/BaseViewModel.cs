﻿using System;
using System.Threading.Tasks;
using Plugin.Connectivity;
using ttpClient.Helpers;

namespace ttpClient.ViewModels
{
    public class BaseViewModel
    {
        private readonly DisplayAlertHelper _displayAlertHelper;

        public BaseViewModel()
        {
            _displayAlertHelper = new DisplayAlertHelper();
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
    }
}