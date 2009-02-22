﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3521
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Moq.Properties
{


	/// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Moq.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mock type has already been initialized by accessing its Object property. Adding interfaces must be done before that..
        /// </summary>
        internal static string AlreadyInitialized {
            get {
                return ResourceManager.GetString("AlreadyInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value cannot be an empty string..
        /// </summary>
        internal static string ArgumentCannotBeEmpty {
            get {
                return ResourceManager.GetString("ArgumentCannotBeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can only add interfaces to the mock..
        /// </summary>
        internal static string AsMustBeInterface {
            get {
                return ResourceManager.GetString("AsMustBeInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t set return value for void method {0}..
        /// </summary>
        internal static string CantSetReturnValueForVoid {
            get {
                return ResourceManager.GetString("CantSetReturnValueForVoid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Constructor arguments cannot be passed for interface mocks..
        /// </summary>
        internal static string ConstructorArgsForInterface {
            get {
                return ResourceManager.GetString("ConstructorArgsForInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A matching constructor for the given arguments was not found on the mocked type..
        /// </summary>
        internal static string ConstructorNotFound {
            get {
                return ResourceManager.GetString("ConstructorNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression {0} involves a field access, which is not supported. Use properties instead..
        /// </summary>
        internal static string FieldsNotSupported {
            get {
                return ResourceManager.GetString("FieldsNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type to mock must be an interface or an abstract or non-sealed class. .
        /// </summary>
        internal static string InvalidMockClass {
            get {
                return ResourceManager.GetString("InvalidMockClass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot retrieve a mock with the given object type {0} as it&apos;s not the main type of the mock or any of its additional interfaces.
        ///Please cast the argument to one of the supported types: {1}.
        ///Remember that there&apos;s no generics covariance in the CLR, so your object must be one of these types in order for the call to succeed..
        /// </summary>
        internal static string InvalidMockGetType {
            get {
                return ResourceManager.GetString("InvalidMockGetType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Member {0}.{1} does not exist..
        /// </summary>
        internal static string MemberMissing {
            get {
                return ResourceManager.GetString("MemberMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method {0}.{1} is public. Use strong-typed Expect overload instead:
        ///mock.Setup(x =&gt; x.{1}());
        ///.
        /// </summary>
        internal static string MethodIsPublic {
            get {
                return ResourceManager.GetString("MethodIsPublic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} invocation failed with mock behavior {1}.
        ///{2}.
        /// </summary>
        internal static string MockExceptionMessage {
            get {
                return ResourceManager.GetString("MockExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected only {0} calls to {1}..
        /// </summary>
        internal static string MoreThanNCalls {
            get {
                return ResourceManager.GetString("MoreThanNCalls", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected only one call to {0}..
        /// </summary>
        internal static string MoreThanOneCall {
            get {
                return ResourceManager.GetString("MoreThanOneCall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///Invocation was performed on the mock less than {2} times: {1}.
        /// </summary>
        internal static string NoMatchingCallsAtLeast {
            get {
                return ResourceManager.GetString("NoMatchingCallsAtLeast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///Invocation was not performed on the mock: {1}.
        /// </summary>
        internal static string NoMatchingCallsAtLeastOnce {
            get {
                return ResourceManager.GetString("NoMatchingCallsAtLeastOnce", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///Invocation was performed on the mock more than {3} times: {1}.
        /// </summary>
        internal static string NoMatchingCallsAtMost {
            get {
                return ResourceManager.GetString("NoMatchingCallsAtMost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///Invocation was performed on the mock more than a time: {1}.
        /// </summary>
        internal static string NoMatchingCallsAtMostOnce {
            get {
                return ResourceManager.GetString("NoMatchingCallsAtMostOnce", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///Invocation was performed on the mock less or equal than {2} times or more or equal than {3} times: {1}.
        /// </summary>
        internal static string NoMatchingCallsBetweenExclusive {
            get {
                return ResourceManager.GetString("NoMatchingCallsBetweenExclusive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///Invocation was performed on the mock less than {2} times or more than {3} times: {1}.
        /// </summary>
        internal static string NoMatchingCallsBetweenInclusive {
            get {
                return ResourceManager.GetString("NoMatchingCallsBetweenInclusive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///Invocation was not performed on the mock {2} times: {1}.
        /// </summary>
        internal static string NoMatchingCallsExactly {
            get {
                return ResourceManager.GetString("NoMatchingCallsExactly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///Invocation was performed on the mock: {1}.
        /// </summary>
        internal static string NoMatchingCallsNever {
            get {
                return ResourceManager.GetString("NoMatchingCallsNever", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All invocations on the mock must have a corresponding setup..
        /// </summary>
        internal static string NoSetup {
            get {
                return ResourceManager.GetString("NoSetup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object instance was not created by Moq..
        /// </summary>
        internal static string ObjectInstanceNotMock {
            get {
                return ResourceManager.GetString("ObjectInstanceNotMock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property {0}.{1} does not exist..
        /// </summary>
        internal static string PropertyMissing {
            get {
                return ResourceManager.GetString("PropertyMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property {0}.{1} is write-only..
        /// </summary>
        internal static string PropertyNotReadable {
            get {
                return ResourceManager.GetString("PropertyNotReadable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property {0}.{1} is read-only..
        /// </summary>
        internal static string PropertyNotWritable {
            get {
                return ResourceManager.GetString("PropertyNotWritable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot raise a mocked event unless it has been associated (attached) to a concrete event in a mocked object..
        /// </summary>
        internal static string RaisedUnassociatedEvent {
            get {
                return ResourceManager.GetString("RaisedUnassociatedEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invocation needs to return a value and therefore must have a corresponding setup that provides it..
        /// </summary>
        internal static string ReturnValueRequired {
            get {
                return ResourceManager.GetString("ReturnValueRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A lambda expression is expected as the argument to It.Is&lt;T&gt;..
        /// </summary>
        internal static string SetupLambda {
            get {
                return ResourceManager.GetString("SetupLambda", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invocation {0} should not have been made..
        /// </summary>
        internal static string SetupNever {
            get {
                return ResourceManager.GetString("SetupNever", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression is not a method invocation: {0}.
        /// </summary>
        internal static string SetupNotMethod {
            get {
                return ResourceManager.GetString("SetupNotMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression is not a property access: {0}.
        /// </summary>
        internal static string SetupNotProperty {
            get {
                return ResourceManager.GetString("SetupNotProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression is not a property setter invocation..
        /// </summary>
        internal static string SetupNotSetter {
            get {
                return ResourceManager.GetString("SetupNotSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid setup on a non-overridable member:
        ///{0}.
        /// </summary>
        internal static string SetupOnNonOverridableMember {
            get {
                return ResourceManager.GetString("SetupOnNonOverridableMember", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type {0} does not implement required interface {1}.
        /// </summary>
        internal static string TypeNotImplementInterface {
            get {
                return ResourceManager.GetString("TypeNotImplementInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type {0} does not from required type {1}.
        /// </summary>
        internal static string TypeNotInheritFromType {
            get {
                return ResourceManager.GetString("TypeNotInheritFromType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To specify a setup for public property {0}.{1}, use the typed overloads, such as:
        ///mock.Setup(x =&gt; x.{1}).Returns(value);
        ///mock.SetupGet(x =&gt; x.{1}).Returns(value); //equivalent to previous one
        ///mock.SetupSet(x =&gt; x.{1}).Callback(callbackDelegate);
        ///.
        /// </summary>
        internal static string UnexpectedPublicProperty {
            get {
                return ResourceManager.GetString("UnexpectedPublicProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression {0} is not supported..
        /// </summary>
        internal static string UnsupportedExpression {
            get {
                return ResourceManager.GetString("UnsupportedExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only property accesses are supported in intermediate invocations on a setup. Unsupported expression {0}..
        /// </summary>
        internal static string UnsupportedIntermediateExpression {
            get {
                return ResourceManager.GetString("UnsupportedIntermediateExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression contains intermediate property access {0}.{1} which is of type {2} and cannot be mocked. Unsupported expression {3}..
        /// </summary>
        internal static string UnsupportedIntermediateType {
            get {
                return ResourceManager.GetString("UnsupportedIntermediateType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Setter expression cannot use argument matchers that receive parameters..
        /// </summary>
        internal static string UnsupportedMatcherParamsForSetter {
            get {
                return ResourceManager.GetString("UnsupportedMatcherParamsForSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Member {0} is not supported for protected mocking..
        /// </summary>
        internal static string UnsupportedMember {
            get {
                return ResourceManager.GetString("UnsupportedMember", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Setter expression can only use static custom matchers..
        /// </summary>
        internal static string UnsupportedNonStaticMatcherForSetter {
            get {
                return ResourceManager.GetString("UnsupportedNonStaticMatcherForSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To specify a setup for protected property {0}.{1}, use:
        ///mock.Setup&lt;{2}&gt;(x =&gt; x.{1}).Returns(value);
        ///mock.SetupGet(x =&gt; x.{1}).Returns(value); //equivalent to previous one
        ///mock.SetupSet(x =&gt; x.{1}).Callback(callbackDelegate);.
        /// </summary>
        internal static string UnsupportedProtectedProperty {
            get {
                return ResourceManager.GetString("UnsupportedProtectedProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The following setups were not matched:
        ///{0}.
        /// </summary>
        internal static string VerficationFailed {
            get {
                return ResourceManager.GetString("VerficationFailed", resourceCulture);
            }
        }
    }
}
