namespace TwoN.Common.Error.Json
{
    internal class ErrorResponseJson
    {
        public bool Success { get; set; }
        public Json.ErrorResponse.Error Error { get; set; }
    }

    namespace Json.ErrorResponse
    {
        internal class Error
        {
            public int Code { get; set; }
            public string Param { get; set; }
            public string Description { get; set; }
        }
    }
}