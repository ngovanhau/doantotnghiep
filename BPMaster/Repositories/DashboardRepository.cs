using System.Data;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Databases;
using Common.Repositories;
using Dapper;
using Domain.Entities;
using Common.Application.CustomAttributes;
using Common.Services;
using Repositories;
using Utilities;


namespace Repositories
{
    [ScopedService]
    public class DashboardRepository(IDbConnection connection) : SimpleCrudRepository<Dashboard, Guid>(connection)
    {
        public async Task<Dashboard> GetDashboardSummary()
        {
            var sql = @"
            SELECT
            (SELECT COUNT(*) FROM building) AS building,
            (SELECT COUNT(*) FROM customer) AS customer,
            (SELECT COUNT(*) FROM contract) AS contract,
            (SELECT COUNT(*) FROM problem) AS problem,
            (SELECT COUNT(*) FROM room) AS room";

            var result = await connection.QueryFirstOrDefaultAsync<Dashboard>(sql);

            return result;
        }
    }
}
