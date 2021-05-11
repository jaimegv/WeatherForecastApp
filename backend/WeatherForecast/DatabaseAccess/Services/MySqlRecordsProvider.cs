using AutoMapper;
using Microsoft.Extensions.Logging;
using KickBase.DatabaseAccess.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KickBase.DatabaseAccess.Services
{
    public class MySqlRecordsProvider : ICanGetRecords
    {
        private readonly ILogger<MySqlRecordsProvider> _logger;
        private readonly DbConfiguration _mySqlConfiguration;
        private readonly IMapper _mapper;
        
        public MySqlRecordsProvider(
            ILogger<MySqlRecordsProvider> logger,
            DbConfiguration mySqlConfiguration,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mySqlConfiguration = mySqlConfiguration ?? throw new ArgumentNullException(nameof(mySqlConfiguration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<T>> GetRecords<T>(string query)
            where T : class, new()
        {
            if(string.IsNullOrEmpty(query))
            {
                throw new ArgumentException($"Null or empty {nameof(query)}");
            }

            var items = new List<T>();

            using (var mySqlConnection = new MySqlConnection(_mySqlConfiguration.ConnectionString))
            using (var sqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                try
                {
                    mySqlConnection.Open();
                    var reader = sqlCommand.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        var newItem = _mapper.Map<T>(reader);
                        if (newItem != null)
                        {
                            items.Add(newItem);
                        }
                        else
                        {
                            _logger.LogError($"{nameof(GetRecords)}: error while reading items from db");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception while reading db {@query}", query);
                }
                finally
                {
                    mySqlConnection.Close();
                }
                return items;
            }
        }
    }
}
