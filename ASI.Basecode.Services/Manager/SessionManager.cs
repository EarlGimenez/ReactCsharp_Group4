using Microsoft.AspNetCore.Http;

namespace ASI.Basecode.Services.Manager
{
    /// 
    /// Session Manager
    /// 
    public class SessionManager
    {
        /// 
        /// Initializes a new instance of the SessionManager class.
        /// 
        /// <param name="session">Session</param>
        public SessionManager(ISession session)
        {
            this._session = session;
        }

        /// 
        /// Gets or sets the session.
        /// 
        protected ISession _session { get; set; }

        /// 
        /// Clears the session instance.
        /// 
        public void Clear()
        {
            this._session.Clear();
        }
    }
}
