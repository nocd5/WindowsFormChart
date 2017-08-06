using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsFormChart.example.Model
{
    class SomethingModel
    {
        private CancellationTokenSource cancelTokenSrc;

        public async Task RunSin(Action<IEnumerable<Point>> onDataUpdated)
        {
            cancelTokenSrc = new CancellationTokenSource();
            foreach (var v in GetSin())
            {
                if (cancelTokenSrc.Token.IsCancellationRequested)
                {
                    break;
                }
                onDataUpdated?.Invoke(new Point[] { await v });
            }
        }

        public IEnumerable<Task<Point>> GetSin()
        {
            double i = 0;
            while (true)
            {
                yield return Task.Run(async () =>
                {
                    await Task.Delay(50);
                    return new Point(i, Math.Sin(i));
                });
                i += Math.PI / 50;
            }
        }

        public async Task RunCos(Action<IEnumerable<Point>> onDataUpdated)
        {
            cancelTokenSrc = new CancellationTokenSource();
            foreach (var v in GetCos())
            {
                if (cancelTokenSrc.Token.IsCancellationRequested)
                {
                    break;
                }
                onDataUpdated?.Invoke(new Point[] { await v });
            }
        }

        public IEnumerable<Task<Point>> GetCos()
        {
            double i = 0;
            while (true)
            {
                yield return Task.Run(async () =>
                {
                    await Task.Delay(50);
                    return new Point(i, Math.Cos(i));
                });
                i += Math.PI / 50;
            }
        }

        public async Task RunRect(Action<IEnumerable<Point>> onDataUpdated)
        {
            cancelTokenSrc = new CancellationTokenSource();
            foreach (var v in GetRect())
            {
                if (cancelTokenSrc.Token.IsCancellationRequested)
                {
                    break;
                }
                onDataUpdated?.Invoke(new Point[] { await v });
            }
        }

        public IEnumerable<Task<Point>> GetRect()
        {
            double i = 0;
            while (true)
            {
                yield return Task.Run(async () =>
                {
                    await Task.Delay(50);
                    var y = Enumerable.Range(0, 1 + (int)(i / (2 * Math.PI)))
                        .Select(e => Math.Sin((double)(e * 2 + 1) * i) / (double)(e * 2 + 1))
                        .Sum();
                    return new Point(i, y);
                });
                i += Math.PI / 50;
            }
        }

        public async Task RunSinc(Action<IEnumerable<Point>> onDataUpdated)
        {
            cancelTokenSrc = new CancellationTokenSource();
            foreach (var v in GetSinc())
            {
                if (cancelTokenSrc.Token.IsCancellationRequested)
                {
                    break;
                }
                onDataUpdated?.Invoke(await v);
            }
        }

        public IEnumerable<Task<Point[]>> GetSinc()
        {
            double i = 0;
            while (true)
            {
                yield return Task.Run(async () =>
                {
                    await Task.Delay(50);
                    if (i == 0)
                    {
                        return new Point[] { new Point(0, 1) };
                    }
                    var y = Math.Sin(i) / i;
                    return new Point[] { new Point(i, y), new Point(-i, y) };
                });
                i += Math.PI / 50;
            }
        }

        public async Task Cancel()
        {
            await Task.Run(() => cancelTokenSrc?.Cancel());
        }
    }
}
