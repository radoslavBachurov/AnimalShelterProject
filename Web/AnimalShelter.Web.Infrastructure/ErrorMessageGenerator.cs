namespace AnimalShelter.Web.Infrastructure
{
    public class ErrorMessageGenerator
    {
        public ErrorInfo GenerateErrorInfo(int errorCode)
        {
            var info = new ErrorInfo();
            info.ErrorCode = errorCode;

            switch (errorCode)
            {
                case 401:
                    {
                        info.ErrorMessage = "Unauthorized Error";
                        info.ErrorText = "The request has not been applied because it lacks valid authentication credentials for the target resource.";
                    }

                    break;
                case 404:
                    {
                        info.ErrorMessage = "page not found";
                        info.ErrorText = "The page requested couldn't be found - this could be due to a spelling error in the URL or a removed page.";
                    }

                    break;
                case 503:
                    {
                        info.ErrorMessage = "Service is Unavailable";
                        info.ErrorText = "Sorry, we're offline right now to make our site even better. Please, come back later and check what we've been up to.";
                    }

                    break;
                default:
                    info.ErrorMessage = "Error";
                    info.ErrorText = "Report to a site administrator";
                    break;
            }

            return info;
        }
    }

    public class ErrorInfo
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorText { get; set; }
    }
}
