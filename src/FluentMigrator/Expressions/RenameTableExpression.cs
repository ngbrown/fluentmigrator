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

using System;
using System.Collections.Generic;
using FluentMigrator.Infrastructure;

namespace FluentMigrator.Expressions
{
	public class RenameTableExpression : MigrationExpressionBase
	{
		public virtual string OldName { get; set; }
		public virtual string NewName { get; set; }

		public override void CollectValidationErrors(ICollection<string> errors)
		{
			if (String.IsNullOrEmpty(OldName))
				errors.Add(ErrorMessages.OldTableNameCannotBeNullOrEmpty);

			if (String.IsNullOrEmpty(NewName))
				errors.Add(ErrorMessages.NewTableNameCannotBeNullOrEmpty);
		}

		public override void ExecuteWith(IMigrationProcessor processor)
		{
			processor.Process(this);
		}

		public override IMigrationExpression Reverse()
		{
			return new RenameTableExpression { OldName = NewName, NewName = OldName };
		}

		public override string ToString()
		{
			return base.ToString() + OldName + " " + NewName;
		}
	}
}