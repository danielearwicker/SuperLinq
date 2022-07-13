﻿namespace SuperLinq.Async;

public static partial class AsyncSuperEnumerable
{

	/// <summary>
	/// Splits the source sequence by a separator.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separator">Separator element.</param>
	/// <returns>A sequence of splits of elements.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	public static IAsyncEnumerable<IEnumerable<TSource>> Split<TSource>(
		this IAsyncEnumerable<TSource> source,
		TSource separator)
	{
		return Split(source, separator, int.MaxValue);
	}

	/// <summary>
	/// Splits the source sequence by a separator given a maximum count of splits.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separator">Separator element.</param>
	/// <param name="count">Maximum number of splits.</param>
	/// <returns>A sequence of splits of elements.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 1.</exception>
	public static IAsyncEnumerable<IEnumerable<TSource>> Split<TSource>(
		this IAsyncEnumerable<TSource> source,
		TSource separator, int count)
	{
		return Split(source, separator, count, s => s);
	}

	/// <summary>
	/// Splits the source sequence by a separator and then transforms
	/// the splits into results.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <typeparam name="TResult">Type of the result sequence elements.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separator">Separator element.</param>
	/// <param name="resultSelector">Function used to project splits
	/// of source elements into elements of the resulting sequence.</param>
	/// <returns>
	/// A sequence of values typed as <typeparamref name="TResult"/>.
	/// </returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="resultSelector"/> is <see langword="null"/>.</exception>
	public static IAsyncEnumerable<TResult> Split<TSource, TResult>(
		this IAsyncEnumerable<TSource> source,
		TSource separator,
		Func<IEnumerable<TSource>, TResult> resultSelector)
	{
		return Split(source, separator, int.MaxValue, resultSelector);
	}

	/// <summary>
	/// Splits the source sequence by a separator, given a maximum count
	/// of splits, and then transforms the splits into results.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <typeparam name="TResult">Type of the result sequence elements.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separator">Separator element.</param>
	/// <param name="count">Maximum number of splits.</param>
	/// <param name="resultSelector">Function used to project splits
	/// of source elements into elements of the resulting sequence.</param>
	/// <returns>
	/// A sequence of values typed as <typeparamref name="TResult"/>.
	/// </returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="resultSelector"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 1.</exception>
	public static IAsyncEnumerable<TResult> Split<TSource, TResult>(
		this IAsyncEnumerable<TSource> source,
		TSource separator, int count,
		Func<IEnumerable<TSource>, TResult> resultSelector)
	{
		return Split(source, separator, null, count, resultSelector);
	}

	/// <summary>
	/// Splits the source sequence by a separator and then transforms the
	/// splits into results.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separator">Separator element.</param>
	/// <param name="comparer">Comparer used to determine separator
	/// element equality.</param>
	/// <returns>A sequence of splits of elements.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	public static IAsyncEnumerable<IEnumerable<TSource>> Split<TSource>(
		this IAsyncEnumerable<TSource> source,
		TSource separator, IEqualityComparer<TSource>? comparer)
	{
		return Split(source, separator, comparer, int.MaxValue);
	}

	/// <summary>
	/// Splits the source sequence by a separator, given a maximum count
	/// of splits. A parameter specifies how the separator is compared
	/// for equality.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separator">Separator element.</param>
	/// <param name="comparer">Comparer used to determine separator
	/// element equality.</param>
	/// <param name="count">Maximum number of splits.</param>
	/// <returns>A sequence of splits of elements.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 1.</exception>
	public static IAsyncEnumerable<IEnumerable<TSource>> Split<TSource>(
		this IAsyncEnumerable<TSource> source,
		TSource separator, IEqualityComparer<TSource>? comparer, int count)
	{
		return Split(source, separator, comparer, count, s => s);
	}

	/// <summary>
	/// Splits the source sequence by a separator and then transforms the
	/// splits into results. A parameter specifies how the separator is
	/// compared for equality.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <typeparam name="TResult">Type of the result sequence elements.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separator">Separator element.</param>
	/// <param name="comparer">Comparer used to determine separator
	/// element equality.</param>
	/// <param name="resultSelector">Function used to project splits
	/// of source elements into elements of the resulting sequence.</param>
	/// <returns>
	/// A sequence of values typed as <typeparamref name="TResult"/>.
	/// </returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="resultSelector"/> is <see langword="null"/>.</exception>
	public static IAsyncEnumerable<TResult> Split<TSource, TResult>(
		this IAsyncEnumerable<TSource> source,
		TSource separator, IEqualityComparer<TSource> comparer,
		Func<IEnumerable<TSource>, TResult> resultSelector)
	{
		return Split(source, separator, comparer, int.MaxValue, resultSelector);
	}

	/// <summary>
	/// Splits the source sequence by a separator, given a maximum count
	/// of splits, and then transforms the splits into results. A
	/// parameter specifies how the separator is compared for equality.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <typeparam name="TResult">Type of the result sequence elements.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separator">Separator element.</param>
	/// <param name="comparer">Comparer used to determine separator
	/// element equality.</param>
	/// <param name="count">Maximum number of splits.</param>
	/// <param name="resultSelector">Function used to project splits
	/// of source elements into elements of the resulting sequence.</param>
	/// <returns>
	/// A sequence of values typed as <typeparamref name="TResult"/>.
	/// </returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="resultSelector"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 1.</exception>
	public static IAsyncEnumerable<TResult> Split<TSource, TResult>(
		this IAsyncEnumerable<TSource> source,
		TSource separator, IEqualityComparer<TSource>? comparer, int count,
		Func<IEnumerable<TSource>, TResult> resultSelector)
	{
		source.ThrowIfNull();
		count.ThrowIfLessThan(1);
		resultSelector.ThrowIfNull();

		comparer ??= EqualityComparer<TSource>.Default;
		return Split(source, item => comparer.Equals(item, separator), count, resultSelector);
	}

	/// <summary>
	/// Splits the source sequence by separator elements identified by a
	/// function.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separatorFunc">Predicate function used to determine
	/// the splitter elements in the source sequence.</param>
	/// <returns>A sequence of splits of elements.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="separatorFunc"/> is <see langword="null"/>.</exception>
	public static IAsyncEnumerable<IEnumerable<TSource>> Split<TSource>(
		this IAsyncEnumerable<TSource> source,
		Func<TSource, bool> separatorFunc)
	{
		return Split(source, separatorFunc, int.MaxValue);
	}

	/// <summary>
	/// Splits the source sequence by separator elements identified by a
	/// function, given a maximum count of splits.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separatorFunc">Predicate function used to determine
	/// the splitter elements in the source sequence.</param>
	/// <param name="count">Maximum number of splits.</param>
	/// <returns>A sequence of splits of elements.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="separatorFunc"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 1.</exception>
	public static IAsyncEnumerable<IEnumerable<TSource>> Split<TSource>(
		this IAsyncEnumerable<TSource> source,
		Func<TSource, bool> separatorFunc, int count)
	{
		return Split(source, separatorFunc, count, s => s);
	}

	/// <summary>
	/// Splits the source sequence by separator elements identified by
	/// a function and then transforms the splits into results.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <typeparam name="TResult">Type of the result sequence elements.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separatorFunc">Predicate function used to determine
	/// the splitter elements in the source sequence.</param>
	/// <param name="resultSelector">Function used to project splits
	/// of source elements into elements of the resulting sequence.</param>
	/// <returns>
	/// A sequence of values typed as <typeparamref name="TResult"/>.
	/// </returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="separatorFunc"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="resultSelector"/> is <see langword="null"/>.</exception>
	public static IAsyncEnumerable<TResult> Split<TSource, TResult>(
		this IAsyncEnumerable<TSource> source,
		Func<TSource, bool> separatorFunc,
		Func<IEnumerable<TSource>, TResult> resultSelector)
	{
		return Split(source, separatorFunc, int.MaxValue, resultSelector);
	}

	/// <summary>
	/// Splits the source sequence by separator elements identified by
	/// a function, given a maximum count of splits, and then transforms
	/// the splits into results.
	/// </summary>
	/// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
	/// <typeparam name="TResult">Type of the result sequence elements.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="separatorFunc">Predicate function used to determine
	/// the splitter elements in the source sequence.</param>
	/// <param name="count">Maximum number of splits.</param>
	/// <param name="resultSelector">Function used to project a split
	/// group of source elements into an element of the resulting sequence.</param>
	/// <returns>
	/// A sequence of values typed as <typeparamref name="TResult"/>.
	/// </returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="separatorFunc"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="resultSelector"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 1.</exception>
	public static IAsyncEnumerable<TResult> Split<TSource, TResult>(
		this IAsyncEnumerable<TSource> source,
		Func<TSource, bool> separatorFunc, int count,
		Func<IEnumerable<TSource>, TResult> resultSelector)
	{
		source.ThrowIfNull();
		separatorFunc.ThrowIfNull();
		count.ThrowIfLessThan(1);
		resultSelector.ThrowIfNull();

		return _(source, separatorFunc, count, resultSelector);

		static async IAsyncEnumerable<TResult> _(
			IAsyncEnumerable<TSource> source,
			Func<TSource, bool> separatorFunc, int count,
			Func<IEnumerable<TSource>, TResult> resultSelector,
			[EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			if (count == 0) // No splits?
			{
				yield return resultSelector(await source.ToListAsync(cancellationToken).ConfigureAwait(false));
				yield break;
			}

			var items = new List<TSource>();

			await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
			{
				if (count > 0 && separatorFunc(item))
				{
					yield return resultSelector(items);
					count--;
					items = new();
				}
				else
				{
					items.Add(item);
				}
			}

			if (items.Count > 0)
				yield return resultSelector(items);
		}
	}
}