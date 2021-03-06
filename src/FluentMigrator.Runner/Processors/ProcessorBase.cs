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

using FluentMigrator.Builders.Insert;
using FluentMigrator.Expressions;

namespace FluentMigrator.Runner.Processors
{
	public abstract class ProcessorBase : IMigrationProcessor
	{
		protected IMigrationGenerator generator;

		public virtual void Process(CreateTableExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public virtual void Process(CreateColumnExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public virtual void Process(DeleteTableExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public virtual void Process(DeleteColumnExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public virtual void Process(CreateForeignKeyExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public virtual void Process(DeleteForeignKeyExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public virtual void Process(CreateIndexExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public virtual void Process(DeleteIndexExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public virtual void Process(RenameTableExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public virtual void Process(RenameColumnExpression expression)
		{
			Process(generator.Generate(expression));
		}

		public void Process(InsertDataExpression expression)
		{
			Process(generator.Generate(expression));
		}

		protected abstract void Process(string sql);

		public virtual void CommitTransaction()
		{
		}

		public virtual void RollbackTransaction()
		{
		}

		public abstract System.Data.DataSet ReadTableData(string tableName);
		public abstract System.Data.DataSet Read(string template, params object[] args);
		public abstract bool Exists(string template, params object[] args);
		public abstract void Execute(string template, params object[] args);
		public abstract bool TableExists(string tableName);
		public abstract bool ColumnExists(string tableName, string columnName);
		public abstract bool ConstraintExists(string tableName, string constraintName);
	}
}