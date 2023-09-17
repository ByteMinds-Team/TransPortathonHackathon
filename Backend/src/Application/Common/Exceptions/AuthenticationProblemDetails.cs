using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.Common.Exceptions;

public class AuthenticationProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}