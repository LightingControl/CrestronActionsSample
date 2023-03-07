using System;
using Newtonsoft.Json;
using TwoN.Common.Error;
using TwoN.Common.Error.Json;
using TwoN.Common.Interfaces;

namespace TwoN.Common
{
    internal class Utils
    {
        internal static TEnum ParseEnum<TEnum>(string value) where TEnum : struct
        {
            TEnum result;
            Enum.TryParse(value, true, out result);
            return result;
        }

        internal static IResponse ErrorResponse(string error)
        {
            ErrorResponseJson errorResponse = JsonConvert.DeserializeObject<ErrorResponseJson>(error);
            return new ErrorResponse(errorResponse.Error.Code, errorResponse.Error.Param, errorResponse.Error.Description);
        }
    }
}