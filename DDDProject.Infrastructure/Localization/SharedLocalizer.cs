using System.Globalization;
using Microsoft.Extensions.Localization;

namespace DDDProject.Infrastructure.Localization
{
    public class SharedLocalizer
    {
        private readonly IStringLocalizer<SharedLocalizer> _localizer;

        public SharedLocalizer(IStringLocalizer<SharedLocalizer> localizer)
        {
            _localizer = localizer;
        }

        public string Get(string key)
        {
            return _localizer[key];
        }

        public void SetCulture(string culture)
        {
            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
        }
    }
}