using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Errors
{
    public class Option
    {
        public static Option<T> Some<T>(T item) { return new Option<T>(item); }
        public static Option<T> None<T>() { return new Option<T>(); }
    }

    public class Option<T>
    {
        readonly T item;
        readonly bool has_some;

        public Option(T item) { this.item = item; this.has_some = true; }
        public Option() { this.item = default!; this.has_some = false; }

        public T unwrap([CallerFilePath] string path = null, [CallerLineNumber] int? line_number = null)
        {
            if (this.has_some)
                return this.item;

            throw new InvalidOperationException($"{path}:{line_number} Called unwrap() on a None value");
        }
        public bool is_some() { return this.has_some; }
        public bool is_none() { return !this.has_some; }

        public R match<R>(Func<T, R> some, Func<R> none)
        {
            return this.has_some ? some(this.item) : none();
        }

        public void match(Action<T> some, Action none)
        {
            if (this.has_some) some(this.item);
            else none();
        }
    }

    public static class OptionExtension
    {
        public static Option<T> as_some<T>(this T item) { return Option.Some(item); }
        public static Option<T> as_none<T>(this T _) { return Option.None<T>(); }
    }














    readonly struct Nothing;
    readonly struct Ok<T>(T i)
    {
        public T item { get; } = i;
        public override string ToString() { return $"Ok({item.GetType().FullName})"; }
    }
    readonly struct Err<E>(E e) {
        public E err { get; } = e;
        public override string ToString() { return $"Err({err.GetType().FullName})"; }
    }


    readonly struct Result
    {
        public static Ok<T> Ok<T>(T i)
        {
            return new Ok<T>(i);
        }
        public static Err<E> Err<E>(E e) {
            return new Err<E>(e);
        }

        public static Ok<Nothing> Ok()
        {
            return new Ok<Nothing>(new Nothing());
        }
    }

    readonly struct Result<T, E> {
        readonly bool success;
        readonly T ok;
        readonly E error;
        

        public Result(Ok<T> i) { ok = i.item; success = true; error = default!; }
        public Result(Err<E> e) { error = e.err; success = false; ok = default!; }
        public static implicit operator Result<T, E>(Ok<T> ok)
        {
            return new Result<T, E>(ok);
        }
        public static implicit operator Result<T,E>(Err<E> err)
        {
            return new Result<T, E>(err);
        }

        public bool is_ok() { return success; }
        public bool is_err() { return !success; }
        public T unwrap([CallerFilePath] string path = null, [CallerLineNumber] int? line_number = null)
        {
            if (success)
                return this.ok;

            throw new InvalidOperationException($"{path}:{line_number} Called unwrap() on an Err value");
        }
        public E unwrap_err([CallerFilePath] string path = null, [CallerLineNumber] int? line_number = null)
        {
            if (!success)
                return this.error;
            throw new InvalidOperationException($"{path}:{line_number} Called unwrap() on an Ok value");
        }

        public T unwrap_or_else(Func<T> or) {
            if (success)
                return ok;
            return or();
        }

        public T unwrap_or(T or)
        {
            if (success)
                return ok;
            return or;
        }

        public R match<R>(Func<T, R> ok, Func<R> err) {
            return this.success ? ok(this.ok) : err();
        }

        public void match(Action<T> ok, Action err)
        {
            if (this.success) ok(this.ok);
            else err();
        }

        public override string ToString()
        {
            if(success)
                return $"Ok({ok})";
            return $"Err({error})";
        }
    }
}
