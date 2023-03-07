using TwoN.Common.Interfaces;

namespace TwoN.Common.Error
{
    public class ErrorResponse : IResponse
    {
        public ErrorResponse(int code, string param, string description)
        {
            Code = code;
            Param = param;
            Description = description;
        }

        public int Code { get; private set; }
        public string Param { get; private set; }
        public string Description { get; private set; }

        public bool Success => false;
    }
}