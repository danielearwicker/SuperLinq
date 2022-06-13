﻿using NUnit.Framework;

namespace SuperLinq.Test;

[TestFixture]
public class UnfoldTest
{
	[Test]
	public void UnfoldInfiniteSequence()
	{
		var result = SuperEnumerable.Unfold(1, x => (Result: x, State: x + 1),
											  _ => true,
											  e => e.State,
											  e => e.Result)
								   .Take(100);

		var expectations = SuperEnumerable.Generate(1, x => x + 1).Take(100);

		Assert.That(result, Is.EqualTo(expectations));
	}

	[Test]
	public void UnfoldFiniteSequence()
	{
		var result = SuperEnumerable.Unfold(1, x => (Result: x, State: x + 1),
											  e => e.Result <= 100,
											  e => e.State,
											  e => e.Result);

		var expectations = SuperEnumerable.Generate(1, x => x + 1).Take(100);

		Assert.That(result, Is.EqualTo(expectations));
	}

	[Test]
	public void UnfoldIsLazy()
	{
		SuperEnumerable.Unfold(0, BreakingFunc.Of<int, (int, int)>(),
								 BreakingFunc.Of<(int, int), bool>(),
								 BreakingFunc.Of<(int, int), int>(),
								 BreakingFunc.Of<(int, int), int>());
	}


	[Test]
	public void UnfoldSingleElementSequence()
	{
		var result = SuperEnumerable.Unfold(0, x => (Result: x, State: x + 1),
											  x => x.Result == 0,
											  e => e.State,
											  e => e.Result);

		var expectations = new[] { 0 };

		Assert.That(result, Is.EqualTo(expectations));
	}

	[Test]
	public void UnfoldEmptySequence()
	{
		var result = SuperEnumerable.Unfold(0, x => (Result: x, State: x + 1),
											  x => x.Result < 0,
											  e => e.State,
											  e => e.Result);
		Assert.That(result, Is.Empty);
	}
}