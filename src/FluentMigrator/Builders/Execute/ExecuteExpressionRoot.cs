#region License
// 
// Copyright (c) 2007-2009, Sean Chambers <schambers80@gmail.com>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace FluentMigrator.Builders.Execute
{
	public class ExecuteExpressionRoot : IExecuteExpressionRoot
	{
		private readonly IMigrationContext _context;

		public ExecuteExpressionRoot(IMigrationContext context)
		{
			_context = context;
		}

		public void Sql(string sqlStatement)
		{
			var expression = new ExecuteSqlStatementExpression {SqlStatement = sqlStatement};
			_context.Expressions.Add(expression);
		}

		public void Script(string sqlScript)
		{
			var expression = new ExecuteSqlScriptExpression {SqlScript = sqlScript};
			_context.Expressions.Add(expression);
		}
	}
}