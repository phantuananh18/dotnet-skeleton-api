using DotnetSkeleton.SharedKernel.Utils;

namespace DotnetSkeleton.API.Extensions.Middleware.ApiHeaderHandling
{
    public class ApiHeaderHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiHeaderHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var languageCode = context.GetHeaderValue(Constant.HeaderAttribute.ContentLanguage) ?? Constant.HeaderAttribute.DefaultContentLanguage;
            var timeZone = context.GetHeaderValue(Constant.HeaderAttribute.Timezone) ?? Constant.HeaderAttribute.DefaultTimeZone;

            // Set default culture based on language code
            CultureInfo.CurrentCulture = new CultureInfo(languageCode);
            CultureInfo.CurrentUICulture = new CultureInfo(languageCode);

            // Session management
            context.Session.SetString(Constant.HeaderAttribute.ContentLanguage, languageCode);
            context.Session.SetString(Constant.HeaderAttribute.Timezone, timeZone);

            await _next(context);
        }
    }
}
