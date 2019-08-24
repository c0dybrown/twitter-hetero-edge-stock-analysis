import os

import json
import hetio.readwrite
import hetio.stats
from hetio.pathtools import paths_between, DWPC
from hetio.stats import plot_degrees

symbols = ['AMZN', 'AAPL', 'JNJ', 'JPM', 'XOM', 'WMT', 'BAC', 'DIS', 'CSCO', 'UNH', 'PFE', 'INTC', 'CVX', 'WFC', 'MRK', 'CMCSA', 'ORCL', 'PEP', 'NFLX', 'MCD', 'ABT', 'ADBE', 'NKE', 'PYPL', 'UNP', 'HON', 'IBM', 'CRM', 'AVGO', 'MDT', 'UTX', 'ABBV', 'LLY', 'ACN', 'TMO', 'NVDA', 'TXN', 'COST', 'AMGN', 'MMM', 'QCOM', 'LIN', 'AXP', 'SBUX', 'DHR', 'LMT', 'NEE', 'UPS', 'AMT', 'LOW', 'USB', 'GILD', 'CHTR', 'BKNG', 'BMY', 'CAT', 'MDLZ', 'CVS', 'BLK', 'DWDP', 'SYK', 'COP', 'ADP', 'ANTM', 'CELG', 'TJX', 'DUK', 'CSX', 'CME', 'INTU', 'BDX', 'SCHW', 'PNC', 'ISRG', 'SLB', 'EOG', 'NSC', 'SPG', 'SPGI', 'ECL', 'CCI', 'BSX', 'RTN', 'ITW', 'NOC', 'WBA', 'ZTS', 'MMC', 'ILMN', 'PLD', 'EXC', 'FDX', 'AGN', 'ICE', 'MAR', 'MET', 'OXY', 'APD', 'BIIB', 'KMI', 'AIG', 'KMB', 'VRTX', 'HCA', 'AON', 'COF', 'PGR', 'AEP', 'ADI', 'EMR', 'SHW', 'PRU', 'EQIX', 'STZ', 'AMAT', 'KHC', 'DOW', 'BAX', 'PSA', 'CCL', 'TGT', 'PSX', 'APC', 'BBT', 'ADSK', 'AFL', 'FIS', 'MPC', 'SYY', 'VFC', 'DAL', 'TRV', 'ROP', 'MCO', 'ATVI', 'REGN', 'ROST', 'JCI', 'SRE', 'VLO', 'ETN', 'MNST', 'FISV', 'CTSH', 'HUM', 'WMB', 'RHT', 'EBAY', 'ALL', 'YUM', 'LYB', 'TEL', 'WELL', 'GIS', 'PAYX', 'HPQ', 'XLNX', 'ALXN', 'TWTR', 'LRCX', 'AMD', 'PEG', 'ORLY', 'XEL', 'LUV', 'APH', 'TSN', 'STI', 'EQR', 'FTV', 'AVB', 'OKE', 'PPG', 'RCL', 'RSG', 'IQV', 'HLT', 'CMI', 'HSY', 'ALGN', 'DFS', 'PXD', 'TDG', 'DLTR', 'MCK', 'BF.B', 'AZO', 'ZBH', 'NEM', 'DLR', 'WEC', 'TROW', 'GLW', 'STT', 'PCAR', 'ADM', 'BHGE', 'CNC', 'SBAC', 'SYF', 'MSI', 'HAL', 'CTAS', 'FOXA', 'VRSK', 'VRSN', 'MTB', 'GPN', 'FOX', 'WLTW', 'FLT', 'DTE', 'INFO', 'UAL', 'CERN', 'PPL', 'SWK', 'MCHP', 'CXO', 'VTR',
           'HPE', 'NTRS', 'IDXX', 'HRS', 'HRL', 'BLL', 'APTV', 'FITB', 'ULTA', 'MKC', 'ANET', 'ROK', 'BXP', 'BBY', 'EIX', 'CMG', 'AME', 'CDNS', 'AMP', 'HES', 'HIG', 'FAST', 'MSCI', 'CLX', 'AWK', 'KLAC', 'MTD', 'ESS', 'LLL', 'CBS', 'EXPE', 'CHD', 'ETR', 'INCY', 'SNPS', 'TSS', 'AEE', 'FANG', 'OMC', 'KEY', 'NUE', 'BEN', 'FRC', 'VMC', 'NTAP', 'CBRE', 'LEN', 'ABC', 'CFG', 'FCX', 'DISH', 'KEYS', 'DHI', 'DXC', 'RMD', 'ANSS', 'ARE', 'MXIM', 'PFG', 'CINF', 'CMS', 'GWW', 'AJG', 'CNP', 'AAL', 'DISCA', 'NDAQ', 'CPRT', 'GRMN', 'WAT', 'CAH', 'WYNN', 'EFX', 'DRI', 'SWKS', 'FTNT', 'EVRG', 'GPC', 'CAG', 'COO', 'DISCK', 'XYL', 'SYMC', 'HBAN', 'HCP', 'IFF', 'HST', 'SJM', 'DOV', 'WDC', 'MGM', 'MLM', 'STX', 'TFX', 'EXR', 'LNC', 'DGX', 'WCG', 'SIVB', 'DVN', 'EXPD', 'KMX', 'CTXS', 'XRAY', 'TIF', 'MRO', 'TAP', 'HAS', 'VNO', 'CTL', 'AKAM', 'MAA', 'RJF', 'NCLH', 'UDR', 'ETFC', 'KSU', 'HOLX', 'TSCO', 'VAR', 'TXT', 'ROL', 'ATO', 'WAB', 'CMA', 'NBL', 'ABMD', 'CPB', 'APA', 'CHRW', 'TTWO', 'VIAB', 'AAP', 'CBOE', 'MAS', 'KSS', 'UHS', 'MYL', 'LNT', 'REG', 'JEC', 'COG', 'DRE', 'AES', 'URI', 'EMN', 'JBHT', 'PKI', 'JKHY', 'PNW', 'FMC', 'FTI', 'ARNC', 'HSIC', 'UAA', 'MHK', 'WRK', 'FRT', 'NRG', 'TMK', 'QRVO', 'GPS', 'NOV', 'PKG', 'SNA', 'ALLE', 'JNPR', 'MOS', 'PVH', 'AVY', 'TPR', 'FFIV', 'IPG', 'IRM', 'ZION', 'LKQ', 'DVA', 'COTY', 'WHR', 'PHM', 'NLSN', 'IVZ', 'AOS', 'HII', 'IPGP', 'BWA', 'ADS', 'FBHS', 'ALB', 'KIM', 'UNM', 'ALK', 'HFC', 'XRX', 'AIV', 'SLG', 'RHI', 'FLIR', 'NWS', 'XEC', 'PBCT', 'SEE', 'NWSA', 'TRIP', 'NWL', 'PRGO', 'FLS', 'CPRI', 'PNR', 'HBI', 'JWN', 'AIZ', 'JEF', 'MAC', 'HOG', 'NKTR', 'PWR', 'HRB', 'LEG', 'AMG', 'FLR', 'MAT']



# create graph
graph = hetio.readwrite.read_graph(
    "C:\\Users\\cody\\Misc\\SocialMediaRawData\\results\\stock_mappings_hetio_preprocessed.json")

metagraph = graph.metagraph

metapaths = metagraph.extract_metapaths('StockSymbol', 'StockSymbol', max_length=4)
abbreviations = [str(metapath) for metapath in metapaths]
print(str(abbreviations))

metapath = metagraph.metapath_from_abbrev('SiUiS')

# test out some path analysis
source_target_pairs = [
    {'source_id': ('StockSymbol', 'ORCL'), 'target_id': ('StockSymbol', 'AAPL')},
    {'source_id': ('StockSymbol', 'ORCL'), 'target_id': ('StockSymbol', 'AMZN')},
    {'source_id': ('StockSymbol', 'ORCL'), 'target_id': ('StockSymbol', 'CSCO')},
    {'source_id': ('StockSymbol', 'ORCL'), 'target_id': ('StockSymbol', 'NFLX')},
    {'source_id': ('StockSymbol', 'ORCL'), 'target_id': ('StockSymbol', 'PYPL')}
]

results = []
for pair in source_target_pairs:
    paths = paths_between(
        graph, pair['source_id'], pair['target_id'], metapath)
    results.append({'source': pair['source_id'][1],
                    'target': pair['target_id'][1], 'paths': paths})

for result in results:
    print(result['source'] + " --> " + result['target'])
    print("Paths: " + str(len(result['paths'])))
    dwpc = DWPC(result['paths'], damping_exponent=0.5)
    print("DWPC: " + str(dwpc))
    print("")


plot_degrees(
    graph, "C:\\Users\\cody\\Misc\\SocialMediaRawData\\results\\degree_graph.pdf")


# calculate the best predictor of a given symbol
results = []
for symbolToPredict in symbols:
    source_target_pairs = []
    for s in symbols:
        if s != symbolToPredict:
            source_target_pairs.append(
                {'source_id': ('StockSymbol', symbolToPredict), 'target_id': ('StockSymbol', s)})


    numPaths = 0
    bestPredictor = symbolToPredict
    for pair in source_target_pairs:
        paths = paths_between(
            graph, pair['source_id'], pair['target_id'], metapath)
        if len(paths) > numPaths:
            numPaths = len(paths)
            bestPredictor = pair['target_id'][1]

    results.append({'symbol': symbolToPredict,
                    'bestPredictor': bestPredictor, 'numPaths': numPaths})


with open('C:\\Users\\cody\\Misc\\SocialMediaRawData\\results\\best_predictors.json', 'w') as fp:
    json.dump(results, fp)




# Map all symbols to all other symbols
source_target_pairs = []
for symbol1 in symbols:
    for symbol2 in symbols:
        if symbol1 != symbol2:
            source_target_pairs.append(
                {'source_id': ('StockSymbol', symbol1), 'target_id': ('StockSymbol', symbol2)})


# calculate the number of paths between all stock symbols
results = []
for pair in source_target_pairs:
    paths = paths_between(graph, pair['source_id'], pair['target_id'], metapath)
    results.append({'source': pair['source_id'][1], 'target': pair['target_id'][1], 'numPaths': len(paths)})


with open('C:\\Users\\cody\\Misc\\SocialMediaRawData\\results\\num_paths_all_symbols.json', 'w') as fp:
    json.dump(results, fp)


