﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq.Language.Flow;
using Moq.Language;

namespace Moq
{
	/// <summary>
	/// Makes the Verifiable legacy members on the Moq fluent API visible.
	/// </summary>
	public static class VerifiableExtension
	{
		/// <summary>
		/// Marks the setup as verifiable, meaning that a call 
		/// to <c>mock.Verify()</c> will check if this particular 
		/// setup was matched.
		/// </summary>
		/// <example>
		/// The following example marks the setup as verifiable:
		/// <code>
		/// mock.Setup(x => x.Execute("ping"))
		///     .Returns(true)
		///     .Verifiable();
		/// </code>
		/// </example>
		public static void Verifiable(this IVerifies mock)
		{
			((MethodCall)mock).IsVerifiable = true;
		}
	}
}
