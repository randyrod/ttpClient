using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ttpClient.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

        private const string PublicKeyKey = "public_key";
        private const string PrivateKeyKey = "private_key";
        private const string AccessTokenKey = "access_token_key";
		private static readonly string SettingsDefault = string.Empty;

		#endregion

        public static string PublicKey
        {
            get => AppSettings.GetValueOrDefault(PublicKeyKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(PublicKeyKey, value);
        }

        public static string PrivateKey
        {
            get => AppSettings.GetValueOrDefault(PrivateKeyKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(PrivateKeyKey, value);
        }

        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault(AccessTokenKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(AccessTokenKey, value);
        }

	}
}