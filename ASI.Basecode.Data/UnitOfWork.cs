using ASI.Basecode.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASI.Basecode.Data
{

    /// 
    /// Unit of Work Implementation
    /// 
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        /// 
        /// Gets the database context
        /// 
        public DbContext Database { get; private set; }

        /// 
        /// Initializes a new instance of the UnitOfWork class.
        /// 
        /// <param name="serviceContext">The service context.</param>
        public UnitOfWork(AsiBasecodeDBContext serviceContext)
        {
            Database = serviceContext;
        }

        /// 
        /// Saves the changes to database
        /// 
        public void SaveChanges()
        {
            Database.SaveChanges();
        }

        /// 
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// 
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
