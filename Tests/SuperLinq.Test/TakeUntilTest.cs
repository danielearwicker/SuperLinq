﻿namespace Test;

public class TakeUntilTest
{
	[Fact]
	public void TakeUntilPredicateNeverFalse()
	{
		var sequence = Enumerable.Range(0, 5).TakeUntil(x => x != 100);
		sequence.AssertSequenceEqual(0);
	}

	[Fact]
	public void TakeUntilPredicateNeverTrue()
	{
		var sequence = Enumerable.Range(0, 5).TakeUntil(x => x == 100);
		sequence.AssertSequenceEqual(0, 1, 2, 3, 4);
	}

	[Fact]
	public void TakeUntilPredicateBecomesTrueHalfWay()
	{
		var sequence = Enumerable.Range(0, 5).TakeUntil(x => x == 2);
		sequence.AssertSequenceEqual(0, 1, 2);
	}

	[Fact]
	public void TakeUntilEvaluatesSourceLazily()
	{
		new BreakingSequence<string>().TakeUntil(x => x.Length == 0);
	}

	[Fact]
	public void TakeUntilEvaluatesPredicateLazily()
	{
		// Predicate would explode at x == 0, but we never need to evaluate it due to the Take call.
		var sequence = Enumerable.Range(-2, 5).TakeUntil(x => 1 / x == 1).Take(3);
		sequence.AssertSequenceEqual(-2, -1, 0);
	}
}