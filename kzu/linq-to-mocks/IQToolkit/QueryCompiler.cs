﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace IQToolkit
{
    /// <summary>
    /// Creates a reusable, parameterized representation of a query that caches the execution plan
    /// </summary>
    public static class QueryCompiler
    {
        public static Delegate Compile(LambdaExpression query)
        {
            CompiledQuery cq = new CompiledQuery(query);
            Type dt = query.Type;
            MethodInfo method = dt.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
            ParameterInfo[] parameters = method.GetParameters();
            ParameterExpression[] pexprs = parameters.Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
            var args = Expression.NewArrayInit(typeof(object), pexprs.Select(p => Expression.Convert(p, typeof(object))).ToArray());
            Expression body = Expression.Convert(Expression.Call(Expression.Constant(cq), "Invoke", Type.EmptyTypes, args), method.ReturnType);
            LambdaExpression e = Expression.Lambda(dt, body, pexprs);
            return e.Compile();
        }

        public static D Compile<D>(Expression<D> query)
        {
            return (D)(object)Compile((LambdaExpression)query);
        }

        public static Func<TResult> Compile<TResult>(Expression<Func<TResult>> query)
        {
            return new CompiledQuery(query).Invoke<TResult>;
        }

        public static Func<T1, TResult> Compile<T1, TResult>(Expression<Func<T1, TResult>> query)
        {
            return new CompiledQuery(query).Invoke<T1, TResult>;
        }

        public static Func<T1, T2, TResult> Compile<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> query)
        {
            return new CompiledQuery(query).Invoke<T1, T2, TResult>;
        }

        public static Func<T1, T2, T3, TResult> Compile<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> query)
        {
            return new CompiledQuery(query).Invoke<T1, T2, T3, TResult>;
        }

        public static Func<T1, T2, T3, T4, TResult> Compile<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> query)
        {
            return new CompiledQuery(query).Invoke<T1, T2, T3, T4, TResult>;
        }

        public static Func<IEnumerable<T>> Compile<T>(this IQueryable<T> source)
        {
            return Compile<IEnumerable<T>>(
                Expression.Lambda<Func<IEnumerable<T>>>(((IQueryable)source).Expression)
                );
        }

        public class CompiledQuery
        {
            LambdaExpression query;
            Delegate fnQuery;

            internal CompiledQuery(LambdaExpression query)
            {
                this.query = query;
            }

            internal void Compile(params object[] args)
            {
                if (this.fnQuery == null)
                {
                    // first identify the query provider being used
                    Expression body = this.query.Body;

                    // ask the query provider to compile the query by 'executing' the lambda expression
                    IQueryProvider provider = this.FindProvider(body, args);
                    if (provider == null)
                    {
                        throw new InvalidOperationException("Could not find query provider");
                    }

                    Delegate result = (Delegate)provider.Execute(this.query);
                    System.Threading.Interlocked.CompareExchange(ref this.fnQuery, result, null);
                }
            }

            internal IQueryProvider FindProvider(Expression expression, object[] args)
            {
                ConstantExpression root = TypedSubtreeFinder.Find(expression, typeof(IQueryProvider)) as ConstantExpression;
                if (root == null)
                {
                    root = TypedSubtreeFinder.Find(expression, typeof(IQueryable)) as ConstantExpression;
                }
                if (root == null && args != null && args.Length > 0)
                {
                    Expression replaced = ExpressionReplacer.ReplaceAll(
                        expression,
                        this.query.Parameters.ToArray(),
                        args.Select((a, i) => Expression.Constant(a, this.query.Parameters[i].Type)).ToArray()
                        );
                    Expression partial = PartialEvaluator.Eval(replaced);
                    return FindProvider(partial, null);
                }
                if (root != null) 
                {
                    IQueryProvider provider = root.Value as IQueryProvider;
                    if (provider == null)
                    {
                        IQueryable query = root.Value as IQueryable;
                        if (query != null)
                        {
                            provider = query.Provider;
                        }
                    }
                    return provider;
                }
                return null;
            }

            public object Invoke(object[] args)
            {
                this.Compile(args);
                try
                {
                    return this.fnQuery.DynamicInvoke(args);
                }
                catch (TargetInvocationException tie)
                {
                    throw tie.InnerException;
                }
            }

            internal TResult Invoke<TResult>()
            {
                this.Compile(null);
                return ((Func<TResult>)this.fnQuery)();
            }

            internal TResult Invoke<T1, TResult>(T1 arg)
            {
                this.Compile(arg);
                return ((Func<T1, TResult>)this.fnQuery)(arg);
            }

            internal TResult Invoke<T1, T2, TResult>(T1 arg1, T2 arg2)
            {
                this.Compile(arg1, arg2);
                return ((Func<T1, T2, TResult>)this.fnQuery)(arg1, arg2);
            }

            internal TResult Invoke<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3)
            {
                this.Compile(arg1, arg2, arg3);
                return ((Func<T1, T2, T3, TResult>)this.fnQuery)(arg1, arg2, arg3);
            }

            internal TResult Invoke<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                this.Compile(arg1, arg2, arg3, arg4);
                return ((Func<T1, T2, T3, T4, TResult>)this.fnQuery)(arg1, arg2, arg3, arg4);
            }
        }
    }
}