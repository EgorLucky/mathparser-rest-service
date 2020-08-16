using MathParserService.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IRepository _repository;

        public DatabaseService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task SaveAsync(Expression expression)
        {
            if (expression.Id == Guid.Empty)
                expression.Id = Guid.NewGuid();

            if (expression.CreateDate == default)
                expression.CreateDate = DateTimeOffset.Now;

            if (expression.Points != null)
                foreach (var point in expression.Points)
                {
                    if (point.Id == Guid.Empty)
                        point.Id = Guid.NewGuid();

                    if (point.ExpressionId != expression.Id)
                        point.ExpressionId = expression.Id;

                    if (point.Coordinates != null)
                        foreach (var coordinate in point.Coordinates)
                        {
                            if (coordinate.Id == Guid.Empty)
                                coordinate.Id = Guid.NewGuid();

                            if (coordinate.PointId != point.Id)
                                coordinate.PointId = point.Id;
                        }
                }

            if (expression.Parameters != null)
                foreach (var parameter in expression.Parameters)
                {
                    if (parameter.Id == Guid.Empty)
                        parameter.Id = Guid.NewGuid();

                    if (parameter.ExpressionId != expression.Id)
                        parameter.ExpressionId = expression.Id;

                    if (parameter.Values != null)
                        foreach (var value in parameter.Values)
                        {
                            if (value.Id == Guid.Empty)
                                value.Id = Guid.NewGuid();

                            if (value.ParameterId != parameter.Id)
                                value.ParameterId = parameter.Id;
                        }
                }

            await _repository.SaveAsync(expression);
        }

        public Task<List<Expression>> GetLastAsync(int limit)
        {
            return _repository.GetLastAsync(limit);
        }
    }
}
