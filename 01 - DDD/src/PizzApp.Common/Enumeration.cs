using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PizzApp.Common
{
    /// <inheritdoc />
    /// <summary>
    ///     Based on:
    ///     <see
    ///         href="https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types" />
    /// </summary>
    public abstract class Enumeration<TId, TElement> : IComparable
        where TId : IComparable, IEquatable<TId>
        where TElement : Enumeration<TId, TElement>
    {
        protected Enumeration(TId id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Name { get; }

        public TId Id { get; }

        public static bool operator !=(Enumeration<TId, TElement> left, Enumeration<TId, TElement> right) =>
            !(left == right);

        public static bool operator ==(Enumeration<TId, TElement> left, Enumeration<TId, TElement> right) =>

            // ReSharper disable ArrangeRedundantParentheses
            (left is null && right is null) || (!ReferenceEquals(left, null) && left.Equals(right));

        public static bool operator <(Enumeration<TId, TElement> left, Enumeration<TId, TElement> right) =>
            Compare(left, right) < 0;

        public static bool operator >(Enumeration<TId, TElement> left, Enumeration<TId, TElement> right) =>
            Compare(left, right) > 0;

        public static bool operator <=(Enumeration<TId, TElement> left, Enumeration<TId, TElement> right) =>
            Compare(left, right) <= 0;

        public static bool operator >=(Enumeration<TId, TElement> left, Enumeration<TId, TElement> right) =>
            Compare(left, right) >= 0;

        public static IEnumerable<TElement> GetAll()
        {
            var fields =
                typeof(TElement).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<TElement>();
        }

        public static TElement GetById(TId id) => GetAll().First(x => x.Id.Equals(id));

        public static int Compare(Enumeration<TId, TElement> left, Enumeration<TId, TElement> right)
        {
            if (ReferenceEquals(left, right))
            {
                return 0;
            }

            if (ReferenceEquals(left, null))
            {
                return -1;
            }

            return left.CompareTo(right);
        }

        public int CompareTo(object obj) => this.Id.CompareTo(((TElement)obj).Id);

        public override string ToString() => this.Name;

        public override bool Equals(object obj)
        {
            var otherValue = obj as TElement;
            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = this.GetType() == obj.GetType();
            var valueMatches = this.Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => this.Id.GetHashCode() ^ this.Name.GetHashCode();
    }
}