using Dapper;
using System.Data;
using System.Data.Common;

namespace MIDIS.SGPVL.Utils.Helpers.Dapper
{
    public interface IDapper
    {
        DbConnection GetDbConnection();

        Task<List<T>> GetAll<T>(string sql, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure);
        Task<T> Get<T>(string sql, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure);
        Task<T> Insert<T>(string sql, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure);
        Task<T> Update<T>(string sql, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure);
        Task<int> Execute(string sql, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure); //Delete or others

    }
}
