namespace Component.Utility
{
    public static class AppSettingsProvider
    {
        public static string SocketServerIP { get; set; }
        public static string SocketListeningPort { get; set; }
        public static string ConnectionString { get; set; }
        public static string UserImageFolderPath { get; set; }
        public static string DriverImageFolderPath { get; set; }
        public static string LicenseImageFolderPath { get; set; }
        public static string TopupReceiptsImageFolderPath { get; set; }
        public static string NexmoMessageSenderName { get; set; }
        public static string PaypalAPIGetTokenUrl { get; set; }
        public static string PaypalAPIVerificationUrl { get; set; }
        public static string PaypalUsername { get; set; }
        public static string PaypalPassword { get; set; }
        public static string PaypalAPIMakePaymentUrl { get; set; }
        public static string APIBaseURL { get; set; }

        public static string RegistrationCopyImageFolderPath { get; set; }
        public static string CarImageFolderPath { get; set; }
        public static string RideImageFolderPath { get; set; }
        public static string GoogleMapsAPIKey { get; set; }

        public static string FCMSenderIdDriver { get; set; }
        public static string FCMSenderIdUser { get; set; }
        public static string AndroidUserServerKey { get; set; }
        public static string RideLaterService { get; set; }

        public static string FCMServerKeyUserApp { get; set; }
        public static string FCMServerKeyDriverApp { get; set; }
        public static string EmailHost { get; set; }
        public static string EmailPort { get; set; }
        public static string EmailSSL { get; set; }
        public static string EmailUseDefaultCredentials { get; set; }
        public static string FromMailAddress { get; set; }
        public static string FromPassword { get; set; }
        public static string FromMailName { get; set; }

        public static string NexmoApiKey { get; set; }
        public static string NexmoApiSecret { get; set; }
        public static string NexmoFromNumber { get; set; }
        public static string NexmoApplicationId { get; set; }

        public static string DataBaseName { get; set; }

        public static string DBUser { get; set; }
        public static string Password { get; set; }
        public static string DataSource { get; set; }
        public static string DatabaseBackUpUrl { get; set; }
        public static string DatabaseBackUpZipUrl { get; set; }

        






    }
}
