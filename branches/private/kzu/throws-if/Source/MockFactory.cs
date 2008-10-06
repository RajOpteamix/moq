﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moq
{
	/// <summary>
	/// Utility factory class to use to construct multiple 
	/// mocks when consistent verification is 
	/// desired for all of them.
	/// </summary>
	/// <remarks>
	/// If multiple mocks will be created during a test, passing 
	/// the desired <see cref="MockBehavior"/> (if different than the 
	/// <see cref="MockBehavior.Default"/> or the one 
	/// passed to the factory constructor) and later verifying each
	/// mock can become repetitive and tedious.
	/// <para>
	/// This factory class helps in that scenario by providing a 
	/// simplified creation of multiple mocks with a default 
	/// <see cref="MockBehavior"/> (unless overriden by calling 
	/// <see cref="Create{T}(MockBehavior)"/>) and posterior verification.
	/// </para>
	/// </remarks>
	/// <example group="factory">
	/// The following is a straightforward example on how to 
	/// create and automatically verify strict mocks using a <see cref="MockFactory"/>:
	/// <code>
	/// var factory = new MockFactory(MockBehavior.Strict);
	/// 
	/// var foo = factory.Create&lt;IFoo&gt;();
	/// var bar = factory.Create&lt;IBar&gt;();
	/// 
	///	// no need to call Verifiable() on the expectation 
	///	// as we'll be validating all expectations anyway.
	/// foo.Expect(f => f.Do());
	/// bar.Expect(b => b.Redo());
	/// 
	///	// exercise the mocks here
	/// 
	/// factory.VerifyAll(); 
	/// // At this point all expectations are already checked 
	/// // and an optional MockException might be thrown. 
	/// // Note also that because the mocks are strict, any invocation 
	/// // that doesn't have a matching expectation will also throw a MockException.
	/// </code>
	/// The following examples shows how to setup the factory 
	/// to create loose mocks and later verify only verifiable expectations:
	/// <code>
	/// var factory = new MockFactory(MockBehavior.Loose);
	/// 
	/// var foo = factory.Create&lt;IFoo&gt;();
	/// var bar = factory.Create&lt;IBar&gt;();
	/// 
	/// // this expectation will be verified at the end of the "using" block
	/// foo.Expect(f => f.Do()).Verifiable();
	/// 	
	/// // this expectation will NOT be verified 
	/// foo.Expect(f => f.Calculate());
	/// 	
	/// // this expectation will be verified at the end of the "using" block
	/// bar.Expect(b => b.Redo()).Verifiable();
	/// 
	///	// exercise the mocks here
	///	// note that because the mocks are Loose, members 
	///	// called in the interfaces for which no matching
	///	// expectations exist will NOT throw exceptions, 
	///	// and will rather return default values.
	///	
	/// factory.Verify();
	/// // At this point verifiable expectations are already checked 
	/// // and an optional MockException might be thrown.
	/// </code>
	/// The following examples shows how to setup the factory with a 
	/// default strict behavior, overriding that default for a 
	/// specific mock:
	/// <code>
	/// var factory = new MockFactory(MockBehavior.Strict);
	/// 
	/// // this particular one we want loose
	/// var foo = factory.Create&lt;IFoo&gt;(MockBehavior.Loose);
	/// var bar = factory.Create&lt;IBar&gt;();
	/// 
	/// // set expectations
	/// 
	///	// exercise the mocks here
	///	
	/// factory.Verify();
	/// </code>
	/// </example>
	/// <seealso cref="MockBehavior"/>
	public sealed class MockFactory
	{
		List<IVerifiable> mocks = new List<IVerifiable>();
		MockBehavior defaultBehavior;

		/// <summary>
		/// Initializes the factory with the given <paramref name="defaultBehavior"/> 
		/// for newly created mocks from the factory.
		/// </summary>
		/// <param name="defaultBehavior">The behavior to use for mocks created 
		/// using the <see cref="Create{T}()"/> factory method if not overriden 
		/// by using the <see cref="Create{T}(MockBehavior)"/> overload.</param>
		public MockFactory(MockBehavior defaultBehavior)
		{
			this.defaultBehavior = defaultBehavior;
		}

		/// <summary>
		/// Creates a new mock with the default <see cref="MockBehavior"/> 
		/// specified at factory construction time.
		/// </summary>
		/// <typeparam name="T">Type to mock.</typeparam>
		/// <returns>A new <see cref="Mock{T}"/>.</returns>
		/// <example ignore="true">
		/// <code>
		/// var factory = new MockFactory(MockBehavior.Strict);
		/// 
		/// var foo = factory.Create&lt;IFoo&gt;();
		/// // use mock on tests
		/// 
		/// factory.VerifyAll();
		/// </code>
		/// </example>
		public Mock<T> Create<T>()
			where T : class
		{
			return CreateImpl<T>(defaultBehavior, new object[0]);
		}

		/// <summary>
		/// Creates a new mock with the default <see cref="MockBehavior"/> 
		/// specified at factory construction time and with the 
		/// the given constructor arguments for the class.
		/// </summary>
		/// <remarks>
		/// The mock will try to find the best match constructor given the 
		/// constructor arguments, and invoke that to initialize the instance. 
		/// This applies only to classes, not interfaces.
		/// </remarks>
		/// <typeparam name="T">Type to mock.</typeparam>
		/// <param name="args">Constructor arguments for mocked classes.</param>
		/// <returns>A new <see cref="Mock{T}"/>.</returns>
		/// <example ignore="true">
		/// <code>
		/// var factory = new MockFactory(MockBehavior.Default);
		/// 
		/// var mock = factory.Create&lt;MyBase&gt;("Foo", 25, true);
		/// // use mock on tests
		/// 
		/// factory.Verify();
		/// </code>
		/// </example>
		public Mock<T> Create<T>(params object[] args)
			where T : class
		{
			// "fix" compiler picking this overload instead of 
			// the one receiving the mock behavior.
			if (args.Length > 0 && args[0] is MockBehavior)
				return CreateImpl<T>((MockBehavior)args[0], args.Skip(1).ToArray());
			else
				return CreateImpl<T>(defaultBehavior, args);
		}

		/// <summary>
		/// Creates a new mock with the given <paramref name="behavior"/>.
		/// </summary>
		/// <typeparam name="T">Type to mock.</typeparam>
		/// <param name="behavior">Behavior to use for the mock, which overrides 
		/// the default behavior specified at factory construction time.</param>
		/// <returns>A new <see cref="Mock{T}"/>.</returns>
		/// <example group="factory">
		/// The following example shows how to create a mock with a different 
		/// behavior to that specified as the default for the factory:
		/// <code>
		/// var factory = new MockFactory(MockBehavior.Strict);
		/// 
		/// var foo = factory.Create&lt;IFoo&gt;(MockBehavior.Normal);
		/// </code>
		/// </example>
		public Mock<T> Create<T>(MockBehavior behavior)
			where T : class
		{
			return CreateImpl<T>(behavior, new object[0]);
		}

		/// <summary>
		/// Creates a new mock with the given <paramref name="behavior"/> 
		/// and with the the given constructor arguments for the class.
		/// </summary>
		/// <remarks>
		/// The mock will try to find the best match constructor given the 
		/// constructor arguments, and invoke that to initialize the instance. 
		/// This applies only to classes, not interfaces.
		/// </remarks>
		/// <typeparam name="T">Type to mock.</typeparam>
		/// <param name="behavior">Behavior to use for the mock, which overrides 
		/// the default behavior specified at factory construction time.</param>
		/// <param name="args">Constructor arguments for mocked classes.</param>
		/// <returns>A new <see cref="Mock{T}"/>.</returns>
		/// <example group="factory">
		/// The following example shows how to create a mock with a different 
		/// behavior to that specified as the default for the factory, passing 
		/// constructor arguments:
		/// <code>
		/// var factory = new MockFactory(MockBehavior.Default);
		/// 
		/// var mock = factory.Create&lt;MyBase&gt;(MockBehavior.Strict, "Foo", 25, true);
		/// </code>
		/// </example>
		public Mock<T> Create<T>(MockBehavior behavior, params object[] args)
			where T : class
		{
			return CreateImpl<T>(behavior, args);
		}

		private Mock<T> CreateImpl<T>(MockBehavior behavior, object[] args)
			where T : class
		{
			var mock = new Mock<T>(behavior, args);
			mocks.Add(mock);

			return mock;
		}

		/// <summary>
		/// Verifies all verifiable expectations on all mocks created 
		/// by this factory.
		/// </summary>
		/// <seealso cref="Mock{T}.Verify()"/>
		/// <exception cref="MockException">One or more mocks had expectations that were not satisfied.</exception>
		public void Verify()
		{
			VerifyImpl(verifiable => verifiable.Verify());
		}

		/// <summary>
		/// Verifies all verifiable expectations on all mocks created 
		/// by this factory.
		/// </summary>
		/// <seealso cref="Mock{T}.Verify()"/>
		/// <exception cref="MockException">One or more mocks had expectations that were not satisfied.</exception>
		public void VerifyAll()
		{
			VerifyImpl(verifiable => verifiable.VerifyAll());
		}

		private void VerifyImpl(Action<IVerifiable> verifyAction)
		{
			StringBuilder message = new StringBuilder();

			foreach (var mock in mocks)
			{
				try
				{
					verifyAction(mock);
				}
				catch (MockVerificationException mve)
				{
					message.AppendLine(mve.GetRawExpectations());
				}
			}

			if (message.ToString().Length > 0)
				throw new MockException(MockException.ExceptionReason.VerificationFailed,
					String.Format(Properties.Resources.VerficationFailed, message));
		}
	}
}