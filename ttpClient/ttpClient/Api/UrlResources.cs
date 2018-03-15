namespace ttpClient.Api
{
    public static class UrlResources
    {
        public const string ApiBaseUrl = "http://newhuaweiapstudent-10-58-195-152.ncl.ac.uk:8000";

        public const string RegisterUrl = "registry";

        public const string TransactionsUrl = "transations";

        public const string TransactionConfirmUrlFormat = "transations/{0}/confirm"; //param: Transaction Id 
    }
}
