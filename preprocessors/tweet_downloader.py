from tweepy import Stream
from tweepy import OAuthHandler
from tweepy.streaming import StreamListener
import json
import time
import tweepy

def get_tweets(api, input_query):
    for tweet in tweepy.Cursor(api.search, wait_on_rate_limit=True, wait_on_rate_limit_notify=True, q=input_query, lang='en').items():
        yield tweet

access_token = ''
access_token_secret = ''
consumer_key = ''
consumer_secret = ''
auth = tweepy.OAuthHandler(consumer_key, consumer_secret)
auth.set_access_token(access_token, access_token_secret)
api = tweepy.API(auth, wait_on_rate_limit=True, wait_on_rate_limit_notify=True)

input_queries = [ 'MSFT', 'AMZN', 'AAPL', 'JNJ', 'JPM', 'XOM', 'WMT', 'BAC', 'DIS', 'CSCO', 'UNH', 'PFE', 'INTC', 'CVX', 'WFC', 'MRK', 'CMCSA', 'ORCL', 'PEP', 'NFLX', 'MCD', 'ABT', 'ADBE', 'NKE', 'PYPL', 'UNP', 'HON', 'IBM', 'CRM', 'AVGO', 'MDT', 'UTX', 'ABBV', 'LLY', 'ACN', 'TMO', 'NVDA', 'TXN', 'COST', 'AMGN', 'MMM', 'QCOM', 'LIN', 'AXP', 'SBUX', 'DHR', 'LMT', 'NEE', 'UPS', 'AMT', 'LOW', 'USB', 'GILD', 'CHTR', 'BKNG', 'BMY', 'CAT', 'MDLZ', 'CVS', 'BLK', 'DWDP', 'SYK', 'COP', 'ADP', 'ANTM', 'CELG', 'TJX', 'DUK', 'CSX', 'CME', 'INTU', 'BDX', 'SCHW', 'PNC', 'ISRG', 'SLB', 'EOG', 'NSC', 'SPG', 'SPGI', 'ECL', 'CCI', 'BSX', 'RTN', 'ITW', 'NOC', 'WBA', 'ZTS', 'MMC', 'ILMN', 'PLD', 'EXC', 'FDX', 'AGN', 'ICE', 'MAR', 'MET', 'OXY', 'APD', 'BIIB', 'KMI', 'AIG', 'KMB', 'VRTX', 'HCA', 'AON', 'COF', 'PGR', 'AEP', 'ADI', 'EMR', 'SHW', 'PRU', 'EQIX', 'STZ', 'AMAT', 'KHC', 'DOW', 'BAX', 'PSA', 'CCL', 'TGT', 'PSX', 'APC', 'BBT', 'ADSK', 'AFL', 'FIS', 'MPC', 'SYY', 'VFC', 'DAL', 'TRV', 'ROP', 'MCO', 'ATVI', 'REGN', 'ROST', 'JCI', 'SRE', 'VLO', 'ETN', 'MNST', 'FISV', 'CTSH', 'HUM', 'WMB', 'RHT', 'EBAY', 'ALL', 'YUM', 'LYB', 'TEL', 'WELL', 'GIS', 'PAYX', 'HPQ', 'XLNX', 'ALXN', 'TWTR', 'LRCX', 'AMD', 'PEG', 'ORLY', 'XEL', 'LUV', 'APH', 'TSN', 'STI', 'EQR', 'FTV', 'AVB', 'OKE', 'PPG', 'RCL', 'RSG', 'IQV', 'HLT', 'CMI', 'HSY', 'ALGN', 'DFS', 'PXD', 'TDG', 'DLTR', 'MCK', 'BF.B', 'AZO', 'ZBH', 'NEM', 'DLR', 'WEC', 'TROW', 'GLW', 'STT', 'PCAR', 'ADM', 'BHGE', 'CNC', 'SBAC', 'SYF', 'MSI', 'HAL', 'CTAS', 'FOXA', 'VRSK', 'VRSN', 'MTB', 'GPN', 'FOX', 'WLTW', 'FLT', 'DTE', 'INFO', 'UAL', 'CERN', 'PPL', 'SWK', 'MCHP', 'CXO', 'VTR', 'HPE', 'NTRS', 'IDXX', 'HRS', 'HRL', 'BLL', 'APTV', 'FITB', 'ULTA', 'MKC', 'ANET', 'ROK', 'BXP', 'BBY', 'EIX', 'CMG', 'AME', 'CDNS', 'AMP', 'HES', 'HIG', 'FAST', 'MSCI', 'CLX', 'AWK', 'KLAC', 'MTD', 'ESS', 'LLL', 'CBS', 'EXPE', 'CHD', 'ETR', 'INCY', 'SNPS', 'TSS', 'AEE', 'FANG', 'OMC', 'KEY', 'NUE', 'BEN', 'FRC', 'VMC', 'NTAP', 'CBRE', 'LEN', 'ABC', 'CFG', 'FCX', 'DISH', 'KEYS', 'DHI', 'DXC', 'RMD', 'ANSS', 'ARE', 'MXIM', 'PFG', 'CINF', 'CMS', 'GWW', 'AJG', 'CNP', 'AAL', 'DISCA', 'NDAQ', 'CPRT', 'GRMN', 'WAT', 'CAH', 'WYNN', 'EFX', 'DRI', 'SWKS', 'FTNT', 'EVRG', 'GPC', 'CAG', 'COO', 'DISCK', 'XYL', 'SYMC', 'HBAN', 'HCP', 'IFF', 'HST', 'SJM', 'DOV', 'WDC', 'MGM', 'MLM', 'STX', 'TFX', 'EXR', 'LNC', 'DGX', 'WCG', 'SIVB', 'DVN', 'EXPD', 'KMX', 'CTXS', 'XRAY', 'TIF', 'MRO', 'TAP', 'HAS', 'VNO', 'CTL', 'AKAM', 'MAA', 'RJF', 'NCLH', 'UDR', 'ETFC', 'KSU', 'HOLX', 'TSCO', 'VAR', 'TXT', 'ROL', 'ATO', 'WAB', 'CMA', 'NBL', 'ABMD', 'CPB', 'APA', 'CHRW', 'TTWO', 'VIAB', 'AAP', 'CBOE', 'MAS', 'KSS', 'UHS', 'MYL', 'LNT', 'REG', 'JEC', 'COG', 'DRE', 'AES', 'URI', 'EMN', 'JBHT', 'PKI', 'JKHY', 'PNW', 'FMC', 'FTI', 'ARNC', 'HSIC', 'UAA', 'MHK', 'WRK', 'FRT', 'NRG', 'TMK', 'QRVO', 'GPS', 'NOV', 'PKG', 'SNA', 'ALLE', 'JNPR', 'MOS', 'PVH', 'AVY', 'TPR', 'FFIV', 'IPG', 'IRM', 'ZION', 'LKQ', 'DVA', 'COTY', 'WHR', 'PHM', 'NLSN', 'IVZ', 'AOS', 'HII', 'IPGP', 'BWA', 'ADS', 'FBHS', 'ALB', 'KIM', 'UNM', 'ALK', 'HFC', 'XRX', 'AIV', 'SLG', 'RHI', 'FLIR', 'NWS', 'XEC', 'PBCT', 'SEE', 'NWSA', 'TRIP', 'NWL', 'PRGO', 'FLS', 'CPRI', 'PNR', 'HBI', 'JWN', 'AIZ', 'JEF', 'MAC', 'HOG', 'NKTR', 'PWR', 'HRB', 'LEG', 'AMG', 'FLR', 'MAT' ]

download_tweet_count = 10000
for input_query in input_queries:
    print(input_query)
    counter = 0
    tweets = get_tweets(api, input_query)
    while counter < download_tweet_count:
        try:
            tweet = tweets.next()
            file_name = input_query + '.json'
            f = open(file_name, 'a')
            f.write(str(tweet))
            f.close()
            counter = counter + 1
        except tweepy.RateLimitError:
            print('sleeping for 15...')
            time.sleep(15 * 60)
        except Exception:
            print('another type of error')