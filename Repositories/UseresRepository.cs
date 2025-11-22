using System.Data;
using Dapper;
using WorkWell.Api.Models;

namespace WorkWell.Api.Repositories
{
    public class UserRepository : IUseresRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Useres>> GetAllAsync()
        {
            var sql = "SELECT id, nome, idade FROM useres ORDER BY id";
            return await _db.QueryAsync<Useres>(sql);
        }

        public async Task<Useres?> GetByIdAsync(decimal id)
        {
            var sql = "SELECT id, nome, idade FROM useres WHERE id = :Id";
            return await _db.QueryFirstOrDefaultAsync<Useres>(sql, new { Id = id });
        }

        public async Task<decimal> CreateAsync(Useres user)
        {
            var sql = @"
                INSERT INTO useres (nome, idade)
                VALUES (:Nome, :Idade)
                RETURNING id INTO :Id";

            var parameters = new DynamicParameters();
            parameters.Add("Nome", user.Nome);
            parameters.Add("Idade", user.Idade);
            parameters.Add("Id", dbType: DbType.Decimal, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(sql, parameters);

            return parameters.Get<decimal>("Id");
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var sql = "DELETE FROM useres WHERE id = :Id";
            var rows = await _db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<IEnumerable<Useres>> GetPagedAsync(int page, int pageSize)
        {
            var sql = @"
        SELECT *
        FROM (
            SELECT u.*, ROW_NUMBER() OVER (ORDER BY id) AS rn
            FROM useres u
        )
        WHERE rn BETWEEN :StartRow AND :EndRow";

            int startRow = ((page - 1) * pageSize) + 1;
            int endRow = page * pageSize;

            return await _db.QueryAsync<Useres>(sql, new
            {
                StartRow = startRow,
                EndRow = endRow
            });
        }

        public async Task<int> CountAsync()
        {
            return await _db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM useres");
        }

    }
}
