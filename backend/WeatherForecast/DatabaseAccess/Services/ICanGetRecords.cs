using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KickBase.DatabaseAccess.Services
{
    /// <summary>
    /// This interfaces provides records from database source
    /// </summary>
    public interface ICanGetRecords
    {
        /// <summary>
        /// This method returns a secuence of records queried on the database. The return objects type is defined 
        /// by the given Type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type of return values</typeparam>
        /// <param name="query">Query to execute</param>
        /// <returns>Returns the secuence of items selected </returns>
        Task<IEnumerable<T>> GetRecords<T>(string query)
            where T: class, new();
    }
}
