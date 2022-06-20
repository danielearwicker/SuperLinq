﻿namespace Test;

/// <summary>
/// Verify the behavior of the RunLengthEncode() operator
/// </summary>
public class RunLengthEncodeTests
{
	/// <summary>
	/// Verify that the RunLengthEncode() methods behave in a lazy manner.
	/// </summary>
	[Fact]
	public void TestRunLengthEncodeIsLazy()
	{
		new BreakingSequence<int>().RunLengthEncode();
		new BreakingSequence<int>().RunLengthEncode(EqualityComparer<int>.Default);
	}

	/// <summary>
	/// Verify that run-length encoding an empty sequence results in an empty sequence.
	/// </summary>
	[Fact]
	public void TestRunLengthEncodeEmptySequence()
	{
		var sequence = Enumerable.Empty<int>();
		var result = sequence.RunLengthEncode();

		Assert.Empty(result);
	}

	/// <summary>
	/// Verify that run-length encoding correctly accepts and uses custom equality comparers.
	/// </summary>
	[Fact]
	public void TestRunLengthEncodeCustomComparer()
	{
		var sequence = new[] { "a", "A", "a", "b", "b", "B", "B" };

		var result = sequence.RunLengthEncode(StringComparer.InvariantCultureIgnoreCase)
							 .Select(kvp => (kvp.value.ToLowerInvariant(), kvp.count));
		var expectedResult = new[]
		{
			("a", 3),
			("b", 4)
		};

		Assert.Equal(expectedResult, result);
	}

	/// <summary>
	/// Verify that run-length encoding a known sequence produced a correct result.
	/// </summary>
	[Fact]
	public void TestRunLengthEncodeResults()
	{
		var sequence = new[] { 1, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6 };
		var expectedResult = Enumerable.Range(1, 6).Select(x => (x, x));
		var result = sequence.RunLengthEncode();

		Assert.Equal(expectedResult, result);
	}

	/// <summary>
	/// Verify that run-length encoding a sequence with no runs produces a correct result.
	/// </summary>
	[Fact]
	public void TestRunLengthEncodeNoRuns()
	{
		var sequence = Enumerable.Range(1, 10);
		var result = sequence.RunLengthEncode();
		var expectedResult = sequence.Select(x => (x, 1));

		Assert.Equal(expectedResult, result);
	}

	/// <summary>
	/// Verify that run-length encoding a sequence consisting of a single repeated value
	/// produces a correct result.
	/// </summary>
	[Fact]
	public void TestRunLengthEncodeOneRun()
	{
		const char value = 'q';
		const int repeatCount = 10;
		var sequence = Enumerable.Repeat(value, repeatCount);
		var result = sequence.RunLengthEncode();
		var expectedResult = new[] { (value, repeatCount) };

		Assert.Equal(expectedResult, result);
	}
}