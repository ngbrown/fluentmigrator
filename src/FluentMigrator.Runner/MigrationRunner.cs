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
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace FluentMigrator.Runner
{
	public class MigrationRunner
	{
		private IAnnouncer _announcer;
		public IMigrationConventions Conventions { get; private set; }
		public IMigrationProcessor Processor { get; private set; }
		public IList<Exception> CaughtExceptions { get; private set; }
		public bool SilentlyFail { get; set; }
		private IStopWatch _stopWatch;

		public MigrationRunner(IMigrationConventions conventions, IMigrationProcessor processor)
			: this(conventions, processor, new Announcer(Console.Out), new StopWatch())
		{
		}

		public MigrationRunner(IMigrationConventions conventions, IMigrationProcessor processor, IAnnouncer announcer, IStopWatch stopWatch)
		{
			_announcer = announcer;
			SilentlyFail = false;
			CaughtExceptions = null;
			Conventions = conventions;
			Processor = processor;
			_stopWatch = stopWatch;
		}

		public void Up(IMigration migration)
		{
			var name = migration.GetType().Name;
			_announcer.Announce(name + ": migrating");
			
			CaughtExceptions = new List<Exception>();

			var context = new MigrationContext(Conventions,Processor);

			migration.GetUpExpressions(context);

			_stopWatch.Start();

			ExecuteExpressions(context.Expressions);

			_stopWatch.Stop();
			
			var elapsed = _stopWatch.ElapsedTime().TotalSeconds;

			_announcer.Announce(name + ": migrated (" + elapsed + "s" + ")");
		}

		public void Down(IMigration migration)
		{
			var name = migration.GetType().Name;
			_announcer.Announce(name + ": reverting");
			
			CaughtExceptions = new List<Exception>();

			var context = new MigrationContext(Conventions,Processor);
			migration.GetDownExpressions(context);

			_stopWatch.Start();

			ExecuteExpressions(context.Expressions);

			_stopWatch.Stop();

			var elapsed = _stopWatch.ElapsedTime().TotalSeconds;

			_announcer.Announce(name + ": reverted (" + elapsed + "s" + ")");
		}

		/// <summary>
		/// execute each migration expression in the expression collection
		/// </summary>
		/// <param name="expressions"></param>
		protected void ExecuteExpressions(ICollection<IMigrationExpression> expressions)
		{
			foreach (IMigrationExpression expression in expressions)
			{
				try
				{
					expression.ApplyConventions( Conventions );
					time(expression.ToString(), () => expression.ExecuteWith(Processor));
				}
				catch (Exception er)
				{
					_announcer.Say(er.Message);

					//catch the error and move onto the next expression
					if (SilentlyFail)
					{
						CaughtExceptions.Add(er);
						continue;
					}
					throw;
				}
			}
		}

		private void time(string message, Action action)
		{
			_announcer.Say(message);

			_stopWatch.Start();

			action();

			_stopWatch.Stop();

			var elapsed = _stopWatch.ElapsedTime().TotalSeconds;

			_announcer.SaySubItem(elapsed + "s");
		}
	}
}