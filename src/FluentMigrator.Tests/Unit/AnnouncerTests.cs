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
using System.IO;
using FluentMigrator.Runner;
using NUnit.Framework;
using NUnit.Should;

namespace FluentMigrator.Tests.Unit
{
	[TestFixture]
	public class AnnouncerTests
	{
		private Announcer _announcer;
		private StringWriter _stringWriter;

		[SetUp]
		public void SetUp()
		{
			_stringWriter = new StringWriter();
			_announcer = new Announcer(_stringWriter);
		}

		public string Output
		{
			get
			{
				return _stringWriter.GetStringBuilder().ToString();
			}
		}

		[Test]
		public void CanAnnounceAndPadWithEquals()
		{
			_announcer.Announce("Test");
			Output.ShouldBe("==  Test ======================================================================" + Environment.NewLine);
		}

		[Test]
		public void CanSay()
		{
			_announcer.Say("Create table");
			Output.ShouldBe("-- Create table" + Environment.NewLine);
		}

		[Test]
		public void CanSaySubItem()
		{
			_announcer.SaySubItem("0.0512s");
			Output.ShouldBe("   -> 0.0512s" + Environment.NewLine);
		}
	}
}
