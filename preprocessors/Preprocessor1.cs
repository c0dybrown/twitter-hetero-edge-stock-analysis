using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;


namespace ConsoleApp5
{

    class Program
    {
        static List<string> StockSymbols = new List<string>
        {
            "MSFT", "AMZN", "AAPL", "GOOGL", "GOOG", "JNJ", "JPM", "XOM", "WMT", "BAC", "DIS", "CSCO", "UNH", "PFE", "INTC", "CVX", "WFC", "MRK", "CMCSA", "ORCL", "PEP", "NFLX", "MCD", "ABT", "ADBE", "NKE", "PYPL", "UNP", "HON", "IBM", "CRM", "AVGO", "MDT", "UTX", "ABBV", "LLY", "ACN", "TMO", "NVDA", "TXN", "COST", "AMGN", "MMM", "QCOM", "LIN", "AXP", "SBUX", "DHR", "LMT", "NEE", "UPS", "AMT", "LOW", "USB", "GILD", "CHTR", "BKNG", "BMY", "CAT", "MDLZ", "CVS", "BLK", "DWDP", "SYK", "COP", "ADP", "ANTM", "CELG", "TJX", "DUK", "CSX", "CME", "INTU", "BDX", "SCHW", "PNC", "ISRG", "SLB", "EOG", "NSC", "SPG", "SPGI", "ECL", "CCI", "BSX", "RTN", "ITW", "NOC", "WBA", "ZTS", "MMC", "ILMN", "PLD", "EXC", "FDX", "AGN", "ICE", "MAR", "MET", "OXY", "APD", "BIIB", "KMI", "AIG", "KMB", "VRTX", "HCA", "AON", "COF", "PGR", "AEP", "ADI", "EMR", "SHW", "PRU", "EQIX", "STZ", "AMAT", "KHC", "DOW", "BAX", "PSA", "CCL", "TGT", "PSX", "APC", "BBT", "ADSK", "AFL", "FIS", "MPC", "SYY", "VFC", "DAL", "TRV", "ROP", "MCO", "ATVI", "REGN", "ROST", "JCI", "SRE", "VLO", "ETN", "MNST", "FISV", "CTSH", "HUM", "WMB", "RHT", "EBAY", "ALL", "YUM", "LYB", "TEL", "WELL", "GIS", "PAYX", "HPQ", "XLNX", "ALXN", "TWTR", "LRCX", "AMD", "PEG", "ORLY", "XEL", "LUV", "APH", "TSN", "STI", "EQR", "FTV", "AVB", "OKE", "PPG", "RCL", "RSG", "IQV", "HLT", "CMI", "HSY", "ALGN", "DFS", "PXD", "TDG", "DLTR", "MCK", "BF.B", "AZO", "ZBH", "NEM", "DLR", "WEC", "TROW", "GLW", "STT", "PCAR", "ADM", "BHGE", "CNC", "SBAC", "SYF", "MSI", "HAL", "CTAS", "FOXA", "VRSK", "VRSN", "MTB", "GPN", "FOX", "WLTW", "FLT", "DTE", "INFO", "UAL", "CERN", "PPL", "SWK", "MCHP", "CXO", "VTR", "HPE", "NTRS", "IDXX", "HRS", "HRL", "BLL", "APTV", "FITB", "ULTA", "MKC", "ANET", "ROK", "BXP", "BBY", "EIX", "CMG", "AME", "CDNS", "AMP", "HES", "HIG", "FAST", "MSCI", "CLX", "AWK", "KLAC", "MTD", "ESS", "LLL", "CBS", "EXPE", "CHD", "ETR", "INCY", "SNPS", "TSS", "AEE", "FANG", "OMC", "KEY", "NUE", "BEN", "FRC", "VMC", "NTAP", "CBRE", "LEN", "ABC", "CFG", "FCX", "DISH", "KEYS", "DHI", "DXC", "RMD", "ANSS", "ARE", "MXIM", "PFG", "CINF", "CMS", "GWW", "AJG", "CNP", "AAL", "DISCA", "NDAQ", "CPRT", "GRMN", "WAT", "CAH", "WYNN", "EFX", "DRI", "SWKS", "FTNT", "EVRG", "GPC", "CAG", "COO", "DISCK", "XYL", "SYMC", "HBAN", "HCP", "IFF", "HST", "SJM", "DOV", "WDC", "MGM", "MLM", "STX", "TFX", "EXR", "LNC", "DGX", "WCG", "SIVB", "DVN", "EXPD", "KMX", "CTXS", "XRAY", "TIF", "MRO", "TAP", "HAS", "VNO", "CTL", "AKAM", "MAA", "RJF", "NCLH", "UDR", "ETFC", "KSU", "HOLX", "TSCO", "VAR", "TXT", "ROL", "ATO", "WAB", "CMA", "NBL", "ABMD", "CPB", "APA", "CHRW", "TTWO", "VIAB", "AAP", "CBOE", "MAS", "KSS", "UHS", "MYL", "LNT", "REG", "JEC", "COG", "DRE", "AES", "URI", "EMN", "JBHT", "PKI", "JKHY", "PNW", "FMC", "FTI", "ARNC", "HSIC", "UAA", "MHK", "WRK", "FRT", "NRG", "TMK", "QRVO", "GPS", "NOV", "PKG", "SNA", "ALLE", "JNPR", "MOS", "PVH", "AVY", "TPR", "FFIV", "IPG", "IRM", "ZION", "LKQ", "DVA", "COTY", "WHR", "PHM", "NLSN", "IVZ", "AOS", "HII", "IPGP", "BWA", "ADS", "FBHS", "ALB", "KIM", "UNM", "ALK", "HFC", "XRX", "AIV", "SLG", "RHI", "FLIR", "NWS", "XEC", "PBCT", "SEE", "NWSA", "TRIP", "NWL", "PRGO", "FLS", "CPRI", "PNR", "HBI", "JWN", "AIZ", "JEF", "MAC", "HOG", "NKTR", "PWR", "HRB", "LEG", "AMG", "FLR", "MAT"
        };

        private static string FolderPath = @"C:\Users\cody\Misc\SocialMediaRawData\";
        private static string OutputFolder = @"C:\Users\cody\Misc\SocialMediaRawData\results1\";

        static void Main(string[] args)
        {
            if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);

            if (!Directory.Exists(OutputFolder)) Directory.CreateDirectory(OutputFolder);

            var textFiles = Directory.GetFiles(FolderPath, "*.txt");

            var jsonFiles = new List<string>();

            foreach (var textFile in textFiles)
            {
                jsonFiles.Add(ParseRawTextToJson(textFile));
            }

            Console.WriteLine("Process JSON in parallel...");
            var stringGraphPath = CreateStringGraph(jsonFiles);

            var hetioGraph = ConvertStringGraphToHetio(stringGraphPath);

            Console.WriteLine("Resulting Hetio Graph: " + hetioGraph);
        }

        /// <summary>
        /// Converts a raw text file containing Tweepy-represented tweets to a mapped JSON file
        /// </summary>
        /// <param name="rawTextPath"></param>
        /// <returns></returns>
        private static string ParseRawTextToJson(string rawTextPath)
        {

            if (!File.Exists(rawTextPath)) throw new FileNotFoundException($"File {rawTextPath} does not exist");

            var oldFileName = Path.GetFileNameWithoutExtension(rawTextPath);

            var newFileName = oldFileName + "_json_mapped.json";

            var outputPath = Path.Combine(OutputFolder, newFileName);

            var userNameRegex = new Regex(@"screen_name': '(([a-zA-Z0-9_-])+)'", RegexOptions.Compiled);
            var tweetTextRegex = new Regex(@"text: '(.+?)',", RegexOptions.Compiled);
            var symbolsRegex = new Regex(@"text': '(([a-zA-Z0-9_-])+)'", RegexOptions.Compiled);
            var dateStringRegex = new Regex(@"created_at:datetime.datetime\((.*?)\)", RegexOptions.Compiled);

            var splitText = File.ReadAllText(rawTextPath).Split("Status(").ToList();

            var results = new List<StringDataMapping>();

            Console.WriteLine($"Creating JSON mappings from {oldFileName}...");
            for (var i = 0; i < splitText.Count; i++)
            {
                // Reformat string
                splitText[i] = "{" + splitText[i].Replace("'u", "'").TrimEnd(')').Replace("=", ":");
                splitText[i] = splitText[i].Replace("u'", " '") + "},";
                splitText[i] = splitText[i].Replace("  ", " ");
                splitText[i] = splitText[i].Replace(":None", ": 'None'");
                splitText[i] = splitText[i].Replace(": False", ": 'False'");
                splitText[i] = splitText[i].Replace(":False", ": 'False'");
                splitText[i] = splitText[i].Replace(":True", ": 'True'");
                splitText[i] = splitText[i].Replace(": True", ": 'True'");

                // Regex
                var screenName = userNameRegex.Match(splitText[i]).Groups[1]?.Value;
                var tweetText = tweetTextRegex.Match(splitText[i]).Groups[1]?.Value;
                var symbols = string.Join(',',symbolsRegex.Matches(splitText[i]).Select(m => m.Groups[1].Value));
                var dateString = dateStringRegex.Match(splitText[i]).Groups[1]?.Value;

                var date = TryParseDate(dateString);

                if (
                    !string.IsNullOrWhiteSpace(screenName) &&
                    !(string.IsNullOrWhiteSpace(symbols) && string.IsNullOrWhiteSpace(tweetText)))
                {
                    results.Add(new StringDataMapping
                    {
                        Date = date,
                        ScreenName = screenName,
                        Symbols = symbols,
                        TweetText = tweetText
                    });
                }

            }

            var outputString = JsonConvert.SerializeObject(results, Formatting.Indented);

            File.WriteAllText(outputPath, outputString);

            return outputPath;
        }


        /// <summary>
        /// Creates a node to node mapping between users and stock symbols based on a series of JSON-represented tweets.
        /// Processes each file in parallel.
        /// </summary>
        /// <param name="jsonMappingPaths"></param>
        /// <returns></returns>
        private static string CreateStringGraph(List<string> jsonMappingPaths)
        {

            foreach (var filePath in jsonMappingPaths)
            {
                if (!File.Exists(filePath)) throw new FileNotFoundException($"File {filePath} does not exist");
            }

            var outputPath = Path.Combine(OutputFolder, "string_graph.json");

            var graphMappings = jsonMappingPaths.AsParallel().SelectMany(MapSingleJsonFileToGraph).ToList();

            var outputString = JsonConvert.SerializeObject(graphMappings, Formatting.Indented);

            File.WriteAllText(outputPath, outputString);

            return outputPath;
        }


        /// <summary>
        /// Maps a single json file to a list of StringGraphMappings. Used as the thread process for CreateStringGraph()
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        private static List<StringGraphMapping> MapSingleJsonFileToGraph(string jsonFile)
        {
            var dataMappings = new List<StringDataMapping>();

            var jsonString = File.ReadAllText(jsonFile);
            var sm = JsonConvert.DeserializeObject<List<StringDataMapping>>(jsonString);

            dataMappings.AddRange(sm);

            Console.WriteLine("Creating graph mappings for " + Path.GetFileNameWithoutExtension(jsonFile) + "...");
            var graphMappings = new List<StringGraphMapping>();
            foreach (var stringDataMapping in dataMappings)
            {
                foreach (var stockSymbol in StockSymbols)
                {
                    var symbolRegex = new Regex(stockSymbol, RegexOptions.IgnoreCase);
                    if (symbolRegex.IsMatch(stringDataMapping.Symbols) ||
                        symbolRegex.IsMatch(stringDataMapping.TweetText))
                    {
                        graphMappings.Add(new StringGraphMapping
                        {
                            ScreenName = stringDataMapping.ScreenName,
                            Symbol = stockSymbol
                        });
                    }
                }
            }

            return graphMappings;
        }

        /// <summary>
        /// Converts the string graph to Hetio format to include metagraphs and node/edge lists.
        /// </summary>
        /// <param name="stringGraphPath"></param>
        /// <returns></returns>
        private static string ConvertStringGraphToHetio(string stringGraphPath)
        {

            if (!File.Exists(stringGraphPath)) throw new FileNotFoundException($"File {stringGraphPath} does not exist");

            var outputPath = Path.Combine(OutputFolder, "stock_mappings_hetio_preprocessed.json");

            var dataString = File.ReadAllText(stringGraphPath);

            var stringMappings = JsonConvert.DeserializeObject<List<StringGraphMapping>>(dataString);

            var hetioMappings = new HetIOMappings
            {
                metanode_kinds = new List<string> { "User", "StockSymbol" },
                metaedge_tuples = new List<List<string>>
                {
                    new List<string> {"User", "StockSymbol", "interaction", "both"}
                },
                kind_to_abbrev = new Abbriev { User = "U", StockSymbol = "S" },
                nodes = new HashSet<Node>(),
                edges = new HashSet<Edge>()
            };

            Console.WriteLine("Creating stock symbol node metagraph...");
            foreach (var stockSymbol in StockSymbols)
            {
                hetioMappings.nodes.Add(new Node
                {
                    kind = "StockSymbol",
                    identifier = stockSymbol,
                    name = stockSymbol,
                    data = new { }
                });
            }

            Console.WriteLine("Creating user node metagraph and adding edges...");
            foreach (var stringMapping in stringMappings)
            {
                if (hetioMappings.nodes.All(a => a.name != stringMapping.ScreenName))
                {
                    hetioMappings.nodes.Add(new Node
                    {
                        name = stringMapping.ScreenName,
                        identifier = stringMapping.ScreenName,
                        kind = "User",
                        data = new { }
                    });
                }

                var edge = new Edge
                {
                    source_id = new[] { "User", stringMapping.ScreenName },
                    target_id = new[] { "StockSymbol", stringMapping.Symbol },
                    kind = "interaction",
                    direction = "both",
                    data = new { }
                };

            }

            var outputString = JsonConvert.SerializeObject(hetioMappings, Formatting.Indented);
            File.WriteAllText(outputPath, outputString);

            return outputPath;
        }


        private static DateTime TryParseDate(string dateString)
        {
            try
            {
                var intArr = dateString.Split(", ").Select(int.Parse).ToList();

                var date = new DateTime(intArr[0], intArr[1], intArr[2], intArr[3], intArr[4], intArr[5]);

                return date;
            }
            catch
            {
                return DateTime.MinValue;
            }

        }
    }


    class StringDataMapping
    {
        public string ScreenName { get; set; }
        public string TweetText { get; set; }
        public string Symbols { get; set; }
        public DateTime Date { get; set; }

    }

    class StringGraphMapping
    {
        public string ScreenName { get; set; }
        public string Symbol { get; set; }
    }

    class Node
    {
        public string kind { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
        public object data { get; set; }
    }

    class Edge
    {
        public string[] source_id { get; set; }
        public string[] target_id { get; set; }
        public string kind { get; set; }
        public string direction { get; set; }
        public object data { get; set; }
    }

    class Abbriev
    {
        public string User { get; set; }
        public string StockSymbol { get; set; }
    }


    class HetIOMappings
    {
        public List<string> metanode_kinds { get; set; }
        public List<List<string>> metaedge_tuples { get; set; }
        public Abbriev kind_to_abbrev { get; set; }
        public HashSet<Node> nodes { get; set; }
        public HashSet<Edge> edges { get; set; }
    }
}
