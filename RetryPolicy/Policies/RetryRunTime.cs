using System;
using System.Threading;
using System.Threading.Tasks;
using RetryPolicies.Exceptions;
using RetryPolicies.Interfaces;

namespace RetryPolicies.Policies
{
    public class RetryRunTime<T> where T : IRetryPolicy, new()
    {
        private readonly AutoResetEvent _bypass = new AutoResetEvent(false);
        private readonly TimeSpan _interval;
        private readonly Func<IRetryPolicy> _policy;

        public RetryRunTime(Func<IRetryPolicy> policy, TimeSpan interval)
        {
            _policy = policy;
            _interval = interval;
        }

        //public Retry(IRetryPolicy policy, TimeSpan interval)
        //{
        //    _policy = policy;
        //    _interval = interval;
        //}

        public RetryRunTime(TimeSpan interval)
        {
            _policy = () => new T();
            _interval = interval;
        }

        //public void Exec(Action action)
        //{
        //    while (_tryAgain)
        //    {
        //        try
        //        {
        //            action.Invoke();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(@"Too bad !");

        //            if (!_policy.CanRetry(ex, _count++ > _maxRetries || !_exceptionTypes.Contains(ex.GetType())))
        //            {
        //                _tryAgain = false;
        //                throw;
        //            }
        //            _autoEvent.WaitOne(_interval);
        //        }
        //    }
        //}

        public async Task TryExecuteAsync(Action action)
        {
            await Task.Run(() =>
                           {
                               var policy = _policy.Invoke();

                               do
                               {
                                   try
                                   {
                                       Console.WriteLine("{0}\t" + @"Trying to invoke : {1}", DateTime.Now, action);

                                       action.Invoke();
                                       policy = _policy.Invoke();
                                       _bypass.Set();
                                   }
                                   catch (Exception ex)
                                   {
                                       Console.WriteLine("{0}\t" + @"Too bad !" + "\t{1}", DateTime.Now, ex.Message);

                                       if (!policy.CanRetry(ex))
                                       {
                                           _bypass.Set();

                                           throw new MaxCountExpiredException(ex);
                                       }
                                   }
                               } while (!_bypass.WaitOne(_interval));
                           });
        }
    }
}