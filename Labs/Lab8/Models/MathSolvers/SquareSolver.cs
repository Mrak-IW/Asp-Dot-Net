using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab8.Models.MathSolvers
{
	public static class SquareSolver
	{
		public static ComplexNumber[] Solve(double a, double b, double c)
		{
			double d;

			d = b * b - 4 * a * c;
			ComplexNumber[] x = new ComplexNumber[d == 0 ? 1 : 2];

			if (d < 0)
			{
				x[0] = (new ComplexNumber(0, 1) * Math.Sqrt(-d) - b) / (2 * a);
				x[1] = (new ComplexNumber(0, 1) * -Math.Sqrt(-d) - b) / (2 * a);
			}
			else if (d == 0)
			{
				x[0] = new ComplexNumber(-b / (2 * a), 0);
			}
			else
			{
				x[0] = new ComplexNumber((Math.Sqrt(d) - b) / (2 * a), 0);
				x[1] = new ComplexNumber(-(Math.Sqrt(d) - b) / (2 * a), 0);
			}
			return x;
		}
	}
}