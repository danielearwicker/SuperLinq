﻿using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace SuperLinq.Test;

[TestFixture]
public class SkipUntilTest
{
	[Test]
	public void SkipUntilPredicateNeverFalse()
	{
		var sequence = Enumerable.Range(0, 5).SkipUntil(x => x != 100);
		sequence.AssertSequenceEqual(1, 2, 3, 4);
	}

	[Test]
	public void SkipUntilPredicateNeverTrue()
	{
		var sequence = Enumerable.Range(0, 5).SkipUntil(x => x == 100);
		Assert.That(sequence, Is.Empty);
	}

	[Test]
	public void SkipUntilPredicateBecomesTrueHalfWay()
	{
		var sequence = Enumerable.Range(0, 5).SkipUntil(x => x == 2);
		sequence.AssertSequenceEqual(3, 4);
	}

	[Test]
	public void SkipUntilEvaluatesSourceLazily()
	{
		new BreakingSequence<string>().SkipUntil(x => x.Length == 0);
	}

	[Test]
	public void SkipUntilEvaluatesPredicateLazily()
	{
		// Predicate would explode at x == 0, but we never need to evaluate it as we've
		// started returning items after -1.
		var sequence = Enumerable.Range(-2, 5).SkipUntil(x => 1 / x == -1);
		sequence.AssertSequenceEqual(0, 1, 2);
	}

	public static readonly IEnumerable<ITestCaseData> TestData =
		from e in new[]
		{
				new { Source = Array.Empty<int>()       , Min = 0, Expected = Array.Empty<int>()     }, // empty sequence
                new { Source = new[] { 0       }, Min = 0, Expected = Array.Empty<int>()     }, // one-item sequence, predicate succeed
                new { Source = new[] { 0       }, Min = 1, Expected = Array.Empty<int>()     }, // one-item sequence, predicate don't succeed
                new { Source = new[] { 1, 2, 3 }, Min = 0, Expected = new[] { 2, 3 } }, // predicate succeed on first item
                new { Source = new[] { 1, 2, 3 }, Min = 1, Expected = new[] { 2, 3 } },
				new { Source = new[] { 1, 2, 3 }, Min = 2, Expected = new[] { 3    } },
				new { Source = new[] { 1, 2, 3 }, Min = 3, Expected = Array.Empty<int>()     }, // predicate succeed on last item
                new { Source = new[] { 1, 2, 3 }, Min = 4, Expected = Array.Empty<int>()     }  // predicate never succeed
		}
		select new TestCaseData(e.Source, e.Min).Returns(e.Expected);

	[Test, TestCaseSource(nameof(TestData))]
	public int[] TestSkipUntil(int[] source, int min)
	{
		return source.AsTestingSequence().SkipUntil(v => v >= min).ToArray();
	}
}