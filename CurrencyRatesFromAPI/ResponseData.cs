using System;
using System.Collections.Generic;

namespace CurrencyRatesFromAPI
{
    public class ResponseData
    {
        public string Table;
        public string No;
        public DateTime EffectiveDate;
        public List<Rate> Rates;
    }
}