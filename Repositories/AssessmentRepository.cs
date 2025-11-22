using System.Data;
using Dapper;
using WorkWell.Api.Models;

namespace WorkWell.Api.Repositories
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly IDbConnection _db;

        public AssessmentRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Assessment>> GetAllByUserAsync(decimal useresId)
        {
            var sql = @"
                SELECT 
                    id,
                    humor,
                    estresse,
                    produtividade,
                    useres_id AS UseresId
                FROM assessment
                WHERE useres_id = :UseresId
                ORDER BY id";

            return await _db.QueryAsync<Assessment>(sql, new { UseresId = useresId });
        }

        public async Task<Assessment?> GetByIdAsync(decimal id)
        {
            var sql = @"
                SELECT 
                    id,
                    humor,
                    estresse,
                    produtividade,
                    useres_id AS UseresId
                FROM assessment
                WHERE id = :Id";

            return await _db.QueryFirstOrDefaultAsync<Assessment>(sql, new { Id = id });
        }

        public async Task<decimal> CreateAsync(Assessment a)
        {
            var sql = @"
                INSERT INTO assessment 
                    (humor, estresse, produtividade, useres_id)
                VALUES 
                    (:Humor, :Estresse, :Produtividade, :UseresId)
                RETURNING id INTO :Id";

            var parameters = new DynamicParameters();
            parameters.Add("Humor", a.Humor);
            parameters.Add("Estresse", a.Estresse);
            parameters.Add("Produtividade", a.Produtividade);
            parameters.Add("UseresId", a.UseresId);
            parameters.Add("Id", dbType: DbType.Decimal, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(sql, parameters);

            return parameters.Get<decimal>("Id");
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var sql = "DELETE FROM assessment WHERE id = :Id";
            var rows = await _db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
