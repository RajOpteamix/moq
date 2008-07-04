﻿//Copyright (c) 2007, Moq Team 
//http://code.google.com/p/moq/
//All rights reserved.

//Redistribution and use in source and binary forms, 
//with or without modification, are permitted provided 
//that the following conditions are met:

//    * Redistributions of source code must retain the 
//    above copyright notice, this list of conditions and 
//    the following disclaimer.

//    * Redistributions in binary form must reproduce 
//    the above copyright notice, this list of conditions 
//    and the following disclaimer in the documentation 
//    and/or other materials provided with the distribution.

//    * Neither the name of the <ORGANIZATION> nor the 
//    names of its contributors may be used to endorse 
//    or promote products derived from this software 
//    without specific prior written permission.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
//CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
//INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
//MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
//DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
//CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
//SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
//BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
//SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
//INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
//OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
//SUCH DAMAGE.

//[This is the BSD license, see
// http://www.opensource.org/licenses/bsd-license.php]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moq
{
	/// <summary>
	/// Base class for mocks and static helper class with methods that 
	/// apply to mocked objects, such as <see cref="Get"/> to 
	/// retrieve a <see cref="Mock{T}"/> from an object instance.
	/// </summary>
	public abstract class Mock : IHideObjectMembers
	{
		/// <summary>
		/// Retrieves the mock object for the given object instance.
		/// </summary>
		/// <typeparam name="T">Type of the mock to retrieve. Can be omitted as it's inferred 
		/// from the object instance passed in as the <paramref name="mocked"/> instance.</typeparam>
		/// <param name="mocked">The instance of the mocked object.</param>
		/// <returns>The mock associated with the mocked object.</returns>
		/// <exception cref="ArgumentException">The received <paramref name="mocked"/> instance 
		/// was not created by Moq.</exception>
		/// <example group="advanced">
		/// The following example shows how to add a new expectation to an object 
		/// instance which is not the original <see cref="Mock{T}"/> but rather 
		/// the object associated with it:
		/// <code>
		/// // Typed instance, not the mock, is retrieved from some test API.
		/// HttpContextBase context = GetMockContext();
		/// 
		/// // context.Request is the typed object from the "real" API
		/// // so in order to add an expectation to it, we need to get 
		/// // the mock that "owns" it
		/// Mock&lt;HttpRequestBase&gt; request = Mock.Get(context.Request);
		/// mock.Expect(req => req.AppRelativeCurrentExecutionFilePath)
		///     .Returns(tempUrl);
		/// </code>
		/// </example>
		public static IMock<T> Get<T>(T mocked)
			where T : class
		{
			if (mocked is IMocked<T>)
			{
				// This would be the fastest check.
				return (mocked as IMocked<T>).Mock;
			}
			else if (mocked is IMocked)
			{
				// We may have received a T of an implemented 
				// interface in the mock.
				var mock = ((IMocked)mocked).Mock;
				var imockedType = mocked.GetType().GetInterface("IMocked`1");
				var mockedType = imockedType.GetGenericArguments()[0];

				if (mock.ImplementedInterfaces.Contains(typeof(T)))
				{
					var asMethod = mock.GetType().GetMethod("As");
					var asInterface = asMethod.MakeGenericMethod(typeof(T));
					var asMock = asInterface.Invoke(mock, null);

					return (IMock<T>)asMock;
				}
				else
				{
					// Alternatively, we may have been asked 
					// for a type that is assignable to the 
					// one for the mock.
					// This is not valid as generic types 
					// do not support covariance on 
					// the generic parameters.
					var types = String.Join(", ",
							new[] { mockedType }
							// Skip first interface which is always our internal IMocked<T>
							.Concat(mock.ImplementedInterfaces.Skip(1))
							.Select(t => t.Name)
							.ToArray());

					throw new ArgumentException(String.Format(Properties.Resources.InvalidMockGetType, 
						typeof(T).Name, types));
				}
			}
			else
			{
				throw new ArgumentException(Properties.Resources.ObjectInstanceNotMock, "mocked");
			}
		}

		Dictionary<EventInfo, List<Delegate>> invocationLists = new Dictionary<EventInfo, List<Delegate>>();

		/// <summary>
		/// Initializes the mock
		/// </summary>
		protected Mock()
		{
			this.CallBase = false;
			ImplementedInterfaces = new List<Type>();
		}

		/// <summary>
		/// Exposes the list of extra interfaces implemented by the mock.
		/// </summary>
		protected internal List<Type> ImplementedInterfaces { get; private set; }

		/// <summary>
		/// Whether the base member virtual implementation will be called 
		/// for mocked classes if no expectation is met. Defaults to <see langword="true"/>.
		/// </summary>
		public bool CallBase { get; set; }

		/// <summary>
		/// Exposes the mocked object instance.
		/// </summary>
		public object Object { get { return GetObject(); } }

		/// <summary>
		/// Returns the mocked object value.
		/// </summary>
		protected abstract object GetObject();

		internal void AddEventHandler(EventInfo ev, Delegate handler)
		{
			List<Delegate> handlers;
			if (!invocationLists.TryGetValue(ev, out handlers))
			{
				handlers = new List<Delegate>();
				invocationLists.Add(ev, handlers);
			}

			handlers.Add(handler);
		}

		internal IEnumerable<Delegate> GetInvocationList(EventInfo ev)
		{
			List<Delegate> handlers;
			if (!invocationLists.TryGetValue(ev, out handlers))
				return new Delegate[0];
			else
				return handlers;
		}

		/// <summary>
		/// Creates a handler that can be associated to an event receiving 
		/// the given <typeparamref name="TEventArgs"/> and can be used 
		/// to raise the event.
		/// </summary>
		/// <typeparam name="TEventArgs">Type of <see cref="EventArgs"/> 
		/// data passed in to the event.</typeparam>
		/// <example>
		/// This example shows how to invoke an event with a custom event arguments 
		/// class in a view that will cause its corresponding presenter to 
		/// react by changing its state:
		/// <code>
		/// var mockView = new Mock&lt;IOrdersView&gt;();
		/// var mockedEvent = mockView.CreateEventHandler&lt;OrderEventArgs&gt;();
		/// 
		/// var presenter = new OrdersPresenter(mockView.Object);
		/// 
		/// // Check that the presenter has no selection by default
		/// Assert.Null(presenter.SelectedOrder);
		/// 
		/// // Create a mock event handler of the appropriate type
		/// var handler = mockView.CreateEventHandler&lt;OrderEventArgs&gt;();
		/// // Associate it with the event we want to raise
		/// mockView.Object.Cancel += handler;
		/// // Finally raise the event with a specific arguments data
		/// handler.Raise(new OrderEventArgs { Order = new Order("moq", 500) });
		/// 
		/// // Now the presenter reacted to the event, and we have a selected order
		/// Assert.NotNull(presenter.SelectedOrder);
		/// Assert.Equal("moq", presenter.SelectedOrder.ProductName);
		/// </code>
		/// </example>
		public MockedEvent<TEventArgs> CreateEventHandler<TEventArgs>() where TEventArgs : EventArgs
		{
			return new MockedEvent<TEventArgs>(this);
		}

		/// <summary>
		/// Creates a handler that can be associated to an event receiving 
		/// a generic <see cref="EventArgs"/> and can be used 
		/// to raise the event.
		/// </summary>
		/// <example>
		/// This example shows how to invoke a generic event in a view that will 
		/// cause its corresponding presenter to react by changing its state:
		/// <code>
		/// var mockView = new Mock&lt;IOrdersView&gt;();
		/// var mockedEvent = mockView.CreateEventHandler();
		/// 
		/// var presenter = new OrdersPresenter(mockView.Object);
		/// 
		/// // Check that the presenter is not in the "Canceled" state
		/// Assert.False(presenter.IsCanceled);
		/// 
		/// // Create a mock event handler of the appropriate type
		/// var handler = mockView.CreateEventHandler();
		/// // Associate it with the event we want to raise
		/// mockView.Object.Cancel += handler;
		/// // Finally raise the event
		/// handler.Raise(EventArgs.Empty);
		/// 
		/// // Now the presenter reacted to the event, and changed its state
		/// Assert.True(presenter.IsCanceled);
		/// </code>
		/// </example>
		public MockedEvent<EventArgs> CreateEventHandler()
		{
			return new MockedEvent<EventArgs>(this);
		}

		class NullMockedEvent : MockedEvent
		{
			public NullMockedEvent(Mock mock)
				: base(mock)
			{
			}
		}
	}
}
