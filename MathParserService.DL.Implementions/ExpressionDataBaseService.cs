using MathParserService.DAL;
using MathParserService.DL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.Implementions
{
    public class ExpressionDataBaseService: DatabaseService<Expression>
    {
        public ExpressionDataBaseService(IRepository<Expression> repository): base(repository)
        {
        }

        public override Task SaveAsync(Expression expression)
        {
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
            return base.SaveAsync(expression);
        }
    }
}
