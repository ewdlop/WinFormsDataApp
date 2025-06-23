using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace WinFormsApp3
{
    public class 隨機數據生成器
    {
        private readonly Random _隨機數;

        public 隨機數據生成器(int 種子 = 12345)
        {
            _隨機數 = new MersenneTwister(種子);
        }

        // 使用幾何布朗運動 (Geometric Brownian Motion) 生成股價數據
        public List<金融數據> 生成幾何布朗運動(
            double 初始價格, 
            double 漂移率, 
            double 波動率, 
            int 天數,
            DateTime 開始日期)
        {
            var 數據列表 = new List<金融數據>();
            var 常態分布 = new Normal(0, 1, _隨機數);
            double 當前價格 = 初始價格;
            double 時間間隔 = 1.0 / 252.0; // 假設每年252個交易日

            for (int i = 0; i < 天數; i++)
            {
                double 隨機值 = 常態分布.Sample();
                double 價格變化 = 漂移率 * 時間間隔 + 波動率 * Math.Sqrt(時間間隔) * 隨機值;
                double 新價格 = 當前價格 * Math.Exp(價格變化);

                // 生成開高低收數據
                double 日內波動率 = 波動率 * 0.1; // 日內波動性
                double 開盤 = 當前價格;
                double 收盤 = 新價格;
                
                // 簡單模擬高低價
                double 最高 = Math.Max(開盤, 收盤) * (1 + Math.Abs(常態分布.Sample()) * 日內波動率);
                double 最低 = Math.Min(開盤, 收盤) * (1 - Math.Abs(常態分布.Sample()) * 日內波動率);

                // 模擬交易量
                double 交易量 = Math.Abs(常態分布.Sample() * 1000000 + 5000000);

                數據列表.Add(new 金融數據
                {
                    日期 = 開始日期.AddDays(i),
                    開盤價 = 開盤,
                    最高價 = 最高,
                    最低價 = 最低,
                    收盤價 = 收盤,
                    成交量 = 交易量,
                    報酬率 = i > 0 ? (收盤 - 當前價格) / 當前價格 : 0
                });

                當前價格 = 新價格;
            }

            return 數據列表;
        }

        // 生成帶有跳躍的Merton跳躍擴散模型
        public List<金融數據> 生成默頓跳躍擴散(
            double 初始價格,
            double 漂移率,
            double 波動率,
            double 跳躍強度,
            double 跳躍均值,
            double 跳躍波動率,
            int 天數,
            DateTime 開始日期)
        {
            var 數據列表 = new List<金融數據>();
            var 常態分布 = new Normal(0, 1, _隨機數);
            var 泊松分布 = new Poisson(跳躍強度 / 252.0); // 每日跳躍機率
            double 當前價格 = 初始價格;
            double 時間間隔 = 1.0 / 252.0;

            for (int i = 0; i < 天數; i++)
            {
                // 布朗運動部分
                double 隨機值 = 常態分布.Sample();
                double 擴散項 = 漂移率 * 時間間隔 + 波動率 * Math.Sqrt(時間間隔) * 隨機值;

                // 跳躍部分
                int 跳躍次數 = 泊松分布.Sample();
                double 跳躍項 = 0;
                for (int j = 0; j < 跳躍次數; j++)
                {
                    跳躍項 += Normal.Sample(_隨機數, 跳躍均值, 跳躍波動率);
                }

                double 總變化 = 擴散項 + 跳躍項;
                double 新價格 = 當前價格 * Math.Exp(總變化);

                // 生成OHLC數據（簡化版本）
                double 日內波動率 = 波動率 * 0.1;
                double 開盤 = 當前價格;
                double 收盤 = 新價格;
                double 最高 = Math.Max(開盤, 收盤) * (1 + Math.Abs(常態分布.Sample()) * 日內波動率);
                double 最低 = Math.Min(開盤, 收盤) * (1 - Math.Abs(常態分布.Sample()) * 日內波動率);
                double 交易量 = Math.Abs(常態分布.Sample() * 1000000 + 5000000);

                數據列表.Add(new 金融數據
                {
                    日期 = 開始日期.AddDays(i),
                    開盤價 = 開盤,
                    最高價 = 最高,
                    最低價 = 最低,
                    收盤價 = 收盤,
                    成交量 = 交易量,
                    報酬率 = i > 0 ? (收盤 - 當前價格) / 當前價格 : 0
                });

                當前價格 = 新價格;
            }

            return 數據列表;
        }

        // 添加白雜訊到現有數據
        public void 添加白雜訊(List<金融數據> 數據, double 雜訊水平)
        {
            var 常態分布 = new Normal(0, 雜訊水平, _隨機數);
            
            foreach (var 項目 in 數據)
            {
                double 雜訊 = 常態分布.Sample();
                項目.收盤價 *= (1 + 雜訊);
                項目.開盤價 *= (1 + 雜訊 * 0.5);
                項目.最高價 *= (1 + Math.Abs(雜訊) * 0.3);
                項目.最低價 *= (1 - Math.Abs(雜訊) * 0.3);
            }
        }
    }
} 