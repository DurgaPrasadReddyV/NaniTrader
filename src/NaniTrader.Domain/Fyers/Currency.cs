using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Values;

namespace NaniTrader.Fyers
{
    public class Currency : ValueObject
    {
        public decimal Amount { get; private set; }
        public string IsoCode { get; private set; }
        public bool IsDigital { get; private set; }
        public string GeneralName { get; private set; }
        public string Symbol { get; private set; }
        public int DecimalPlace { get; private set; }
        public int BaseDecimalPlace { get; private set; }
        public string DecimalMark { get; private set; }
        public string ThousandMark { get; private set; }

        private CurrencyTypeRepository _repo = new CurrencyTypeRepository();

        public Currency(string isoCode, decimal amount)
        {
            if (!_repo.Exists(isoCode))
            {
                throw new ArgumentException("Invalid ISO Currency Code");
            }

            var newCurrency = _repo.Get(isoCode);
            IsoCode = newCurrency.IsoCode;
            IsDigital = newCurrency.IsDigital;
            GeneralName = newCurrency.GeneralName;
            Symbol = newCurrency.Symbol;
            DecimalPlace = newCurrency.DecimalPlace;
            BaseDecimalPlace = newCurrency.BaseDecimalPlace;
            DecimalMark = newCurrency.DecimalMark;
            ThousandMark = newCurrency.ThousandMark;
            Amount = amount;
        }

        internal Currency(string isoCode, bool isDigital, string generalName, string symbol, int decimalPlace, int baseDecimalPlace, string decimalMark, string thousandMark)
        {
            IsoCode = isoCode;
            IsDigital = isDigital;
            GeneralName = generalName;
            Symbol = symbol;
            DecimalPlace = decimalPlace;
            BaseDecimalPlace = baseDecimalPlace;
            DecimalMark = decimalMark;
            ThousandMark = thousandMark;
        }

        public string GetStringFormat()
        {
            string decimalZero = "";
            for (int i = 1; i <= this.DecimalPlace; i++)
            {
                decimalZero += "0";
            }
            string specifier = "#" + this.ThousandMark + "0" + this.DecimalMark + decimalZero + ";(#,0." + decimalZero + ")";
            return specifier;
        }

        public override string ToString()
        {
            return this.IsoCode + Symbol;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return IsoCode;
            yield return IsDigital;
            yield return GeneralName;
            yield return Symbol;
            yield return DecimalPlace;
            yield return BaseDecimalPlace;
            yield return DecimalMark;
            yield return ThousandMark;
        }
    }
}
