using System.Collections.Generic;

namespace MS.Accountant.Application.Module
{
    public class TaxesSettings
    {
        public const string Key = nameof(TaxesSettings);

        public Dictionary<string, TaxSettings> Taxes { get; set; }
    }
}
