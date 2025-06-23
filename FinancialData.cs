using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp3
{
    public class FinancialData
    {
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double Returns { get; set; }
    }

    public class StochasticDataGenerator
    {
        private readonly Random _random;

        public StochasticDataGenerator(int seed = 12345)
        {
            _random = new MersenneTwister(seed);
        }

        // 使用幾何布朗運動 (Geometric Brownian Motion) 生成股價數據
        public List<FinancialData> GenerateGeometricBrownianMotion(
            double initialPrice, 
            double drift, 
            double volatility, 
            int days,
            DateTime startDate)
        {
            var data = new List<FinancialData>();
            var normal = new Normal(0, 1, _random);
            double currentPrice = initialPrice;
            double dt = 1.0 / 252.0; // 假設每年252個交易日

            for (int i = 0; i < days; i++)
            {
                double z = normal.Sample();
                double change = drift * dt + volatility * Math.Sqrt(dt) * z;
                double newPrice = currentPrice * Math.Exp(change);

                // 生成開高低收數據
                double dailyVolatility = volatility * 0.1; // 日內波動性
                double open = currentPrice;
                double close = newPrice;
                
                // 簡單模擬高低價
                double high = Math.Max(open, close) * (1 + Math.Abs(normal.Sample()) * dailyVolatility);
                double low = Math.Min(open, close) * (1 - Math.Abs(normal.Sample()) * dailyVolatility);

                // 模擬交易量
                double volume = Math.Abs(normal.Sample() * 1000000 + 5000000);

                data.Add(new FinancialData
                {
                    Date = startDate.AddDays(i),
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Volume = volume,
                    Returns = i > 0 ? (close - currentPrice) / currentPrice : 0
                });

                currentPrice = newPrice;
            }

            return data;
        }

        // 生成帶有跳躍的Merton跳躍擴散模型
        public List<FinancialData> GenerateMertonJumpDiffusion(
            double initialPrice,
            double drift,
            double volatility,
            double jumpIntensity,
            double jumpMean,
            double jumpVolatility,
            int days,
            DateTime startDate)
        {
            var data = new List<FinancialData>();
            var normal = new Normal(0, 1, _random);
            var poisson = new Poisson(jumpIntensity / 252.0); // 每日跳躍機率
            double currentPrice = initialPrice;
            double dt = 1.0 / 252.0;

            for (int i = 0; i < days; i++)
            {
                // 布朗運動部分
                double z = normal.Sample();
                double diffusion = drift * dt + volatility * Math.Sqrt(dt) * z;

                // 跳躍部分
                int jumps = poisson.Sample();
                double jumpComponent = 0;
                for (int j = 0; j < jumps; j++)
                {
                    jumpComponent += Normal.Sample(_random, jumpMean, jumpVolatility);
                }

                double totalChange = diffusion + jumpComponent;
                double newPrice = currentPrice * Math.Exp(totalChange);

                // 生成OHLC數據（簡化版本）
                double dailyVolatility = volatility * 0.1;
                double open = currentPrice;
                double close = newPrice;
                double high = Math.Max(open, close) * (1 + Math.Abs(normal.Sample()) * dailyVolatility);
                double low = Math.Min(open, close) * (1 - Math.Abs(normal.Sample()) * dailyVolatility);
                double volume = Math.Abs(normal.Sample() * 1000000 + 5000000);

                data.Add(new FinancialData
                {
                    Date = startDate.AddDays(i),
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Volume = volume,
                    Returns = i > 0 ? (close - currentPrice) / currentPrice : 0
                });

                currentPrice = newPrice;
            }

            return data;
        }

        // 添加白雜訊到現有數據
        public void AddWhiteNoise(List<FinancialData> data, double noiseLevel)
        {
            var normal = new Normal(0, noiseLevel, _random);
            
            foreach (var item in data)
            {
                double noise = normal.Sample();
                item.Close *= (1 + noise);
                item.Open *= (1 + noise * 0.5);
                item.High *= (1 + Math.Abs(noise) * 0.3);
                item.Low *= (1 - Math.Abs(noise) * 0.3);
            }
        }
    }

    public static class FinancialAnalyzer
    {
        // 計算移動平均
        public static List<double> CalculateMovingAverage(List<FinancialData> data, int period)
        {
            var movingAverages = new List<double>();
            
            for (int i = 0; i < data.Count; i++)
            {
                if (i < period - 1)
                {
                    movingAverages.Add(double.NaN);
                }
                else
                {
                    double sum = 0;
                    for (int j = i - period + 1; j <= i; j++)
                    {
                        sum += data[j].Close;
                    }
                    movingAverages.Add(sum / period);
                }
            }
            
            return movingAverages;
        }

        // 計算波動率
        public static double CalculateVolatility(List<FinancialData> data)
        {
            var returns = data.Select(d => d.Returns).Where(r => !double.IsNaN(r) && r != 0).ToList();
            if (returns.Count < 2) return 0;

            double mean = returns.Average();
            double sumSquaredDeviations = returns.Sum(r => Math.Pow(r - mean, 2));
            return Math.Sqrt(sumSquaredDeviations / (returns.Count - 1)) * Math.Sqrt(252); // 年化波動率
        }

        // 計算夏普比率
        public static double CalculateSharpeRatio(List<FinancialData> data, double riskFreeRate = 0.02)
        {
            var returns = data.Select(d => d.Returns).Where(r => !double.IsNaN(r) && r != 0).ToList();
            if (returns.Count < 2) return 0;

            double meanReturn = returns.Average() * 252; // 年化收益率
            double volatility = CalculateVolatility(data);
            
            return volatility != 0 ? (meanReturn - riskFreeRate) / volatility : 0;
        }
    }
} 