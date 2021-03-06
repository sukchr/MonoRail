﻿#region License
//  Copyright 2004-2010 Castle Project - http://www.castleproject.org/
//  
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// 
#endregion
namespace Castle.MonoRail.Tests
{
	using System.Web;
	using MonoRail.Mvc;
	using MonoRail.Mvc.Typed;
	using Moq;
	using NUnit.Framework;

	[TestFixture]
	public class StringResultTestCase
	{
		[Test]
		public void Execute_should_write_on_response_output_stream()
		{
			var http = new Mock<HttpContextBase>();
			var response = new Mock<HttpResponseBase>();
			var context = new ActionResultContext("test", "testcontroller", "action", http.Object);

			var result = new StringResult("value");

			http.SetupGet(ctx => ctx.Response).Returns(response.Object);

			response.Setup(rp => rp.Write("value"));

			result.Execute(context, new ControllerContext(), null);
		}
	}
}
