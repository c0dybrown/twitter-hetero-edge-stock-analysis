using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApp6
{
    internal class Program
    {
        private static readonly string[] SymbolsToMap = { "NFLX","AMZN", "ORCL", "AAPL", "PYPL", "CSCO", "IBM" } ;
        private static readonly string PathMappingFile = @"C:\Users\cody\Misc\SocialMediaRawData\results\num_paths_all_symbols.json";
        private static readonly string UserStringGraphFile = @"C:\Users\cody\Misc\SocialMediaRawData\results\string_graph.json";

        private static readonly List<string> StockSymbols = new List<string>
        {
            "AMZN", "AAPL", "JNJ", "JPM", "XOM", "WMT", "BAC", "DIS", "CSCO", "UNH", "PFE", "INTC", "CVX", "WFC", "MRK", "CMCSA", "ORCL", "PEP", "NFLX", "MCD", "ABT", "ADBE", "NKE", "PYPL", "UNP", "HON", "IBM", "CRM", "AVGO", "MDT", "UTX", "ABBV", "LLY", "ACN", "TMO", "NVDA", "TXN", "COST", "AMGN", "MMM", "QCOM", "LIN", "AXP", "SBUX", "DHR", "LMT", "NEE", "UPS", "AMT", "LOW", "USB", "GILD", "CHTR", "BKNG", "BMY", "CAT", "MDLZ", "CVS", "BLK", "DWDP", "SYK", "COP", "ADP", "ANTM", "CELG", "TJX", "DUK", "CSX", "CME", "INTU", "BDX", "SCHW", "PNC", "ISRG", "SLB", "EOG", "NSC", "SPG", "SPGI", "ECL", "CCI", "BSX", "RTN", "ITW", "NOC", "WBA", "ZTS", "MMC", "ILMN", "PLD", "EXC", "FDX", "AGN", "ICE", "MAR", "MET", "OXY", "APD", "BIIB", "KMI", "AIG", "KMB", "VRTX", "HCA", "AON", "COF", "PGR", "AEP", "ADI", "EMR", "SHW", "PRU", "EQIX", "STZ", "AMAT", "KHC", "DOW", "BAX", "PSA", "CCL", "TGT", "PSX", "APC", "BBT", "ADSK", "AFL", "FIS", "MPC", "SYY", "VFC", "DAL", "TRV", "ROP", "MCO", "ATVI", "REGN", "ROST", "JCI", "SRE", "VLO", "ETN", "MNST", "FISV", "CTSH", "HUM", "WMB", "RHT", "EBAY", "ALL", "YUM", "LYB", "TEL", "WELL", "GIS", "PAYX", "HPQ", "XLNX", "ALXN", "TWTR", "LRCX", "AMD", "PEG", "ORLY", "XEL", "LUV", "APH", "TSN", "STI", "EQR", "FTV", "AVB", "OKE", "PPG", "RCL", "RSG", "IQV", "HLT", "CMI", "HSY", "ALGN", "DFS", "PXD", "TDG", "DLTR", "MCK", "BF.B", "AZO", "ZBH", "NEM", "DLR", "WEC", "TROW", "GLW", "STT", "PCAR", "ADM", "BHGE", "CNC", "SBAC", "SYF", "MSI", "HAL", "CTAS", "FOXA", "VRSK", "VRSN", "MTB", "GPN", "FOX", "WLTW", "FLT", "DTE", "INFO", "UAL", "CERN", "PPL", "SWK", "MCHP", "CXO", "VTR", "HPE", "NTRS", "IDXX", "HRS", "HRL", "BLL", "APTV", "FITB", "ULTA", "MKC", "ANET", "ROK", "BXP", "BBY", "EIX", "CMG", "AME", "CDNS", "AMP", "HES", "HIG", "FAST", "MSCI", "CLX", "AWK", "KLAC", "MTD", "ESS", "LLL", "CBS", "EXPE", "CHD", "ETR", "INCY", "SNPS", "TSS", "AEE", "FANG", "OMC", "KEY", "NUE", "BEN", "FRC", "VMC", "NTAP", "CBRE", "LEN", "ABC", "CFG", "FCX", "DISH", "KEYS", "DHI", "DXC", "RMD", "ANSS", "ARE", "MXIM", "PFG", "CINF", "CMS", "GWW", "AJG", "CNP", "AAL", "DISCA", "NDAQ", "CPRT", "GRMN", "WAT", "CAH", "WYNN", "EFX", "DRI", "SWKS", "FTNT", "EVRG", "GPC", "CAG", "COO", "DISCK", "XYL", "SYMC", "HBAN", "HCP", "IFF", "HST", "SJM", "DOV", "WDC", "MGM", "MLM", "STX", "TFX", "EXR", "LNC", "DGX", "WCG", "SIVB", "DVN", "EXPD", "KMX", "CTXS", "XRAY", "TIF", "MRO", "TAP", "HAS", "VNO", "CTL", "AKAM", "MAA", "RJF", "NCLH", "UDR", "ETFC", "KSU", "HOLX", "TSCO", "VAR", "TXT", "ROL", "ATO", "WAB", "CMA", "NBL", "ABMD", "CPB", "APA", "CHRW", "TTWO", "VIAB", "AAP", "CBOE", "MAS", "KSS", "UHS", "MYL", "LNT", "REG", "JEC", "COG", "DRE", "AES", "URI", "EMN", "JBHT", "PKI", "JKHY", "PNW", "FMC", "FTI", "ARNC", "HSIC", "UAA", "MHK", "WRK", "FRT", "NRG", "TMK", "QRVO", "GPS", "NOV", "PKG", "SNA", "ALLE", "JNPR", "MOS", "PVH", "AVY", "TPR", "FFIV", "IPG", "IRM", "ZION", "LKQ", "DVA", "COTY", "WHR", "PHM", "NLSN", "IVZ", "AOS", "HII", "IPGP", "BWA", "ADS", "FBHS", "ALB", "KIM", "UNM", "ALK", "HFC", "XRX", "AIV", "SLG", "RHI", "FLIR", "NWS", "XEC", "PBCT", "SEE", "NWSA", "TRIP", "NWL", "PRGO", "FLS", "CPRI", "PNR", "HBI", "JWN", "AIZ", "JEF", "MAC", "HOG", "NKTR", "PWR", "HRB", "LEG", "AMG", "FLR", "MAT"
        };

        private static readonly string FolderPath = @"C:\Users\cody\Misc\SocialMediaRawData\";
        private static readonly string OutputFolder = @"C:\Users\cody\Misc\SocialMediaRawData\results";
        private static readonly string SymbolDataOutputFolder = @"C:\Users\cody\Misc\SocialMediaRawData\results\symbol_datasets";


        private static void Main()
        {

            if (!Directory.Exists(SymbolDataOutputFolder))
            {
                Directory.CreateDirectory(SymbolDataOutputFolder);
            }


            
            var pathMappingsBySymbol = CalculatePathFeatures();

            // Process in parallel
            Console.WriteLine($"Processing these stocks in parallel: {string.Join(", ", SymbolsToMap)}.");
            SymbolsToMap.AsParallel().Select(s => CreateUserDataset(pathMappingsBySymbol, s)).ToList();

        }




        private static bool CreateUserDataset(Dictionary<string, Dictionary<string, int>> pathMappingsBySymbol, string symbolToMap)
        {
            var jsonString = File.ReadAllText(UserStringGraphFile);

            var stringGraph = JsonConvert.DeserializeObject<HashSet<StringGraphMapping>>(jsonString);

            var symbolDatasets = new Dictionary<string, List<List<int>>>();

            foreach (var s in StockSymbols)
            {
                symbolDatasets.Add(s, new List<List<int>>());
            }

            Console.WriteLine("Creating users list...");
            var uniqueUsers = stringGraph.Select(e => e.ScreenName).Distinct().ToList();

            Console.WriteLine($"Mapping {symbolToMap} data for {uniqueUsers.Count} users...");

            var counter = 0;


            var stockSymbol = symbolToMap;


            // If we wanted to weight graph
            //var probabilityMappings = pathMappingsBySymbol[stockSymbol];

            //process stock

            foreach (var user in uniqueUsers)
            {
                if (counter % 1000 == 0) Console.WriteLine(stockSymbol + ": " + counter + " users.");
                var mappings = stringGraph.Where(e => e.ScreenName == user).ToList();


                var result = StockSymbols
                    .Where(s => s != stockSymbol)
                    .Select(s => mappings
                        .Any(a => a.Symbol == s) ? 1 : 0)
                    .ToList();

                result.Add(mappings.Any(s => s.Symbol == stockSymbol) ? 1 : 0);

                symbolDatasets[stockSymbol].Add(result);
                counter++;
            }



            var outputPath = Path.Combine(SymbolDataOutputFolder, stockSymbol + "_feature_dictionary_mapping.json");

            var outputString = JsonConvert.SerializeObject(symbolDatasets);

            File.WriteAllText(outputPath, outputString);


            Console.WriteLine("Generating stock datasets...");
            var outputFileName = stockSymbol + ".csv";
            var outPath = Path.Combine(SymbolDataOutputFolder, outputFileName);
            using (var outfile = new StreamWriter(outPath))
            {

                var header = string.Join(',', StockSymbols.Where(s => s != stockSymbol)) + "\n";
                outfile.Write(header);

                foreach (var row in symbolDatasets[stockSymbol])
                {
                    outfile.Write(string.Join(',', row) + "\n");
                }

            }

            return true;

        }

        private static Dictionary<string, Dictionary<string, int>> CalculatePathFeatures()
        {
            var outputPath = Path.Combine(OutputFolder, "path_mappings_by_symbol.json");

            var jsonString = File.ReadAllText(PathMappingFile);
            var pathMappings = JsonConvert.DeserializeObject<List<PathMapping>>(jsonString);

            

            var results = new Dictionary<string, Dictionary<string, int>>();

            Console.WriteLine("Creating stock symbol mappings...");
            foreach (var stockSymbol in StockSymbols)
            {
                var stockMappings = pathMappings.Where(m => m.Source == stockSymbol).ToList();

                var averagePaths = CalculateAverage(stockMappings);

                var pathFeatures = new Dictionary<string, int>();
                foreach (var stockMapping in stockMappings)
                {
                    if (stockMapping.NumPaths >= averagePaths)
                    {
                        pathFeatures.Add(stockMapping.Target, 1);
                    }
                    else
                    {
                        pathFeatures.Add(stockMapping.Target, 0);

                    }
                }

                results.Add(stockSymbol, pathFeatures);
            }

            var outputString = JsonConvert.SerializeObject(results);
            File.WriteAllText(outputPath, outputString);

            return results;
        }

        private static int CalculateAverage(List<PathMapping> pathMappings)
        {
            return pathMappings.Select(p => p.NumPaths).Sum() / pathMappings.Count;
        }

    }

    internal class PathMapping
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public int NumPaths { get; set; }
    }

    internal class PathFeature
    {
        public string Target { get; set; }
        public int PathProbability { get; set; }
    }

    internal class StringGraphMapping
    {
        public string ScreenName { get; set; }
        public string Symbol { get; set; }
    }
}
