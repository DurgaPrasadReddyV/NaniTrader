using CsvHelper.Configuration;

namespace NaniTrader.Fyers
{
    public sealed class FyersRawSymbolMap : ClassMap<FyersRawSymbolDto>
    {
        public FyersRawSymbolMap()
        {
            Map(f => f.Column1).Index(0);
            Map(f => f.Column2).Index(1);
            Map(f => f.Column3).Index(2);
            Map(f => f.Column4).Index(3);
            Map(f => f.Column5).Index(4);
            Map(f => f.Column6).Index(5);
            Map(f => f.Column7).Index(6);
            Map(f => f.Column8).Index(7);
            Map(f => f.Column9).Index(8);
            Map(f => f.Column10).Index(9);
            Map(f => f.Column11).Index(10);
            Map(f => f.Column12).Index(11);
            Map(f => f.Column13).Index(12);
            Map(f => f.Column14).Index(13);
            Map(f => f.Column15).Index(14);
            Map(f => f.Column16).Index(15);
            Map(f => f.Column17).Index(16);
            Map(f => f.Column18).Index(17);
        }
    }
}
