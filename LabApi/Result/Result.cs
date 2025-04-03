using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LabApi.Errors
{
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
        readonly T? ok;
        readonly E? error;
        

        public Result(Ok<T> i) { ok = i.item; success = true; }
        public Result(Err<E> e) { error = e.err; success = false; }
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
            throw new InvalidOperationException($"{path}:{line_number} Called unwrap() on an Err value");
        }

        public T unwrap_or(Func<T> or) {
            if (success)
                return ok;
            return or();
        }

        public T unwrap_or_else(T or)
        {
            if (success)
                return ok;
            return or;
        }

        public override string ToString()
        {
            if(success)
                return $"Ok({ok})";
            return $"Err({error})";
        }
    }
}
