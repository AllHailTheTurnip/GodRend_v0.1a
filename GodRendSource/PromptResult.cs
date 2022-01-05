using System;

namespace GodRendSource
{
    public struct PromptResult : IDisposable
    {
        public bool succeeded, retry;
        public string reason;

        public PromptResult(bool succeeded, string reason, bool retry = true)
        {
            this.succeeded = succeeded;
            this.reason = reason;
            this.retry = retry;
        }
        
        
        public static PromptResult Succeeded => new PromptResult(true, String.Empty, false);

        public static PromptResult Failed(string reason, bool retry = true)
        {
            return new PromptResult(false, reason, retry);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}