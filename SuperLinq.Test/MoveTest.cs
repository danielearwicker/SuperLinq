﻿using NUnit.Framework;

namespace SuperLinq.Test;

[TestFixture]
public class MoveTest
{
	[Test]
	public void MoveWithNegativeFromIndex()
	{
		AssertThrowsArgument.OutOfRangeException("fromIndex", () =>
			new[] { 1 }.Move(-1, 0, 0));
	}

	[Test]
	public void MoveWithNegativeCount()
	{
		AssertThrowsArgument.OutOfRangeException("count", () =>
			new[] { 1 }.Move(0, -1, 0));
	}

	[Test]
	public void MoveWithNegativeToIndex()
	{
		AssertThrowsArgument.OutOfRangeException("toIndex", () =>
			new[] { 1 }.Move(0, 0, -1));
	}

	[Test]
	public void MoveIsLazy()
	{
		new BreakingSequence<int>().Move(0, 0, 0);
	}

	[TestCaseSource(nameof(MoveSource))]
	public void Move(int length, int fromIndex, int count, int toIndex)
	{
		var source = Enumerable.Range(0, length);

		using var test = source.AsTestingSequence();

		var result = test.Move(fromIndex, count, toIndex);

		var slice = source.Slice(fromIndex, count);
		var exclude = source.Exclude(fromIndex, count);
		var expectations = exclude.Take(toIndex).Concat(slice).Concat(exclude.Skip(toIndex));
		Assert.That(result, Is.EqualTo(expectations));
	}

	public static IEnumerable<object> MoveSource()
	{
		const int length = 10;
		return from index in Enumerable.Range(0, length)
			   from count in Enumerable.Range(0, length + 1)
			   from tcd in new[]
			   {
					   new TestCaseData(length, index, count, Math.Max(0, index - 1)),
					   new TestCaseData(length, index, count, index + 1),
				   }
			   select tcd;
	}

	[TestCaseSource(nameof(MoveWithSequenceShorterThanToIndexSource))]
	public void MoveWithSequenceShorterThanToIndex(int length, int fromIndex, int count, int toIndex)
	{
		var source = Enumerable.Range(0, length);

		using var test = source.AsTestingSequence();

		var result = test.Move(fromIndex, count, toIndex);

		var expectations = source.Exclude(fromIndex, count).Concat(source.Slice(fromIndex, count));
		Assert.That(result, Is.EqualTo(expectations));
	}

	public static IEnumerable<object> MoveWithSequenceShorterThanToIndexSource()
	{
		const int length = 10;

		return Enumerable.Range(length, length + 5)
						 .Select(toIndex => new TestCaseData(length, 5, 2, toIndex));
	}

	[Test]
	public void MoveIsRepeatable()
	{
		var source = Enumerable.Range(0, 10);
		var result = source.Move(0, 5, 10);

		Assert.That(result.ToArray(), Is.EqualTo(result));
	}

	[Test]
	public void MoveWithFromIndexEqualsToIndex()
	{
		var source = Enumerable.Range(0, 10);
		var result = source.Move(5, 999, 5);

		Assert.That(source, Is.SameAs(result));
	}

	[Test]
	public void MoveWithCountEqualsZero()
	{
		var source = Enumerable.Range(0, 10);
		var result = source.Move(5, 0, 999);

		Assert.That(source, Is.SameAs(result));
	}
}